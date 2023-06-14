using System.Drawing;
using System.Xml.Serialization;
using TextureBackport.Api.Geometry;

namespace TextureBackport.Api.ImageFraming;

public class BitmapFrame
{
    [XmlAttribute]
    public string SrcPath { get; set; }

    [XmlAttribute]
    public int MinVersionId { get; set; }
    
    [XmlAttribute]
    public int MaxVersionId { get; set; }

    [XmlElement]
    public XmlPoint TileCoords { get; set; }
    
    [XmlElement]
    public XmlRectangle SrcRect { get; set; }

    [XmlElement]
    public XmlRectangle DstRect { get; set; }
    
    [XmlElement]
    public RotateFlipType RotateFlipType { get; set; }

    public Bitmap Bitmap { get; set; }

    public BitmapFrame(string srcPath, int minVersionId, int maxVersionId)
    {
        SrcPath = srcPath;
        MinVersionId = minVersionId;
        MaxVersionId = maxVersionId;
        TileCoords = new Point(-1, -1);
    }

    public BitmapFrame()
    {
        TileCoords = new Point(-1, -1);
    }
}