using System.Drawing;
using TextureBackport.Api.Geometry;

namespace TextureBackport.Api.ImageProcessing.Layers;

public interface IBitmapTransform
{
    void Rotate(Bitmap bmp, RotateFlipType rotateFlipType);

    Bitmap Scale(Bitmap bmp, int multiplier);

    Bitmap Scale(Bitmap bmp, XmlSize newSize);
}