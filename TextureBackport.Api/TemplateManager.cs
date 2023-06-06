using System.IO.Compression;
using System.Reflection;

namespace TextureBackport.Api;

public class TemplateManager
{
    private string resourceName;

    public TemplateManager()
    {
        resourceName = "TextureBackport.Api.TexturePackTemplates.";
    }

    public void ExtractTemplate(string templateName, string directory)
    {
        if (Directory.Exists(directory))
            Directory.Delete(directory, true);

        var fullName = resourceName + templateName + ".zip";
        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fullName);
        using var fs = new FileStream($"{templateName}.zip", FileMode.CreateNew);
        for (int i = 0; i < stream.Length; i++)
            fs.WriteByte((byte)stream.ReadByte());
        fs.Close();

        ZipFile.ExtractToDirectory($"{templateName}.zip", directory);
        File.Delete($"{templateName}.zip");
    }
}
