using System.Drawing;

namespace TextureBackport.Api.Textures;

public class CompositeTexture : AtlasTexture
{
    private Func<string, TextureResolution, Version, Bitmap> getComposite { get; }

    public string SourceDirectory { get; }

    public CompositeTexture(int x, int y, string sourceDirectory, Func<string, TextureResolution, Version, Bitmap> getComposite) : base(x, y)
    {
        this.getComposite = getComposite;
        this.SourceDirectory = sourceDirectory;
    }

    public Bitmap GetComposite(TextureResolution resolution, Version version)
    {
        var sourceFile = Path.Combine(SourceDirectory, Sources[version]!);
        return getComposite(sourceFile, resolution, version);
    }
}
