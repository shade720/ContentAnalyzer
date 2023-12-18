namespace DevTool.Models.LogModel;

public class LogEntry
{
    public DateTime Date { get; init; }
    public LogLevel Level { get; init; }
    public string Message { get; init; }
}