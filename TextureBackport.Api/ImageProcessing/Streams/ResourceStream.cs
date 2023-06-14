using System.Xml.Serialization;
using TextureBackport.Api.ImageFraming;
using TextureBackport.Api.ImageProcessing.Pipelines;

namespace TextureBackport.Api.ImageProcessing.Streams;

[XmlRoot]
public class ResourceStream
{
    [XmlElement("StreamCollection")]
    public List<StreamCollection> StreamCollections { get; set; }

    [XmlElement("GameVersionId")]
    public List<int> GameVersionIds { get; set; }
    
    public ResourceStream()
    {
        StreamCollections = new List<StreamCollection>();
        GameVersionIds = new List<int>();
    }

    public void AddVersionIdByName(string name)
    {
        GameVersionIds.Add(GameVersion.GetVersionId(name));
    }
    
    public StreamCollection CreateStreamCollection(string dstDir, PipelineOptions options)
    {
        var sc = new StreamCollection(dstDir, options);
        StreamCollections.Add(sc);
        return sc;
    }
    
    public void XmlSerialize(string path)
    {
        var xml = new XmlSerializer(typeof(ResourceStream));
        using var sw = new StreamWriter(path);
        xml.Serialize(sw, this);
    }

    public static ResourceStream FromXml(string path)
    {
        var xml = new XmlSerializer(typeof(ResourceStream));
        using var sr = new StreamReader(path);
        return (ResourceStream)xml.Deserialize(sr)!;
    }
}