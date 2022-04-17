using Serilog.Core;
using Serilog.Events;

namespace Common;

public class EventSink : ILogEventSink
{
    public delegate void OnLogging(string log);
    public event OnLogging OnLoggingEvent;
    public void Emit(LogEvent logEvent)
    {
        var message = logEvent.RenderMessage();
        var level = logEvent.Level.ToString();
        var timeStamp = logEvent.Timestamp.ToString();
        OnLoggingEvent?.Invoke($"{level} {timeStamp} {message}");
    }
}