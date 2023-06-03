using System.Text.Json.Serialization;

namespace TextureBackport.Api.Textures;

public class AtlasTexture
{
    public int X { get; }
    public int Y { get; }
    public TextureSource Sources { get; }

    public AtlasTexture(int x, int y)
    {
        X = x;
        Y = y;
        Sources = new TextureSource();
    }
}