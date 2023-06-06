using BitmapTestProject.Resources;
using System.Diagnostics;
using System.Drawing;
using System.IO.Compression;
using TextureBackport.Api.ResourceMapping;

namespace TextureBackport.Api;

public enum LogLevel
{
    INFO,
    WARN, 
    ERROR
}

public class XmlBackport
{
    private readonly ResourceMap map;
    private TextureResolution resolution;
    private int versionId;

    private TemplateManager templateManager;

    public delegate void ProgressLogged(LogLevel level, string entry);
    public event ProgressLogged OnProgressLogged;

    public XmlBackport(string resourceMapXml)
    {
        templateManager = new TemplateManager();
        map = ResourceMap.FromXml(resourceMapXml) ?? throw new ArgumentException($"Resource map not found at: {resourceMapXml}");
    }

    public List<GameVersion> GetSupportedVersions()
    {
        return map.SupportedVersions.SupportList;
    }

    public void CreateTexturePack(string sourceFile, string targetDirectory, int versionId, TextureResolution resolution)
    {
        this.versionId = versionId;
        this.resolution = resolution;

        if (Directory.Exists("texture_source")) Directory.Delete("texture_source", true);
        if (Directory.Exists("templates")) Directory.Delete("templates", true);
        if (Directory.Exists("backport")) Directory.Delete("backport", true);

        ZipFile.ExtractToDirectory(sourceFile, "texture_source");
        templateManager.ExtractTemplate("b173", "templates");
        Directory.CreateDirectory("backport");

        for (int i = 0; i < map.TextureAtlases.Count; i++)
            drawAtlas(map.TextureAtlases[i]);
        for (int i = 0; i < map.TextureBundles.Count; i++)
            drawBundle(map.TextureBundles[i]);

        var fileNamePrefix = "backport_";
        var targetFile = Path.Combine(targetDirectory, fileNamePrefix + Path.GetFileName(sourceFile));
        while (File.Exists(targetFile))
        {
            fileNamePrefix += '_';
            targetFile = Path.Combine(targetDirectory, fileNamePrefix + Path.GetFileName(sourceFile));
        }

        ZipFile.CreateFromDirectory("backport", targetFile);

        Directory.Delete("texture_source", true);
        Directory.Delete("templates", true);
        Directory.Delete("backport", true);
    }

    private void drawAtlas(TextureAtlas atlas)
    {
        var atlasSourceDirectories = atlas.SourceDirectory.Split(',');
        var templateFile = $"templates\\{atlas.TemplateFile}.png";
        var targetFile = $"backport\\{atlas.TargetFile}.png";

        using var templateBmp = (Bitmap)Image.FromFile(templateFile);

        var result = new Bitmap((int)resolution * 16, (int)resolution * 16);
        using var g = Graphics.FromImage(result);
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

        g.DrawImage(templateBmp, 0, 0, (int)resolution * 16, (int)resolution * 16);

        drawAtlasTextures(atlas.AtlasTextures, atlasSourceDirectories, g);
        drawCompositeTextures(atlas.CompositeTextures, atlasSourceDirectories, g);

        Directory.CreateDirectory(targetFile.Remove(targetFile.LastIndexOf('\\')));
        result.Save(targetFile);
    }

    private void drawAtlasTextures(List<AtlasTexture> atlasTextures, string[] atlasSourceDirectories, Graphics g)
    {
        foreach (var texture in atlasTextures)
        {
            var tSrc = texture.TextureSources.Find(src => versionId >= src.From && (versionId <= src.To || src.To == 0));
            if (tSrc == null)
            {
                OnProgressLogged?.Invoke(LogLevel.WARN, "Texture not supported by selected version!");
                continue;
            }

            bool sourceFileFound = false;
            foreach (var sourceDirectory in atlasSourceDirectories)
            {
                var path = Path.Combine("texture_source", sourceDirectory, tSrc.SourceFileName + ".png");
                if (File.Exists(path))
                {
                    OnProgressLogged?.Invoke(LogLevel.INFO, $"Drawing atlas texture: {path}");
                    drawAtlasTexture(g, texture.X, texture.Y, texture.Width, texture.Height, path);
                    sourceFileFound = true;
                    break;
                }
            }
            if (!sourceFileFound)
                OnProgressLogged?.Invoke(LogLevel.ERROR, $"File ({tSrc.SourceFileName}) not found in any of the provided source directories!");
        }
    }

