using TextureBackport.Api.ImageProcessing.Layers;
using TextureBackport.Api.ImageProcessing.Pipelines;
using TextureBackport.Api.ImageProcessing.Streams;
using TextureBackport.Api.Logging;

namespace TextureBackport.Api.ImageProcessing;

public class ImageProcessor
{
    private Dictionary<int, ImageProcessingPipeline> openPipelines;

    private int lastId;

    public ILogger Logger { get; }
    
    public ImageProcessor(ILogger logger)
    {
        lastId = 0;
        openPipelines = new Dictionary<int, ImageProcessingPipeline>();
        Logger = logger;
    }

    public int OpenPipeline(PipelineOptions options)
    {
        openPipelines.Add(lastId, new ImageProcessingPipeline(new ReadLayer(), new WriteLayer(), new TransformLayer(), Logger));
        openPipelines[lastId].Open(options);
        return lastId++;
    }

    public void PushStream(int pipelineId, BitmapStream stream)
    {
        openPipelines[pipelineId].PushAll(stream);
    }

    public void ClosePipeline(int pipelineId)
    {
        openPipelines[pipelineId].Close();
        openPipelines.Remove(pipelineId);
    }

    public void CloseAllPipelines()
    {
        foreach (var pl in openPipelines.Values) pl.Close();
        openPipelines.Clear();
    }
}