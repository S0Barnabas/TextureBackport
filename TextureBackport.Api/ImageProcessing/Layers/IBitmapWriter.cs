using System.Drawing;
using TextureBackport.Api.Geometry;

namespace TextureBackport.Api.ImageProcessing.Layers;

public interface IBitmapWriter
{
    void DrawBitmap(Bitmap bmp, Graphics target, XmlPoint pos, bool clear);

    void DrawBitmap(Bitmap bmp, Graphics target, XmlRectangle rect, bool clear);
    
    void DrawToFile(Bitmap bmp, string path);
}