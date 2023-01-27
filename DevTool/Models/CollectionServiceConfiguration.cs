namespace DevTool.Models;
public class CollectionServiceConfiguration
{
    public int ScanPostDelay { get; init; }
    public int ScanCommentsDelay { get; init; }
    public int PostQueueSize { get; init; }
}