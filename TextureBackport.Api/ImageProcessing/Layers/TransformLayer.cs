using System.Drawing;
using System.Drawing.Drawing2D;
using TextureBackport.Api.Geometry;

namespace TextureBackport.Api.ImageProcessing.Layers;

public class TransformLayer : IBitmapTransform
{
    public void Rotate(Bitmap bmp, RotateFlipType rotateFlipType)
    {
        bmp.RotateFlip(rotateFlipType);
    }

    public Bitmap Scale(Bitmap bmp, XmlSize newSize)
    {
        var resizeRect = new Rectangle(new Point(0, 0), newSize);
        var result = new Bitmap(resizeRect.Width, resizeRect.Height);
        
        using var g = Graphics.FromImage(result);
        g.InterpolationMode = InterpolationMode.NearestNeighbor;
        g.PixelOffsetMode = PixelOffsetMode.Half;
        
        g.DrawImage(bmp, resizeRect);
        return result;
    }
    
    public Bitmap Scale(Bitmap bmp, int multiplier)
    {
        return Scale(bmp, bmp.Size * multiplier);
    }
}