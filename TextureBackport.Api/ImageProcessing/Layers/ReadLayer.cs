using System.Drawing;
using TextureBackport.Api.Geometry;
using TextureBackport.Api.TextureSources;

namespace TextureBackport.Api.ImageProcessing.Layers;

public class ReadLayer : IBitmapReader
{
    public Bitmap GetBitmap(string path, bool embeddedResource=false)
    {
        var filePath = embeddedResource ? "b1.7.3.zip" : TextureSourceManager.TextureSourcePath;
        return TextureSourceManager.GetImage(filePath, path, embeddedResource);
    }

    public Bitmap GetBitmapFragment(string path, XmlRectangle rect, bool embeddedResource=false)
    {
        //var src = (Bitmap)Image.FromFile(path);
        var src = GetBitmap(path, embeddedResource);
        return src.Clone(rect, src.PixelFormat);
    }
}