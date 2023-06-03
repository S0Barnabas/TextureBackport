using System.Drawing;

namespace TextureBackport.Api.Textures;

public class TextureBundle
{
    private readonly TextureResolution resolution;

    private Size textureSize => new((int)resolution * 4, (int)resolution * 2);
    
    private readonly List<TextureFile> files;

    public int FileCount => files.Count;

    private int progress;
    public int Progress
    {
        get => progress;
        private set
        {
            progress = value;
            OnProgressChanged?.Invoke(progress);
        }
    }

    public delegate void ProgressChanged(int progress);
    public event ProgressChanged? OnProgressChanged;

    public TextureBundle(TextureResolution resolution)
    {
        this.resolution = resolution;
        files = new List<TextureFile>();
    }

    public TextureSource AddTexture(string targetFile)
    {
        if (!targetFile.EndsWith(".png")) targetFile += ".png";
        
        var texture = new TextureFile(targetFile, textureSize);
        files.Add(texture);
        return texture.Sources;
    }

    public void DrawBundle(Version v, string sourceDirectory, string targetDirectory)
    {
        Directory.CreateDirectory(targetDirectory);
        foreach (var texture in files)
        {
            if (texture.Sources[v] is null)
            {
                Console.WriteLine("Texture not supported by selected version!");
                continue;
            }
            var sourceFile = Path.Combine(sourceDirectory, texture.Sources[v]!);
            if (!File.Exists(sourceFile))
            {
                Console.WriteLine("File not found: {0}", sourceFile);
                continue;
            }

            var targetFile = Path.Combine(targetDirectory, texture.TargetFile);

            using var sourceBmp = (Bitmap)Image.FromFile(sourceFile);
            if (sourceBmp.Size == texture.TargetSize)
                File.Copy(sourceFile, targetFile, true);
            else
            {
                using var targetBmp = sourceBmp.Clone(new Rectangle(new Point(0, 0), texture.TargetSize), sourceBmp.PixelFormat);
                targetBmp.Save(targetFile);
            }
            Progress++;
        }
    }
}