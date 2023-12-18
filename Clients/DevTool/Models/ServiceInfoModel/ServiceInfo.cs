namespace DevTool.Models.ServiceInfoModel;

public class ServiceInfo
{
    public ConnectionState ConnectionState { get; set; }
    public State State { get; set; }
    public TimeSpan Uptime { get; set; }
    public int ErrorsCount { get; set; }
    public int WarningsCount { get; set; }
    public int CollectedCommentsCount { get; set; }
    public int EvaluatedCommentsCount { get; set; }
}