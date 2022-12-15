namespace DevTool.Models;

internal class Configuration
{
    public string CurrentCollectionServiceEndpoint { get; set; }
    public string CurrentAnalysisServiceEndpoint { get; set; }
    public List<string> AnalysisServiceEndpoints { get; set; }
    public List<string> CollectionServiceEndpoint { get; set; }

}