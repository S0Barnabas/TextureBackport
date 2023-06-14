using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace TextureBackport.Api.ImageFraming;

public static class GameVersion
{
    public static int GetVersionId(string versionName)
    {
        if (!Regex.IsMatch(versionName, "^1\\.([8-9]|[1-2][0-9])\\.X$", RegexOptions.IgnoreCase))
            throw new ArgumentException($"Invalid version:{versionName}");
        return int.Parse(versionName.Replace(".", "").Replace("x", "", true, CultureInfo.InvariantCulture));
    }

    public static string GetVersionName(int versionId)
    {
        var sb = new StringBuilder(versionId.ToString());
        return sb.Insert(1, ".").Append(".X").ToString();
    }
}