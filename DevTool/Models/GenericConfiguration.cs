namespace DevTool.Models;
internal class GenericConfiguration
{
    public string ConnectionString { get; init; } = string.Empty;
    public string CurrentCollectionServiceEndpoint { get; init; } = string.Empty;
    public string CurrentAnalysisServiceEndpoint { get; init; } = string.Empty;
    public List<string> AnalysisServiceEndpoints { get; init; } = new();
    public List<string> CollectionServiceEndpoints { get; init; } = new();
}