using System.Drawing;
using System.Xml.Serialization;

namespace TextureBackport.Api.Geometry;

public struct XmlPoint
{
    [XmlAttribute]
    public int X { get; set; }
    
    [XmlAttribute]
    public int Y { get; set; }

    public static XmlPoint Empty => new();
    
    public XmlPoint()
    {
        X = 0;
        Y = 0;
    }
    
    public XmlPoint(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static XmlPoint operator *(XmlPoint p, int m) => new(p.X * m, p.Y * m);
    public static XmlPoint operator +(XmlPoint p1, XmlPoint p2) => new(p1.X + p2.X, p1.Y + p2.Y);
    
    public static bool operator ==(XmlPoint a, XmlPoint b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(XmlPoint a, XmlPoint b) => a.X != b.X || a.Y != b.Y;
    
    public static implicit operator Point(XmlPoint p) => new(p.X, p.Y);
    public static implicit operator XmlPoint(Point p) => new(p.X, p.Y);
}