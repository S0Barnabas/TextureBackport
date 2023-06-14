using System.Drawing;
using TextureBackport.Api.Geometry;

namespace TextureBackport.Api.ImageProcessing.Layers;

public interface IBitmapReader
{
    Bitmap GetBitmap(string path, bool embeddedResource=false);

    Bitmap GetBitmapFragment(string path, XmlRectangle rect, bool embeddedResource=false);
}