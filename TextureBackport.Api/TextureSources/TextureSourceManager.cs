using System.Drawing;
using System.IO.Compression;
using System.Reflection;

namespace TextureBackport.Api.TextureSources;

public static class TextureSourceManager
{
    private static string namespacePrefix = typeof(TextureSourceManager).Namespace!;

    public static string TextureSourcePath { get; set; }
    
    public static Bitmap GetImage(string filePath, string entryName, bool embeddedResource=false)
    {
        if (!File.Exists(filePath))
        {
            if (embeddedResource) extractResource(filePath);
            else throw new FileNotFoundException($"File not found: {filePath}");
        }
        using var zip = ZipFile.OpenRead(filePath);
        var entry = zip.Entries.FirstOrDefault(e => e.FullName == entryName);
        return new Bitmap(entry!.Open());
    }

    public static bool EntryExists(string filePath, string entryName, bool embeddedResource=false)
    {
        if (!File.Exists(filePath))
        {
            if (embeddedResource) extractResource(filePath);
            else throw new FileNotFoundException($"File not found: {filePath}");
        }
        using var zip = ZipFile.OpenRead(filePath);
        return zip.Entries.Select(e => e.FullName).Contains(entryName);
    }

    private static void extractResource(string resourceName)
    {
        var fullName = $"{namespacePrefix}.{resourceName}";
        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fullName);
        using var fs = new FileStream($"{resourceName}", FileMode.Create);
        for (int i = 0; i < stream.Length; i++)
            fs.WriteByte((byte)stream.ReadByte());
        fs.Close();
    }

    public static void DeleteResource(string resourceName)
    {
        if (File.Exists(resourceName)) File.Delete(resourceName);
    }
}