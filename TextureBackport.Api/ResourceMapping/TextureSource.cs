using System.Xml.Serialization;

namespace BitmapTestProject.Resources;

public class TextureSource
{
    [XmlAttribute(AttributeName = "sourceFileName")]
    public string SourceFileName { get; set; }
    
    [XmlAttribute(AttributeName = "vFrom")]
    public int From { get; set; }
    
    [XmlAttribute( AttributeName = "vTo")]
    public int To { get; set; }

    public TextureSource() { }

    public TextureSource(string sourceFileName, int from, int to)
    {
        SourceFileName = sourceFileName;
        From = from;
        To = to;
    }
}