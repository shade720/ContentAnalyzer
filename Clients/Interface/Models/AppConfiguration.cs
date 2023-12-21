namespace ContentAnalyzer.Frontend.Desktop.Models;
public class AppConfiguration
{
    public string CurrentBackendHost { get; init; }
    public List<string> BackendHosts { get; init; }
    public string BackendLogin { get; init; }
    public string BackendToken { get; init; }
    public int ScanPostDelay { get; init; }
    public int ScanCommentsDelay { get; init; }
    public int PostQueueSize { get; init; }
    public int ObserveDelay { get; init; }
}