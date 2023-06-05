using System.Xml.Serialization;

namespace BitmapTestProject.Resources;

public class TextureBundle
{
    [XmlAttribute("sourceDirectory")]
    public string SourceDirectory { get; set; }
    
    [XmlElement("TextureFile")]
    public List<TextureFile> TextureFiles { get; set; }
    
    public TextureBundle()
    {
        TextureFiles = new List<TextureFile>();
    }

    public TextureBundle(string sourceDirectory)
    {
        SourceDirectory = sourceDirectory;
        TextureFiles = new List<TextureFile>();
    }

    public TextureFile AddTexture(string targetFile, int targetWidth=0, int targetHeight=0)
    {
        var textureFile = new TextureFile(targetFile, targetWidth, targetHeight);
        TextureFiles.Add(textureFile);
        return textureFile;
    }
}