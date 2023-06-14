using System.Drawing;
using System.Xml.Serialization;

namespace TextureBackport.Api.Geometry;

public struct XmlSize
{
    [XmlAttribute]
    public int Width { get; set; }
    
    [XmlAttribute]
    public int Height { get; set; }

    public static XmlSize Empty => new();
    
    public XmlSize()
    {
        Width = 0;
        Height = 0;
    }

    public XmlSize(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public static XmlSize operator *(XmlSize s, int m) => new(s.Width * m, s.Height * m);
    
    public static bool operator ==(XmlSize a, XmlSize b) => a.Width == b.Width && a.Height == b.Height;
    public static bool operator !=(XmlSize a, XmlSize b) => a.Width != b.Width || a.Height != b.Height;
    
    public static implicit operator Size(XmlSize s) => new(s.Width, s.Height);
    public static implicit operator XmlSize(Size s) => new(s.Width, s.Height);
}