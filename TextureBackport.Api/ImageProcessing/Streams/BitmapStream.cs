using System.Drawing;
using System.Xml.Serialization;
using TextureBackport.Api.Geometry;
using TextureBackport.Api.ImageFraming;

namespace TextureBackport.Api.ImageProcessing.Streams;

public class BitmapStream
{
    [XmlElement("SourceDirectory")]
    public List<string> SourceDirectories { get; set; }
    
    [XmlAttribute]
    public string DestinationFile { get; set; }
    
    [XmlElement("BitmapFrame")]
    public List<BitmapFrame> Frames { get; set; }

    private BitmapFrame newFrame;

    public BitmapStream()
    {
        Frames = new List<BitmapFrame>();
        SourceDirectories = new List<string>();
    }
    
    public BitmapStream(string dstFile, params string[] srcDirectories)
    {
        DestinationFile = dstFile;
        if (!dstFile.EndsWith(".png")) DestinationFile += ".png";
        SourceDirectories = new List<string>(srcDirectories);
        newFrame = new BitmapFrame();
        Frames = new List<BitmapFrame>();
    }
    
    public BitmapStream TileCoords(int top, int left)
    {
        newFrame!.TileCoords = new Point(left, top);
       return this;
    }

    public BitmapStream FromTo(int minVersion, int maxVersion)
    {
        newFrame.MinVersionId = minVersion;
        newFrame.MaxVersionId = maxVersion;
        return this;
    }

    public BitmapStream From(int minVersion) => FromTo(minVersion, 0);

    public BitmapStream To(int maxVersion) => FromTo(0, maxVersion);

    public BitmapStream SrcRect(XmlRectangle srcRect)
    {
        newFrame.SrcRect = srcRect;
        return this;
    }
    
    public BitmapStream DstRect(XmlRectangle dstRect)
    {
        newFrame.DstRect = dstRect;
        return this;
    }

    public BitmapStream SrcPath(string srcPath)
    {
        if (!srcPath.EndsWith(".png")) srcPath += ".png";
        newFrame.SrcPath = srcPath;
        return this;
    }

    public BitmapStream RotateFlipType(RotateFlipType rotateFlipType)
    {
        newFrame.RotateFlipType = rotateFlipType;
        return this;
    }

    public BitmapStream Push()
    {
        Frames.Add(new BitmapFrame()
        {
            SrcPath = newFrame.SrcPath,
            SrcRect = newFrame.SrcRect,
            DstRect = newFrame.DstRect,
            MinVersionId = newFrame.MinVersionId,
            MaxVersionId = newFrame.MaxVersionId,
            TileCoords = newFrame.TileCoords,
            RotateFlipType = newFrame.RotateFlipType
        });
        return this;
    }

    public BitmapStream Flush()
    {
        Frames.Add(new BitmapFrame()
        {
            SrcPath = newFrame.SrcPath,
            SrcRect = newFrame.SrcRect,
            DstRect = newFrame.DstRect,
            MinVersionId = newFrame.MinVersionId,
            MaxVersionId = newFrame.MaxVersionId,
            TileCoords = newFrame.TileCoords,
            RotateFlipType = newFrame.RotateFlipType
        });
        newFrame = new BitmapFrame();
        return this;
    }
}