using System.Drawing;

namespace TextureBackport.Api.Textures;

public class TextureAtlas
{
    private readonly TextureResolution resolution;

    private Size atlasSize => new((int)resolution * 16, (int)resolution * 16);
    
    private readonly List<AtlasTexture> textures;

    public int TextureCount => textures.Count;

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

    public TextureAtlas(TextureResolution resolution)
    {
        this.resolution = resolution;
        textures = new List<AtlasTexture>();
    }

    public TextureSource AddTexture(int top, int left)
    {
        var texture = new AtlasTexture(left, top);
        textures.Add(texture);
        return texture.Sources;
    }

    public TextureSource AddComposite(int top, int left, string sourceDirectory, Func<string, TextureResolution, Version, Bitmap> getComposite)
    {
        var texture = new CompositeTexture(left, top, sourceDirectory, getComposite);
        textures.Add(texture);
        return texture.Sources;
    }

    public void DrawAtlas(Version v, string sourceDirectory, string targetFile, string templateAtlas)
    {
        DrawAtlas(v, sourceDirectory, targetFile, (Bitmap)Image.FromFile(templateAtlas));
    }

    public void DrawAtlas(Version v, string sourceDirectory, string targetFile, Bitmap templateAtlas)
    {
        var result = new Bitmap(atlasSize.Width, atlasSize.Height);
        using var g = Graphics.FromImage(result);
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
        g.DrawImage(templateAtlas, 0, 0, atlasSize.Width, atlasSize.Height);

        foreach (var t in textures)
        {
            var tSource = t.Sources[v];
            if (tSource is null)
            {
                Console.WriteLine("Texture not supported by selected version!");
                continue;
            }
            tSource = Path.Combine(t is CompositeTexture ? ((CompositeTexture)t).SourceDirectory : sourceDirectory , t.Sources[v]!);
            if (!File.Exists(tSource))
            {
                Console.WriteLine("File not found: {0}", tSource);
                continue;
            }

            g.SetClip(new Rectangle(t.X * (int)resolution, t.Y * (int)resolution, (int)resolution, (int)resolution));
            g.Clear(Color.Transparent);
            g.ResetClip();
            Console.WriteLine("Drawing: {0}", t.Sources[v]);
            using var sourceBmp = t is CompositeTexture ? ((CompositeTexture)t).GetComposite(resolution, v) : new Bitmap(tSource);
            g.DrawImage(sourceBmp, t.X * (int)resolution, t.Y * (int)resolution, (int)resolution, (int)resolution);
            Progress++;
        }
        if (targetFile.Contains('\\') && !Directory.Exists(targetFile.Remove(targetFile.LastIndexOf('\\'))))
            Directory.CreateDirectory(targetFile.Remove(targetFile.LastIndexOf('\\')));
        result.Save(targetFile);
    }
}