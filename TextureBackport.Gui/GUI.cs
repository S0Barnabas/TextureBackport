using TextureBackport.Api;
using TextureBackport.Api.ImageFraming;
using TextureBackport.Api.Logging;

namespace TextureBackport.Gui;

public partial class GUI : Form
{
    public List<string> resolutions;
    public Dictionary<string, LogLevel> logLevels;

    public BackportManager Backport;

    public ILogger Logger;

    public GUI()
    {
        InitializeComponent();
        //Backport = new XmlBackport(@"resource_maps\default.xml");
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        CenterToScreen();

        Logger = new Logger();

        resolutions = new List<string>();
        for (int i = 1; i <= 32; i *= 2)
            resolutions.Add(TextureResolution.GetResolutionName(i));

        logLevels = new Dictionary<string, LogLevel>()
        {
            ["Information"] = LogLevel.INFO,
            ["Warning"] = LogLevel.WARN,
            ["Error"] = LogLevel.ERROR
        };
        Load += GUI_Load;
        Logger.OnLogged += Backport_OnProgressLogged;
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
        Backport = new BackportManager(cbResourceMap.SelectedItem.ToString()!, Logger);
        foreach (var name in Backport.GetVersionNames())
            cbVersion.Items.Add(name);
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
            foreach (var name in Backport.GetVersionNames())
                cbVersion.Items.Add(name);
            cbVersion.SelectedIndex = 0;
        }

        foreach (var name in resolutions)
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
        var version = GameVersion.GetVersionId(cbVersion.SelectedItem.ToString()!);
        var upscaleMultiplier = TextureResolution.GetUpscaleMultiplier(cbResolution.SelectedItem.ToString()!);
        var sourceFile = tboxSourceFile.Text;
        var outputDirectory = tboxOutput.Text;
        await Task.Run(
            () => Backport.Start(sourceFile, outputDirectory, version, upscaleMultiplier))
            .ContinueWith(x =>
            {
                Invoke(new MethodInvoker(() =>
                {
                    Backport = new BackportManager(cbResourceMap.SelectedItem.ToString()!, Logger);
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
            if (logLevels[cbLogLevel.SelectedItem.ToString()!] <= level)
                tboxProgress.AppendText($"[{level}]: {entry}{Environment.NewLine}");
        }));
    }
}