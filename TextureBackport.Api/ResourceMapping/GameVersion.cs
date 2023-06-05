using System.Xml.Serialization;

namespace TextureBackport.Api.ResourceMapping;

public class GameVersion
{
    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("id")]
    public int Id { get; set; }

    public GameVersion() { }

    public GameVersion(string name, int id)
    {
        Name = name;
        Id = id;
    }

    public static bool operator >(GameVersion a, GameVersion b) => a.Id > b.Id;
    public static bool operator <(GameVersion a, GameVersion b) => a.Id < b.Id;
}
