namespace DevTool.Models;
public class Configuration
{
    public string ConnectionString { get; init; }
    public string CurrentCollectionServiceEndpoint { get; init; }
    public string CurrentAnalysisServiceEndpoint { get; init; }
    public List<string> AnalysisServiceEndpoints { get; init; }
    public List<string> CollectionServiceEndpoints { get; init; }
    public int ScanPostDelay { get; init; }
    public int ScanCommentsDelay { get; init; }
    public int PostQueueSize { get; init; }
    public int ObserveDelay { get; init; }
}