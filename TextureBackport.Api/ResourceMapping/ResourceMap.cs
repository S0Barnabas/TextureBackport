using System.Xml.Serialization;
using TextureBackport.Api.ResourceMapping;

namespace BitmapTestProject.Resources;

[XmlRoot(ElementName = "ResourceMap")]
public class ResourceMap
{
    [XmlElement("TextureAtlas")]
    public List<TextureAtlas> TextureAtlases { get; set; }

    [XmlElement("TextureBundle")]
    public List<TextureBundle> TextureBundles { get; set; }

    [XmlElement("SupportedVersions")]
    public SupportedVersions SupportedVersions { get; set; }

    public int? this[string name]
    {
        get
        {
            var v = SupportedVersions.SupportList.Find(x => x.Name == name)?.Id;
            if (v == null) throw new ArgumentException($"Support list does not contain version: {name}");
            return v;
        }
    }

    public GameVersion this[int id]
    {
        get
        {
            GameVersion? version = null;
            if (id == 0)
                version = SupportedVersions.SupportList.Find(x => x.Id == SupportedVersions.SupportList.Min(y => y.Id));
            else
                version = SupportedVersions.SupportList.Find(x => x.Id == id);
            if (version == null) throw new ArgumentException($"Support list does not contain version: {id}");
            return version;
        }
    }

    public ResourceMap()
    {
        TextureAtlases = new List<TextureAtlas>();
        TextureBundles = new List<TextureBundle>();
        SupportedVersions = new SupportedVersions();
    }

    public void SetSupportedVersions(List<GameVersion> gameVersions)
    {
        SupportedVersions.SupportList = gameVersions;
    }

    public TextureAtlas AddTextureAtlas(string sourceDirectory)
    {
        var atlas = new TextureAtlas(sourceDirectory);
        TextureAtlases.Add(atlas);
        return atlas;
    }

    public TextureBundle AddTextureBundle(string sourceDirectory)
    {
        var bundle = new TextureBundle(sourceDirectory);
        TextureBundles.Add(bundle);
        return bundle;
    }

    public void XmlSerialize(string file)
    {
        var s = new XmlSerializer(typeof(ResourceMap));
        using var sw = new StreamWriter(file);
        s.Serialize(sw, this);
    }

    public static ResourceMap? FromXml(string file)
    {
        var s = new XmlSerializer(typeof(ResourceMap));
        using var sr = new StreamReader(file);
        return (ResourceMap?)s.Deserialize(sr);
    }
}