namespace DevTool.Models;

internal class LogInfo
{
    public DateTime Date { get; set; }
    public LogLevel Level { get; set; }
    public string Message { get; set; }
}

public enum LogLevel
{
    Fatal,
    Error,
    Warning,
    Information
}