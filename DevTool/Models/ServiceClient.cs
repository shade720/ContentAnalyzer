using Common;
using Grpc.Net.Client;

namespace DevTool.Models;

internal abstract class ServiceClient<TData> : IDisposable
{
    #region Protected

    protected ServiceClient(string host)
    {
        Channel = GrpcChannel.ForAddress(host);
        _serviceInfo = new ServiceInfo { State = State.Down };
        _lastPollingTime = DateTime.Today;
    }

    protected readonly GrpcChannel Channel;
    protected abstract string GetLogFile(DateTime date);

    #endregion

    #region Public

    public IProgress<ServiceInfo> OnServiceInfoProgress;
    public abstract void StartService();
    public abstract void StopService();
    public abstract void ClearDatabase();
    public abstract void LoadConfiguration(string settings);
    public abstract IEnumerable<TData> GetResults(CommentsQueryFilter filter);

    public IEnumerable<LogInfo> GetLog(DateTime date)
    {
        return LogFileParser(GetLogFile(date));
    }
    
    public void StartPolling()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        Task.Run(() =>
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                Poll();
                if (_serviceInfo.ConnectionState == ConnectionState.Disconnected)
                {
                    _cancellationTokenSource.Cancel();
                    break;
                }
                Thread.Sleep(10000);
            }
        }, _cancellationTokenSource.Token);
    }

    public void StopPolling()
    {
        _cancellationTokenSource.Cancel();
    }

    public void Poll()
    {
        var logFile = string.Empty;
        try
        {
            logFile = GetLogFile(_lastPollingTime);
            _serviceInfo.ConnectionState = ConnectionState.Connected;
        }
        catch
        {
            _serviceInfo.ConnectionState = ConnectionState.Disconnected;
        }
        var log = LogFileParser(logFile).ToList();
        if (log.Any()) _lastPollingTime = log[^1].Date;
        UpdateServiceInfo(_serviceInfo, log);
        OnServiceInfoProgress.Report(_serviceInfo);
    }
    public void Dispose() => Channel.Dispose();

    #endregion

    #region Private

    private readonly ServiceInfo _serviceInfo;
    private DateTime _lastPollingTime;
    private CancellationTokenSource _cancellationTokenSource;

    private static void UpdateServiceInfo(ServiceInfo updatedServiceInfo, IEnumerable<LogInfo> logInfos)
    {
        updatedServiceInfo.ErrorsCount = 0;
        updatedServiceInfo.WarningsCount = 0;
        foreach (var logInfo in logInfos)
        {
            if (logInfo.Level == LogLevel.Error) updatedServiceInfo.ErrorsCount++;
            else if (logInfo.Level == LogLevel.Warning) updatedServiceInfo.WarningsCount++;
            else if (logInfo.Message.Contains("stopped")) updatedServiceInfo.State = State.Down;
            else if (logInfo.Message.Contains("started")) updatedServiceInfo.State = State.Up;
            else if (logInfo.Message.Contains("Uptime: 00:00:00")) updatedServiceInfo.State = State.Down;
            else if (logInfo.Message.Contains("collected")) updatedServiceInfo.CollectedCommentsCount = int.Parse(logInfo.Message.Trim().Split(" ")[0]);
            else if (logInfo.Message.Contains("evaluated")) updatedServiceInfo.EvaluatedCommentsCount = int.Parse(logInfo.Message.Trim().Split(" ")[0]);
            else if (logInfo.Message.Contains("Uptime")) updatedServiceInfo.Uptime = TimeSpan.Parse(logInfo.Message.Trim().Replace('"', ' ').Split(" ")[1]);
        }
    }

    private static IEnumerable<LogInfo> LogFileParser(string logFile)
    {
        if (string.IsNullOrEmpty(logFile)) yield break;
        var logRecords = logFile.Split("`~");
        foreach (var log in logRecords)
        {
            if (string.IsNullOrEmpty(log)) continue;
            var date = log[..(log.IndexOf('+') - 2)];
            var level = log[log.IndexOf('[')..(log.IndexOf(']') + 1)];
            var logMessage = log[(log.IndexOf(']') + 1)..];
            yield return new LogInfo
            {
                Date = DateTime.Parse(date),
                Level = level switch
                {
                    "[Fatal]" => LogLevel.Fatal,
                    "[Error]" => LogLevel.Error,
                    "[Warning]" => LogLevel.Warning,
                    "[Information]" => LogLevel.Information,
                    _ => LogLevel.Information
                },
                Message = logMessage
            };
        }
    }

    #endregion
}