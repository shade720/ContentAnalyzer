namespace DevTool.Models;
public class Configuration
{
    public string ConnectionString { get; set; }
    public string CurrentCollectionServiceEndpoint { get; set; }
    public string CurrentAnalysisServiceEndpoint { get; set; }
    public List<string> AnalysisServiceEndpoints { get; set; }
    public List<string> CollectionServiceEndpoint { get; set; }
    public int ScanPostDelay { get; set; }
    public int ScanCommentsDelay { get; set; }
    public int PostQueueSize { get; set; }
    public int EvaluateThreshold { get; set; }
    public List<int> VkCommunities { get; set; }
    public int VkApplicationId { get; set; }
    public string VkSecureKey { get; set; }
    public string VkServiceAccessKey { get; set; }
}