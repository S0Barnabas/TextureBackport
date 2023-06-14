using System.Drawing;
using TextureBackport.Api.Geometry;

namespace TextureBackport.Api.ImageProcessing.Layers;

public class WriteLayer : IBitmapWriter
{
    public void DrawBitmap(Bitmap bmp, Graphics target, XmlPoint pos, bool clear=true)
    {
        DrawBitmap(bmp, target, new Rectangle(pos, bmp.Size), clear);
    }

    public void DrawBitmap(Bitmap bmp, Graphics target, XmlRectangle rect, bool clear=true)
    {
        if (clear)
        {
            target.SetClip(rect);
            target.Clear(Color.Transparent);
            target.ResetClip();
        }
        target.DrawImage(bmp, rect);
    }

    public void DrawToFile(Bitmap bmp, string path)
    {
        bmp.Save(path);
    }
}