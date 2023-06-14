using System.Drawing;
using System.Xml.Serialization;
using TextureBackport.Api.Geometry;

namespace TextureBackport.Api.ImageProcessing.Pipelines;

public class PipelineOptions
{
    [XmlElement]
    public XmlSize Size { get; set; }
    
    [XmlAttribute]
    public int TileSize { get; set; }
    
    [XmlIgnore]
    public int UpscaleMultiplier { get; set; }

    [XmlIgnore]
    public int GameVersionId { get; set; }
    
    [XmlIgnore]
    public string TemplateBitmapPath { get; set; }
    
    [XmlIgnore]
    public string DestinationBitmapPath { get; set; }

    public PipelineOptions() { }
    
    public static PipelineOptions Entity
    {
        get => new()
        {
            Size = new XmlSize(64, 32), 
            TileSize = 1, 
            UpscaleMultiplier = 1,
            GameVersionId = 0,
            TemplateBitmapPath = string.Empty,
            DestinationBitmapPath = string.Empty
        };
    }
    
    public static PipelineOptions Atlas
    {
        get => new()
        {
            Size = new XmlSize(16, 16), 
            TileSize = 16, 
            UpscaleMultiplier = 1,
            GameVersionId = 0,
            TemplateBitmapPath = string.Empty,
            DestinationBitmapPath = string.Empty
        };
    }
    
    public static PipelineOptions Image
    {
        get => new()
        {
            Size = XmlSize.Empty, 
            TileSize = 1, 
            UpscaleMultiplier = 1,
            GameVersionId = 0,
            TemplateBitmapPath = string.Empty,
            DestinationBitmapPath = string.Empty
        };
    }
}