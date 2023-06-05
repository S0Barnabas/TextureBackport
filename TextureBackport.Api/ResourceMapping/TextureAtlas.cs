using System.Xml.Serialization;
using TextureBackport.Api.ResourceMapping;

namespace BitmapTestProject.Resources;

public class TextureAtlas
{
    [XmlAttribute("sourceDirectory")]
    public string SourceDirectory { get; set; }
    
    [XmlElement("AtlasTexture")]
    public List<AtlasTexture> AtlasTextures { get; set; }

    [XmlElement("CompositeTexture")]
    public List<CompositeTexture> CompositeTextures { get; set; }

    public TextureAtlas()
    {
        AtlasTextures = new List<AtlasTexture>();
        CompositeTextures = new List<CompositeTexture>();
    }

    public TextureAtlas(string sourceDirectory)
    {
        SourceDirectory = sourceDirectory;
        AtlasTextures = new List<AtlasTexture>();
        CompositeTextures = new List<CompositeTexture>();
    }

    public AtlasTexture AddTexture(int top, int left)
    {
        var texture = new AtlasTexture(left, top);
        AtlasTextures.Add(texture);
        return texture;
    }

    public CompositeTexture AddComposite(int top, int left)
    {
        var texture = new CompositeTexture(left, top);
        CompositeTextures.Add(texture);
        return texture;
    }
}