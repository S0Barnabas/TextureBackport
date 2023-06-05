using System.Xml.Serialization;

namespace TextureBackport.Api.ResourceMapping;

public class SupportedVersions
{
    [XmlElement("GameVersion")]
    public List<GameVersion> SupportList { get; set; }

    public SupportedVersions()
    {
        SupportList = new List<GameVersion>();
    }

    public SupportedVersions(List<GameVersion> supportedVersions)
    {
        SupportList = supportedVersions;
    }
}
