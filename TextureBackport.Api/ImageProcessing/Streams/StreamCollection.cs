using System.Xml.Serialization;
using TextureBackport.Api.ImageProcessing.Pipelines;

namespace TextureBackport.Api.ImageProcessing.Streams;

public class StreamCollection
{
    [XmlAttribute]
    public string DestinationDirectory { get; set; }
    
    [XmlElement("StreamCollectionItem")]
    public List<StreamCollectionItem> Streams { get; set; }

    [XmlElement]
    public PipelineOptions Options { get; set; }

    public StreamCollection()
    {
        Streams = new List<StreamCollectionItem>();
    }
    
    public StreamCollection(string dstDir, PipelineOptions options)
    {
        DestinationDirectory = dstDir;
        Options = options;
        Streams = new List<StreamCollectionItem>();
    }

    public BitmapStream CreateStream(string dstFile, string[] srcDirs)
    {
        var stream = new BitmapStream(dstFile, srcDirs);
        Streams.Add(new StreamCollectionItem(stream));
        return stream;
    }
    
    public BitmapStream CreateStream(string templateBitmapPath, string dstFile, string[] srcDirs)
    {
        var stream = new BitmapStream(dstFile, srcDirs);
        Streams.Add(new StreamCollectionItem(templateBitmapPath, stream));
        return stream;
    }
}