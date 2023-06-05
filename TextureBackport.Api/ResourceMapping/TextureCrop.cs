using System.Drawing;
using System.Xml.Serialization;

namespace TextureBackport.Api.ResourceMapping;

public class TextureCrop
{
    [XmlAttribute("sourceFileName")]
    public string SourceFileName { get; set; }

    [XmlAttribute("sourceCropX")]
    public int SourceCropX { get; set; }

    [XmlAttribute("sourceCropY")]
    public int SourceCropY { get; set; }

    [XmlAttribute("sourceCropWidth")]
    public int SourceCropWidth { get; set; }

    [XmlAttribute("sourceCropHeight")]
    public int SourceCropHeight { get; set; }

    [XmlAttribute("targetX")]
    public int TargetX { get; set; }

    [XmlAttribute("targetY")]
    public int TargetY { get; set; }

    [XmlAttribute("targetWidth")]
    public int TargetWidth { get; set; }

    [XmlAttribute("targetHeight")]
    public int TargetHeight { get; set; }

    [XmlAttribute("rotateFlipType")]
    public int RotateFlipType { get; set; }

    public TextureCrop() 
    { 
        SourceCropX = 0;
        SourceCropY = 0;
        SourceCropWidth = 16;
        SourceCropHeight = 16;
        TargetX = 0;
        TargetY = 0;
        TargetWidth = 16;
        TargetHeight = 16;
        RotateFlipType = 0;
    }

    public TextureCrop(string sourceFileName, Rectangle sourceCrop, Rectangle targetRect, RotateFlipType rotateFlipType=System.Drawing.RotateFlipType.RotateNoneFlipNone)
    {
        SourceFileName = sourceFileName;
        SourceCropX = sourceCrop.X;
        SourceCropY = sourceCrop.Y;
        SourceCropWidth = sourceCrop.Width;
        SourceCropHeight = sourceCrop.Height;
        TargetX = targetRect.X;
        TargetY = targetRect.Y;
        TargetWidth = targetRect.Width;
        TargetHeight = targetRect.Height;
        RotateFlipType = (int)rotateFlipType;
    }
}
