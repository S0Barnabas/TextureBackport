using System.Drawing;
using TextureBackport.Api.Geometry;
using TextureBackport.Api.ImageFraming;
using TextureBackport.Api.ImageProcessing.Layers;
using TextureBackport.Api.ImageProcessing.Streams;
using TextureBackport.Api.Logging;
using TextureBackport.Api.TextureSources;

namespace TextureBackport.Api.ImageProcessing.Pipelines;

public class ImageProcessingPipeline
{
    public PipelineOptions Options { get; private set; }
    
    private readonly IBitmapReader _bitmapReader;
    private readonly IBitmapWriter _bitmapWriter;
    private readonly IBitmapTransform _bitmapTransform;
    private readonly ILogger _logger;

    private Graphics? graphics;
    private Bitmap? bitmap;

    public ImageProcessingPipeline(IBitmapReader bitmapReader, IBitmapWriter bitmapWriter, IBitmapTransform bitmapTransform, ILogger logger)
    {
        _bitmapReader = bitmapReader;
        _bitmapWriter = bitmapWriter;
        _bitmapTransform = bitmapTransform;
        _logger = logger;
    }

    public void Open(PipelineOptions options)
    {
        Options = options;
        if (!string.IsNullOrEmpty(Options.TemplateBitmapPath))
        {
            using var template = _bitmapReader.GetBitmap(Options.TemplateBitmapPath.Replace(@"\", "/"), true);
            bitmap = _bitmapTransform.Scale(template, Options.UpscaleMultiplier);
        }
        else
        {
            if (Options.Size == XmlSize.Empty) return;
            var upscaleSize = Options.Size * Options.TileSize * Options.UpscaleMultiplier;
            bitmap = new Bitmap(upscaleSize.Width, upscaleSize.Height);
        }
        graphics = Graphics.FromImage(bitmap);
    }

    public void Push(BitmapFrame frame)
    {
        if (graphics == null)
        {
            bitmap = _bitmapReader.GetBitmap(frame.SrcPath);
            return;
        }

        try
        {
            frame.Bitmap = frame.SrcRect == XmlRectangle.Empty
            ? _bitmapReader.GetBitmap(frame.SrcPath)
            : _bitmapReader.GetBitmapFragment(frame.SrcPath, frame.SrcRect * Options.UpscaleMultiplier);
        }
        catch (Exception)
        {

            _logger.Log(LogLevel.ERROR, $"Unexpected resolution! File: {frame.SrcPath}");
            return;
        }
        
        
        if ((int)frame.RotateFlipType != 0) _bitmapTransform.Rotate(frame.Bitmap, frame.RotateFlipType);

        if (frame.DstRect.Size != XmlSize.Empty)
            frame.Bitmap = _bitmapTransform.Scale(frame.Bitmap, frame.DstRect.Size * Options.UpscaleMultiplier);

        if (frame.TileCoords != new XmlPoint(-1, -1))
        {
            if (frame.DstRect != XmlRectangle.Empty)
            {
                var r = frame.DstRect;
                r.Location += frame.TileCoords * Options.TileSize;
                _bitmapWriter.DrawBitmap(frame.Bitmap, graphics, r * Options.UpscaleMultiplier, !string.IsNullOrEmpty(Options.TemplateBitmapPath));
            }
            else
                _bitmapWriter.DrawBitmap(frame.Bitmap, graphics, frame.TileCoords * (Options.TileSize * Options.UpscaleMultiplier), !string.IsNullOrEmpty(Options.TemplateBitmapPath));
        }
        else _bitmapWriter.DrawBitmap(frame.Bitmap, graphics, frame.DstRect.Location * Options.UpscaleMultiplier, !string.IsNullOrEmpty(Options.TemplateBitmapPath));
        frame.Bitmap.Dispose();
    }

    public void PushAll(BitmapStream stream)
    {
        foreach (var frame in stream.Frames.Where(supportedVersion))
        {
            var path = getFullPath(frame.SrcPath, stream.SourceDirectories);
            if (string.IsNullOrEmpty(path))
            {
                _logger.Log(LogLevel.WARN, $"File not found: {frame.SrcPath}");
                continue;
            }

            frame.SrcPath = path;
            _logger.Log(LogLevel.INFO, $"Drawing: {path}");
            Push(frame);
        }
        if (bitmap != null)
            _bitmapWriter.DrawToFile(bitmap, Options.DestinationBitmapPath);
    }

    public void Close()
    {
        graphics?.Dispose();
        bitmap?.Dispose();
    }

    private string getFullPath(string srcPath, List<string> srcDirs)
    {
        foreach (var srcDir in srcDirs)
        {
            var path = Path.Combine(srcDir, srcPath).Replace(@"\", "/");
            if (TextureSourceManager.EntryExists(TextureSourceManager.TextureSourcePath, path)) return path;
        }
        return string.Empty;
    }

    private bool supportedVersion(BitmapFrame frame)
    {
        return Options.GameVersionId >= frame.MinVersionId &&
               (Options.GameVersionId <= frame.MaxVersionId || frame.MaxVersionId == 0);
    }
}