using System.Globalization;

namespace Common;

public static class Logger
{
    public delegate void OnLogging(string log, LogLevel logLevel);
    public static event OnLogging OnLoggingEvent;

    public static void Log(string log, LogLevel? logLevel = null)
    {
        var formattedLog = $"[{logLevel}] [{DateTime.Now.ToString(CultureInfo.InvariantCulture)}] {log}\r\n";
        OnLoggingEvent?.Invoke(formattedLog, logLevel ?? LogLevel.Information);
        Console.WriteLine(formattedLog);
    }

    public enum LogLevel
    {
        Fatal,
        Error,
        Warning,
        Information
    }
}