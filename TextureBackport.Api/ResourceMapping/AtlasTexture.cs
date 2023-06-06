using System.Xml.Serialization;
using TextureBackport.Api.ResourceMapping;

namespace BitmapTestProject.Resources;

public class AtlasTexture
{
    [XmlAttribute("x")]
    public int X { get; set; }
    
    [XmlAttribute("y")]
    public int Y { get; set; }

    [XmlAttribute("width")]
    public int Width { get; set; }

    [XmlAttribute("height")]
    public int Height { get; set; }

    [XmlElement("TextureSource")]
    public List<TextureSource> TextureSources { get; set; }

    public AtlasTexture()
    {
        TextureSources = new List<TextureSource>();
    }

    public AtlasTexture(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
        TextureSources = new List<TextureSource>();
    }
    
    public void All(string sourceFileName)
    {
        TextureSources.Add(new TextureSource(sourceFileName, 0, 0));
    }

    public void From(GameVersion from, string sourceFileName)
    {
        TextureSources.Add(new TextureSource(sourceFileName, from.Id, 0));
    }

    public AtlasTexture To(GameVersion to, string sourceFileName)
    {
        var textureSource =
            new TextureSource(sourceFileName, 0, to.Id);
        TextureSources.Add(textureSource);
        return this;
    }

    public AtlasTexture FromTo(GameVersion from, GameVersion to, string sourceFileName)
    {
        var textureSource = new TextureSource(sourceFileName, from.Id, to.Id);
        TextureSources.Add(textureSource);
        return this;
    }
}