using System.Drawing;

namespace TextureBackport.Api.Textures;

public class TextureSource
{
    private Dictionary<Version, string> sources;
    
    public string? this[Version v] => !sources.ContainsKey(v) ? null : sources[v];

    public TextureSource()
    {
        sources = new Dictionary<Version, string>();
    }
    
    public TextureSource FromTo(string sourceFile, Version vFrom=Version.V18X, Version vTo=Version.V119X)
    {
        if (!sourceFile.EndsWith(".png")) sourceFile += ".png";
        for (int v = (int)vFrom; v <= (int)vTo; v++)
            sources[(Version)v] = sourceFile;
        
        return this;
    }
}