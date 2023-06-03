using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Resources;
using TextureBackport.Api;
using TextureBackport.Api.Textures;

namespace TextureBackport.Gui;

public partial class GUI : Form
{
    public Dictionary<string, Api.Version> versions;
    public Dictionary<string, TextureResolution> resolutions;

    public GUI()
    {
        InitializeComponent();
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        versions = new Dictionary<string, Api.Version>()
        {
            ["1.8.x"] = Api.Version.V18X,
            ["1.9.x"] = Api.Version.V19X,
            ["1.10.x"] = Api.Version.V110X,
            ["1.11.x"] = Api.Version.V111X,
            ["1.12.x"] = Api.Version.V112X,
            ["1.13.x"] = Api.Version.V113X,
            ["1.14.x"] = Api.Version.V114X,
            ["1.15.x"] = Api.Version.V115X,
            ["1.16.x"] = Api.Version.V116X,
            ["1.17.x"] = Api.Version.V117X,
            ["1.18.x"] = Api.Version.V118X,
            ["1.19.x"] = Api.Version.V119X
        };

        resolutions = new Dictionary<string, TextureResolution>()
        {
            ["16x16"] = TextureResolution.X16,
            ["32x32"] = TextureResolution.X32,
            ["64x64"] = TextureResolution.X64,
            ["128x128"] = TextureResolution.X128,
            ["256x256"] = TextureResolution.X256,
            ["512x512"] = TextureResolution.X512,
        };
        Load += GUI_Load;
        tboxItem.TextChanged += TextBox_TextChanged;
        tboxTerrain.TextChanged += TextBox_TextChanged;
        tboxSourceFile.TextChanged += TextBox_TextChanged;
        tboxOutput.TextChanged += TextBox_TextChanged;
        btnSearchSourceFile.Click += BtnSearchSourceFile_Click;
        btnSearchOutput.Click += BtnSearchOutput_Click;
        btnSearchTerrain.Click += BtnSearchTerrain_Click;
        btnSearchItem.Click += BtnSearchItem_Click;
        chDefaultTemplates.CheckedChanged += ChDefaultTemplates_CheckedChanged;
        btnPort.Click += BtnPort_Click;
        Backport.OnPortFinished += Backport_OnPortFinished;
        btnPort.Enabled = false;
    }

    private void TextBox_TextChanged(object? sender, EventArgs e)
    {
        btnPort.Enabled = validation();
    }

    private bool validation()
    {
        bool inout = File.Exists(tboxSourceFile.Text) && Directory.Exists(tboxOutput.Text);
        bool optional = chDefaultTemplates.Checked ? true : File.Exists(tboxTerrain.Text) && File.Exists(tboxItem.Text);

        return inout && optional;
    }

    private void Backport_OnPortFinished()
    {
        Invoke(new MethodInvoker(delegate () { btnPort.Enabled = true; }));
        Invoke(new MethodInvoker(delegate () { pbarArmor.Value = pbarArmor.Maximum; }));
        Invoke(new MethodInvoker(delegate () { pbarItems.Value = pbarItems.Maximum; }));
        Invoke(new MethodInvoker(delegate () { pbarMobs.Value = pbarMobs.Maximum; }));
        Invoke(new MethodInvoker(delegate () { pbarTerrain.Value = pbarTerrain.Maximum; }));
    }

    private void GUI_Load(object? sender, EventArgs e)
    {
        foreach (var name in versions.Keys)
            cbVersion.Items.Add(name);
        cbVersion.SelectedIndex = 0;

        foreach (var name in resolutions.Keys)
            cbResolution.Items.Add(name);
        cbResolution.SelectedIndex = 0;

        chDefaultTemplates.Checked = true;
    }

    private void BtnSearchSourceFile_Click(object? sender, EventArgs e)
    {
        var ofd = new OpenFileDialog();
        ofd.Filter = "ZIP Files|*.zip";
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            tboxSourceFile.Text = ofd.FileName;
            btnPort.Enabled = validation();
        }
    }

    private void BtnSearchOutput_Click(object? sender, EventArgs e)
    {
        var fbd = new FolderBrowserDialog();
        if (fbd.ShowDialog() == DialogResult.OK)
        {
            tboxOutput.Text = fbd.SelectedPath;
            btnPort.Enabled = validation();
        }
    }

    private void BtnSearchTerrain_Click(object? sender, EventArgs e)
    {
        var ofd = new OpenFileDialog();
        ofd.Filter = "PNG Files|*.png";
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            tboxTerrain.Text = ofd.FileName;
            btnPort.Enabled = validation();
        }
    }

    private void BtnSearchItem_Click(object? sender, EventArgs e)
    {
        var ofd = new OpenFileDialog();
        ofd.Filter = "PNG Files| *.png";
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            tboxItem.Text = ofd.FileName;
            btnPort.Enabled = validation();
        }
    }

    private void ChDefaultTemplates_CheckedChanged(object? sender, EventArgs e)
    {
        tboxTerrain.Enabled = !chDefaultTemplates.Checked;
        tboxItem.Enabled = !chDefaultTemplates.Checked;
        btnSearchItem.Enabled = !chDefaultTemplates.Checked;
        btnSearchTerrain.Enabled = !chDefaultTemplates.Checked;
    }

    private async void BtnPort_Click(object? sender, EventArgs e)
    {
        btnPort.Enabled = false;

        var terrainTemplate = chDefaultTemplates.Checked ? null : tboxTerrain.Text;
        var itemTemplate = chDefaultTemplates.Checked ? null : tboxItem.Text;
        var version = versions[cbVersion.SelectedItem.ToString()!];
        var resolution = resolutions[cbResolution.SelectedItem.ToString()!];
        var sourceFile = tboxSourceFile.Text;
        var targetDirectory = tboxOutput.Text;
        Backport.TerrainTemplate = Properties.Resources.terrain;
        Backport.ItemTemplate = Properties.Resources.items;
        await Task.Run(() => Backport.CreateTexturePack(
            version,
            resolution,
            sourceFile,
            targetDirectory,
            terrainTemplate,
            itemTemplate));

        pbarTerrain.Maximum = Backport.terrain.TextureCount;
        pbarItems.Maximum = Backport.items.TextureCount;
        pbarMobs.Maximum = Backport.entity.FileCount;
        pbarArmor.Maximum = Backport.armor.FileCount;

        Backport.terrain.OnProgressChanged += Terrain_OnProgressChanged;
        Backport.items.OnProgressChanged += Items_OnProgressChanged;
        Backport.entity.OnProgressChanged += Entity_OnProgressChanged;
        Backport.armor.OnProgressChanged += Armor_OnProgressChanged;
    }

    private void Armor_OnProgressChanged(int progress)
    {
        Invoke(new MethodInvoker(delegate () { pbarArmor.Value = progress; }));
    }

    private void Entity_OnProgressChanged(int progress)
    {
        Invoke(new MethodInvoker(delegate () { pbarMobs.Value = progress; }));
    }

    private void Items_OnProgressChanged(int progress)
    {
        Invoke(new MethodInvoker(delegate () { pbarItems.Value = progress; }));
    }

    private void Terrain_OnProgressChanged(int progress)
    {
        Invoke(new MethodInvoker(delegate () { pbarTerrain.Value = progress; }));
    }
}