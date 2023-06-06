using System.Xml.Serialization;
using TextureBackport.Api.ResourceMapping;

namespace BitmapTestProject.Resources;

public class TextureAtlas
{
    [XmlAttribute("sourceDirectory")]
    public string SourceDirectory { get; set; }

    [XmlAttribute("templateFile")]
    public string TemplateFile { get; set; }

    [XmlAttribute("targetFile")]
    public string TargetFile { get; set; }

    [XmlElement("AtlasTexture")]
    public List<AtlasTexture> AtlasTextures { get; set; }

    [XmlElement("CompositeTexture")]
    public List<CompositeTexture> CompositeTextures { get; set; }

    public TextureAtlas()
    {
        AtlasTextures = new List<AtlasTexture>();
        CompositeTextures = new List<CompositeTexture>();
    }

    public TextureAtlas(string sourceDirectory, string templateFile, string targetFile)
    {
        SourceDirectory = sourceDirectory;
        TemplateFile = templateFile;
        TargetFile = targetFile;
        AtlasTextures = new List<AtlasTexture>();
        CompositeTextures = new List<CompositeTexture>();
    }

    public AtlasTexture AddTexture(int top, int left, int width=1, int height=1)
    {
        var texture = new AtlasTexture(left, top, width, height);
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