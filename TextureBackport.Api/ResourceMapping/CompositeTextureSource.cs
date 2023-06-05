using System.Drawing;
using System.Xml.Serialization;

namespace TextureBackport.Api.ResourceMapping;

public class CompositeTextureSource
{
    [XmlAttribute("vFrom")]
    public int From { get; set; }

    [XmlAttribute("vTo")]
    public int To { get; set; }

    [XmlElement("TextureCrop")]
    public List<TextureCrop> TextureCrops { get; set; }
    
    public CompositeTextureSource() 
    {
        TextureCrops = new List<TextureCrop>();
    }

    public CompositeTextureSource(int from, int to)
    {
        From = from;
        To = to;
        TextureCrops = new List<TextureCrop>();
    }

    public CompositeTextureSource AddCrop(TextureCrop crop)
    {
        TextureCrops.Add(crop);
        return this;
    }
}
