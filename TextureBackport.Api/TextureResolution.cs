using System.Globalization;
using System.Text.RegularExpressions;

namespace TextureBackport.Api;

public static class TextureResolution
{
    public static int GetUpscaleMultiplier(string resName)
    {
        if (!Regex.IsMatch(resName, "X[0-9]*", RegexOptions.IgnoreCase))
            throw new ArgumentException($"Invalid resolution: {resName}");
        var res = int.Parse(resName.Replace("x", "", true, CultureInfo.InvariantCulture));
        var mul = res / 16;
        if (mul != 0 && (mul & (mul - 1)) == 0)
            return mul;
        throw new ArgumentException($"Invalid resolution: {resName}");
    }

    public static string GetResolutionName(int upscaleMultiplier)
    {
        if (upscaleMultiplier != 0 && (upscaleMultiplier & (upscaleMultiplier - 1)) == 0)
            return $"X{upscaleMultiplier * 16}";
        throw new ArgumentException($"Invalid upscale multiplier: {upscaleMultiplier}");
    }
}