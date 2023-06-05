using System.Xml.Serialization;
using TextureBackport.Api.ResourceMapping;

namespace BitmapTestProject.Resources;

public class TextureFile
{
    [XmlAttribute("targetFile")]
    public string TargetFile { get; set; }

    [XmlAttribute("targetWidth")]
    public int TargetWidth { get; set; }

    [XmlAttribute("targetHeight")]
    public int TargetHeight { get; set; }

    [XmlElement("TextureSource")]
    public List<TextureSource> TextureSources { get; set; }
    
    public TextureFile()
    {
        TextureSources = new List<TextureSource>();
    }

    public TextureFile(string targetFile, int targetWidth, int targetHeight)
    {
        TargetFile = targetFile;
        TextureSources = new List<TextureSource>();
        TargetWidth = targetWidth;
        TargetHeight = targetHeight;
    }

    public void All(string sourceFileName)
    {
        TextureSources.Add(new TextureSource(sourceFileName, 0, 0));
    }

    public void From(GameVersion from, string sourceFilePath)
    {
        TextureSources.Add(new TextureSource(sourceFilePath, from.Id, 0));
    }

    public TextureFile To(GameVersion to, string sourceFilePath)
    {
        var textureSource =
            new TextureSource(sourceFilePath, 0, to.Id);
        TextureSources.Add(textureSource);
        return this;
    }

    public TextureFile FromTo(GameVersion from, GameVersion to, string sourceFilePath)
    {
        var textureSource = new TextureSource(sourceFilePath, from.Id, to.Id);
        TextureSources.Add(textureSource);
        return this;
    }
}