using System.Xml.Serialization;

namespace BitmapTestProject.Resources;

public class TextureBundle
{
    [XmlAttribute("sourceDirectory")]
    public string SourceDirectory { get; set; }

    [XmlAttribute("targetDirectory")]
    public string TargetDirectory { get; set; }

    [XmlElement("TextureFile")]
    public List<TextureFile> TextureFiles { get; set; }
    
    public TextureBundle()
    {
        TextureFiles = new List<TextureFile>();
    }

    public TextureBundle(string sourceDirectory, string targetDirectory)
    {
        SourceDirectory = sourceDirectory;
        TextureFiles = new List<TextureFile>();
        TargetDirectory = targetDirectory;
    }

    public TextureFile AddTexture(string targetFile, int targetWidth=0, int targetHeight=0)
    {
        var textureFile = new TextureFile(targetFile, targetWidth, targetHeight);
        TextureFiles.Add(textureFile);
        return textureFile;
    }
}