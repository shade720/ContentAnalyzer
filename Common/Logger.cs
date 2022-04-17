using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Common;

public static class Logger
{
    public delegate void OnLogging(string log);
    public static event OnLogging OnLoggingEvent;

    public static void Log(string log, LogLevel? logLevel = null)
    {
        OnLoggingEvent?.Invoke(log);
    }
    public enum LogLevel
    {
        Fatal,
        Error,
        Warning,
        Information,
        Debug,
        Verbose
    }
}