    private void drawCompositeTextures(List<CompositeTexture> compositeTextures, string[] atlasSourceDirectories, Graphics g)
    {
        foreach (var texture in compositeTextures)
        {
            var tSrc = texture.CompositeTextureSources.Find(src => versionId >= src.From && (versionId <= src.To || src.To == 0));
            if (tSrc == null)
            {
                OnProgressLogged.Invoke(LogLevel.WARN, "Texture not supported by selected version!");
                continue;
            }

            var validSourceDirectory = "";
            bool allSourceFilesFound = false;
            foreach (var sourceDirectory in atlasSourceDirectories)
            {
                foreach (var crop in tSrc.TextureCrops)
                {
                    var path = Path.Combine("texture_source", sourceDirectory, crop.SourceFileName + ".png");
                    if (!File.Exists(path)) break;
                    allSourceFilesFound = true;
                }

                if (allSourceFilesFound)
                {
                    validSourceDirectory = Path.Combine("texture_source", sourceDirectory);
                    break;
                }
            }
            if (!allSourceFilesFound)
            {
                OnProgressLogged.Invoke(LogLevel.WARN, "One or more of the source files of composite texture not found!");
                continue;
            }

            drawCompositeTexture(g, texture.X, texture.Y, tSrc, validSourceDirectory);
        }
    }

    private void drawAtlasTexture(Graphics g, int x, int y, int width, int height, string filePath)
    {
        g.SetClip(new Rectangle(x * (int)resolution, y * (int)resolution, (int)resolution, (int)resolution));
        g.Clear(Color.Transparent);
        g.ResetClip();

        using var sourceBmp = new Bitmap(filePath);
        g.DrawImage(sourceBmp, x * (int)resolution, y * (int)resolution, width * (int)resolution, height * (int)resolution);
    }

    private void drawCompositeTexture(Graphics g, int x, int y, CompositeTextureSource cts, string sourceDirectory)
    {
        g.SetClip(new Rectangle(x * (int)resolution, y * (int)resolution, (int)resolution, (int)resolution));
        g.Clear(Color.Transparent);
        g.ResetClip();

        var resMul = (int)resolution / 16;
        var result = new Bitmap((int)resolution, (int)resolution);
        using var cg = Graphics.FromImage(result);
        cg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        cg.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

        foreach (var crop in cts.TextureCrops)
        {
            var sourceFile = Path.Combine(sourceDirectory, crop.SourceFileName + ".png");
            OnProgressLogged?.Invoke(LogLevel.INFO, $"Drawing composite fragment: {sourceFile}");
            using var sourceBmp = (Bitmap)Image.FromFile(sourceFile);
            using var fragmentBmp = sourceBmp.Clone(
                new Rectangle(
                    crop.SourceCropX * resMul,
                    crop.SourceCropY * resMul,
                    crop.SourceCropWidth * resMul,
                    crop.SourceCropHeight * resMul),
                sourceBmp.PixelFormat);
            fragmentBmp.RotateFlip((RotateFlipType)crop.RotateFlipType);
            cg.DrawImage(fragmentBmp,
                crop.TargetX * resMul,
                crop.TargetY * resMul,
                crop.TargetWidth * resMul,
                crop.TargetHeight * resMul);
        }
        g.DrawImage(result, x * (int)resolution, y * (int)resolution, (int)resolution, (int)resolution);
    }

    private void drawBundle(TextureBundle bundle)
    {
        var targetDirectory = $"backport\\{bundle.TargetDirectory}";

        if (!string.IsNullOrEmpty(targetDirectory))
            Directory.CreateDirectory(targetDirectory);

        foreach (var texture in bundle.TextureFiles)
        {
            var tSrc = texture.TextureSources.Find(src => versionId >= src.From && (versionId <= src.To || src.To == 0));
            if (tSrc is null)
            {
                OnProgressLogged?.Invoke(LogLevel.WARN, "Texture not supported by selected version!");
                continue;
            }
            var sourceFile = Path.Combine("texture_source", bundle.SourceDirectory, tSrc.SourceFileName + ".png");
            if (!File.Exists(sourceFile))
            {
                OnProgressLogged?.Invoke(LogLevel.WARN, $"File not found: {sourceFile}");
                continue;
            }
            var targetFile = Path.Combine(targetDirectory, texture.TargetFile + ".png");
            OnProgressLogged?.Invoke(LogLevel.INFO, $"Drawing: {targetFile}");

            using var sourceBmp = (Bitmap)Image.FromFile(sourceFile);
            int resMul = (int)resolution / 16;
            if (texture.TargetWidth == 0 || texture.TargetHeight == 0)
                File.Copy(sourceFile, targetFile, true);
            else
            {
                using var targetBmp = sourceBmp.Clone(new Rectangle(0, 0, texture.TargetWidth * resMul, texture.TargetHeight * resMul), sourceBmp.PixelFormat);
                targetBmp.Save(targetFile);
            }
        }
    }
}
