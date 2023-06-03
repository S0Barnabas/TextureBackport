using System.Drawing;

namespace TextureBackport.Api.Textures;

public class TextureFile
{
    public string TargetFile { get; }
    
    public Size TargetSize { get; }

    public TextureSource Sources { get; }
    
    public TextureFile(string targetFile, Size targetSize)
    {
        TargetFile = targetFile;
        TargetSize = targetSize;
        Sources = new TextureSource();
    }
}