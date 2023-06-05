using TextureBackport.Api;
using TextureBackport.Api.ResourceMapping;

namespace TextureBackport.Gui;

public partial class GUI : Form
{
    public Dictionary<string, TextureResolution> resolutions;
    public Dictionary<string, LogLevel> logLevels;

    public XmlBackport Backport;

    public GUI()
    {
        InitializeComponent();
        //Backport = new XmlBackport(@"resource_maps\default.xml");
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        CenterToScreen();

        resolutions = new Dictionary<string, TextureResolution>()
        {
            ["16x16"] = TextureResolution.X16,
            ["32x32"] = TextureResolution.X32,
            ["64x64"] = TextureResolution.X64,
            ["128x128"] = TextureResolution.X128,
            ["256x256"] = TextureResolution.X256,
            ["512x512"] = TextureResolution.X512,
        };

        logLevels = new Dictionary<string, LogLevel>()
        {
            ["Information"] = LogLevel.INFO,
            ["Warning"] = LogLevel.WARN,
            ["Error"] = LogLevel.ERROR
        };
        Load += GUI_Load;
        tboxSourceFile.TextChanged += TextBox_TextChanged;
        tboxOutput.TextChanged += TextBox_TextChanged;
        btnSearchSourceFile.Click += BtnSearchSourceFile_Click;
        cbResourceMap.SelectedIndexChanged += CbResourceMap_SelectedIndexChanged;
        btnSearchOutput.Click += BtnSearchOutput_Click;
        btnPort.Click += BtnPort_Click;
        btnPort.Enabled = false;
    }

    private void CbResourceMap_SelectedIndexChanged(object? sender, EventArgs e)
    {
        cbVersion.Items.Clear();
        Backport = new XmlBackport(cbResourceMap.SelectedItem.ToString()!);

        foreach (var version in Backport.GetSupportedVersions())
            cbVersion.Items.Add(version.Name);
        cbVersion.SelectedIndex = 0;
    }

    private void TextBox_TextChanged(object? sender, EventArgs e)
    {
        btnPort.Enabled = validation();
    }

    private bool validation()
    {
        return cbResourceMap.Items.Count > 0 && File.Exists(tboxSourceFile.Text) && Directory.Exists(tboxOutput.Text);
    }

    private void GUI_Load(object? sender, EventArgs e)
    {
        if (Backport != null)
        {
            foreach (var version in Backport.GetSupportedVersions())
                cbVersion.Items.Add(version.Name);
            cbVersion.SelectedIndex = 0;
        }

        foreach (var name in resolutions.Keys)
            cbResolution.Items.Add(name);
        cbResolution.SelectedIndex = 0;

        if (Directory.Exists("resource_maps"))
        {
            foreach (var name in Directory.GetFiles("resource_maps", "*.xml"))
                cbResourceMap.Items.Add(name);
            cbResourceMap.SelectedIndex = 0;
        }

        foreach (var name in logLevels.Keys)
            cbLogLevel.Items.Add(name);
        cbLogLevel.SelectedIndex = 2;
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

    private async void BtnPort_Click(object? sender, EventArgs e)
    {
        btnPort.Enabled = false;
        tboxProgress.Clear();
        Backport.OnProgressLogged += Backport_OnProgressLogged;
        var version = Backport.GetSupportedVersions().First(x => x.Name == cbVersion.SelectedItem.ToString()!).Id;
        var resolution = resolutions[cbResolution.SelectedItem.ToString()!];
        var sourceFile = tboxSourceFile.Text;
        var targetDirectory = tboxOutput.Text;
        await Task.Run(() => Backport.CreateTexturePack(
            sourceFile,
            targetDirectory,
            version,
            resolution,
            new[]
            {
                (0, Properties.Resources.terrain),
                (1, Properties.Resources.items)
            },
            new[]
            {
                (0, @"backport\terrain.png"),
                (1, @"backport\gui\items.png"),
                (2, @"backport\mob"),
                (3, @"backport\armor"),
                (4, @"backport")
            }
            )).ContinueWith(x =>
            {
                Invoke(new MethodInvoker(() =>
                {
                    btnPort.Enabled = true;
                    MessageBox.Show(
                        "Texture pack generated successfully!",
                        "Done",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }));
            });
    }

    private void Backport_OnProgressLogged(LogLevel level, string entry)
    {
        Invoke(new MethodInvoker(() =>
        {
            if (logLevels[cbLogLevel.SelectedItem.ToString()!] == level)
                tboxProgress.AppendText($"[{level}]: {entry}{Environment.NewLine}");
        }));
    }
}