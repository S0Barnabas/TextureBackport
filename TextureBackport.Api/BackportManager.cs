using System.IO.Compression;
using System.Text;
using TextureBackport.Api.ImageFraming;
using TextureBackport.Api.ImageProcessing;
using TextureBackport.Api.ImageProcessing.Streams;
using TextureBackport.Api.Logging;
using TextureBackport.Api.TextureSources;

namespace TextureBackport.Api;

public class BackportManager
{
    private ImageProcessor _imgProc;

    public ILogger Logger { get; }

    private ResourceStream resourceStream;

    public BackportManager(string resourceMap, ILogger logger)
    {
        resourceStream = ResourceStream.FromXml(resourceMap);
        Logger = logger;
        _imgProc = new ImageProcessor(Logger);
    }

    public List<string> GetVersionNames()
    {
        return resourceStream.GameVersionIds.Select(id => GameVersion.GetVersionName(id)).ToList();
    }

    public void Start(string inputFile, string outputDirectory, int versionId, int upscaleMultiplier)
    {
        TextureSourceManager.TextureSourcePath = inputFile;
        cleanup();
        foreach (var collection in resourceStream.StreamCollections)
        {
            collection.Options.GameVersionId = versionId;
            collection.Options.UpscaleMultiplier = upscaleMultiplier;
            Directory.CreateDirectory(Path.Combine("backport", collection.DestinationDirectory));
            foreach (var collectionItem in collection.Streams)
            {
                collection.Options.TemplateBitmapPath = string.Empty;
                if (!string.IsNullOrEmpty(collectionItem.TemplateBitmapPath))
                    collection.Options.TemplateBitmapPath = collectionItem.TemplateBitmapPath;//Path.Combine("template", collectionItem.TemplateBitmapPath);
                collection.Options.DestinationBitmapPath = Path.Combine("backport", collection.DestinationDirectory, collectionItem.Stream.DestinationFile);
                var plId = _imgProc.OpenPipeline(collection.Options);
                _imgProc.PushStream(plId, collectionItem.Stream);
                _imgProc.ClosePipeline(plId);
            }
        }
        compressOutput(outputDirectory, Path.GetFileName(inputFile));
        cleanup();
    }

    private void compressOutput(string outDir, string fileName)
    {
        var sb = new StringBuilder(fileName).Insert(0, "backport_");
        while (File.Exists(Path.Combine(outDir, sb.ToString()))) sb.Insert(8, "_");
        ZipFile.CreateFromDirectory("backport", Path.Combine(outDir, sb.ToString()));
    }

    private void cleanup()
    {
        if (Directory.Exists("backport")) Directory.Delete("backport", true);
        TextureSourceManager.DeleteResource("b1.7.3.zip");
    }
}