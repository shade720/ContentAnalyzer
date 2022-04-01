namespace Common;

public static class Logger
{
    public delegate void OnLogging(string log);
    public static event OnLogging OnLoggingEvent;

    public static void Write(string log)
    {
        OnLoggingEvent?.Invoke(log);
        Console.WriteLine(log);
    }
}