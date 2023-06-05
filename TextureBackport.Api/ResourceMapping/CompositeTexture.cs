using System.Drawing;
using System.Xml.Serialization;

namespace TextureBackport.Api.ResourceMapping;

public class CompositeTexture
{
    [XmlAttribute("x")]
    public int X { get; set; }

    [XmlAttribute("y")]
    public int Y { get; set; }

    [XmlElement("CompositeTextureSource")]
    public List<CompositeTextureSource> CompositeTextureSources { get; set; }

    public CompositeTexture()
    {
        CompositeTextureSources = new List<CompositeTextureSource>();
    }

    public CompositeTexture(int x, int y)
    {
        X = x;
        Y = y;
        CompositeTextureSources = new List<CompositeTextureSource>();
    }

    public CompositeTextureSource From(GameVersion from)
    {
        var compositeTextureSource = new CompositeTextureSource(from.Id, 0);
        CompositeTextureSources.Add(compositeTextureSource);
        return compositeTextureSource;
    }

    public CompositeTextureSource To(GameVersion to)
    {
        var compositeTextureSource = new CompositeTextureSource(0, to.Id);
        CompositeTextureSources.Add(compositeTextureSource);
        return compositeTextureSource;
    }

    public CompositeTextureSource FromTo(GameVersion from, GameVersion to)
    {
        var compositeTextureSource = new CompositeTextureSource(from.Id, to.Id);
        CompositeTextureSources.Add(compositeTextureSource);
        return compositeTextureSource;
    }
}
