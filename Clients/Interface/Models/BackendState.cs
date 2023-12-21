namespace ContentAnalyzer.Frontend.Desktop.Models;

public class BackendState
{
    public ServiceState CollectionServiceState { get; set; }
    public ServiceState AnalysisServiceState { get; set; }
    public int ErrorsCount { get; set; }
    public int WarningsCount { get; set; }
    public int ProcessedComments { get; set; }
}