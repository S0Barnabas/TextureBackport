using System.Drawing;
using System.Xml.Serialization;

namespace TextureBackport.Api.Geometry;

public struct XmlRectangle
{
    [XmlAttribute]
    public int X { get; set; }
    
    [XmlAttribute]
    public int Y { get; set; }
    
    [XmlAttribute]
    public int Width { get; set; }
    
    [XmlAttribute]
    public int Height { get; set; }

    public static XmlRectangle Empty => new();

    [XmlIgnore]
    public XmlPoint Location
    {
        get => new(X, Y);
        set
        {
            X = value.X;
            Y = value.Y;
        }
    }

    public XmlSize Size => new(Width, Height);
    
    public XmlRectangle()
    {
        X = 0;
        Y = 0;
        Width = 0;
        Height = 0;
    }

    public XmlRectangle(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public static XmlRectangle operator *(XmlRectangle r, int m) => new(r.X * m, r.Y * m, r.Width * m, r.Height * m);

    public static bool operator ==(XmlRectangle a, XmlRectangle b) => a.X == b.X && a.Y == b.Y && a.Width == b.Width && a.Height == b.Height;
    public static bool operator !=(XmlRectangle a, XmlRectangle b) => a.X != b.X || a.Y != b.Y || a.Width != b.Width || a.Height != b.Height;
    
    public static implicit operator Rectangle(XmlRectangle r) => new(r.X, r.Y, r.Width, r.Height);
    public static implicit operator XmlRectangle(Rectangle r) => new(r.X, r.Y, r.Width, r.Height);
}