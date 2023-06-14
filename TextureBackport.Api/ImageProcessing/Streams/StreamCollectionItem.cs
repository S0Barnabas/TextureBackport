using System.Xml.Serialization;

namespace TextureBackport.Api.ImageProcessing.Streams;

public class StreamCollectionItem
{
    [XmlAttribute]
    public string TemplateBitmapPath { get; set; }
    
    [XmlElement("BitmapStream")]
    public BitmapStream Stream { get; set; }

    public StreamCollectionItem() { }
    
    public StreamCollectionItem(BitmapStream stream)
    {
        TemplateBitmapPath = string.Empty;
        Stream = stream;
    }
    
    public StreamCollectionItem(string templateBitmapPath, BitmapStream stream)
    {
        TemplateBitmapPath = templateBitmapPath;
        if (!TemplateBitmapPath.EndsWith(".png")) TemplateBitmapPath += ".png";
        Stream = stream;
    }
}