namespace DevTool.Models;

public class ServiceManager
{
    private CollectionServiceClient _collectionServiceClient;
    private AnalysisServiceClient _analysisServiceClient;

    private readonly ServiceInfo _collectionServiceInfo = new() { State = State.Down};
    private readonly ServiceInfo _analysisServiceInfo = new() { State = State.Down};

    private DateTime _lastPollingTime = DateTime.Now;

    private CancellationTokenSource _cancellationCollectionService;
    private CancellationTokenSource _cancellationAnalysisService;

    private IProgress<ServiceInfo> OnCollectionServicInfoArrived;
    private IProgress<ServiceInfo> OnAnalysisServicInfoArrived;

    #region Public

    public void Subscribe(Action<ServiceInfo> collectionInfoProcessor, Action<ServiceInfo> analysisInfoProcessor)
    {
        OnCollectionServicInfoArrived = new Progress<ServiceInfo>(collectionInfoProcessor);
        OnAnalysisServicInfoArrived = new Progress<ServiceInfo>(analysisInfoProcessor);
    }

    public void SetCollectionServiceHost(string host)
    {
        _cancellationCollectionService?.Cancel();
        _collectionServiceClient?.Dispose();
        _collectionServiceClient = new CollectionServiceClient(host);
    }
    public void SetAnalysisServiceHost(string host)
    {
        _cancellationAnalysisService?.Cancel();
        _analysisServiceClient?.Dispose();
        _analysisServiceClient = new AnalysisServiceClient(host);
    }

    public void StartDataCollectionService()
    {
        _collectionServiceClient.StartService();
        _cancellationCollectionService = new CancellationTokenSource();
        Task.Run(() => PollingCollectionService(_cancellationCollectionService.Token), _cancellationCollectionService.Token);
    }

    public void StopDataCollectionService()
    {
        _cancellationCollectionService.Cancel();
        _collectionServiceClient.StopService();
    }

    public void StartDataAnalysisService()
    {
        _analysisServiceClient.StartService();
        _cancellationAnalysisService = new CancellationTokenSource();
        Task.Run(() => PollingAnalysisService(_cancellationAnalysisService.Token), _cancellationAnalysisService.Token);
    }

    public void StopDataAnalysisService()
    {
        _cancellationAnalysisService.Cancel();
        _analysisServiceClient.StopService();
    }

    public ServiceInfo PollCollectionService() => Poll(_collectionServiceClient, _collectionServiceInfo);
    public ServiceInfo PollAnalysisService() => Poll(_analysisServiceClient, _analysisServiceInfo);

    public IEnumerable<LogInfo> ViewCollectionLog(DateTime date) => LogFileParser(_collectionServiceClient.GetLogFile(date));
    public IEnumerable<LogInfo> ViewAnalysisLog(DateTime date) => LogFileParser(_analysisServiceClient.GetLogFile(date));

    public void LoadConfiguration(string settings)
    {
        _collectionServiceClient.LoadConfiguration(settings);
    }

    #endregion

    #region Private

    private void PollingCollectionService(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            var serviceInfo = PollCollectionService();
            if (serviceInfo.ConnectionState == ConnectionState.Disconnected)
            {
                _cancellationCollectionService.Cancel();
                break;
            }
            OnCollectionServicInfoArrived.Report(serviceInfo);
            Thread.Sleep(10000);
        }
    }

    private void PollingAnalysisService(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            var serviceInfo = PollAnalysisService();
            if (serviceInfo.ConnectionState == ConnectionState.Disconnected)
            {
                _cancellationCollectionService.Cancel();
                break;
            }
            OnAnalysisServicInfoArrived.Report(serviceInfo);
            Thread.Sleep(10000);
        }
    }

    private ServiceInfo Poll(ServiceClient serviceClient, ServiceInfo serviceInfo)
    {
        string logFile;
        try
        {
            logFile = serviceClient.GetLogFile(_lastPollingTime);
            serviceInfo.ConnectionState = ConnectionState.Connected;
        }
        catch
        {
            serviceInfo.ConnectionState = ConnectionState.Disconnected;
            return serviceInfo;
        }
        var logs = LogFileParser(logFile).Where(l => l.Date > _lastPollingTime).ToList();
        if (logs.Any())
            _lastPollingTime = logs[^1].Date;
        UpdateServiceInfo(serviceInfo, logs);
        return serviceInfo;
    }

    private static void UpdateServiceInfo(ServiceInfo updatedServiceInfo, IEnumerable<LogInfo> logInfos)
    {
        foreach (var logInfo in logInfos)
        {
            if (logInfo.Level == LogLevel.Error) updatedServiceInfo.ErrorsCount++;
            if (logInfo.Level == LogLevel.Warning) updatedServiceInfo.WarningsCount++;
            if (logInfo.Message.Contains("stopped")) updatedServiceInfo.State = State.Down;
            if (logInfo.Message.Contains("started")) updatedServiceInfo.State = State.Up;
            if (logInfo.Message.Contains("collected")) updatedServiceInfo.CollectedCommentsCount = int.Parse(logInfo.Message.Trim().Split(" ")[0]);
            if (logInfo.Message.Contains("evaluated")) updatedServiceInfo.EvaluatedCommentsCount = int.Parse(logInfo.Message.Trim().Split(" ")[0]);
            if (logInfo.Message.Contains("Uptime")) updatedServiceInfo.Uptime = TimeSpan.Parse(logInfo.Message.Trim().Replace('"', ' ').Split(" ")[1]);
        }
    }

    private static IEnumerable<LogInfo> LogFileParser(string logFile)
    {
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
                Level = level switch { "[Fatal]" => LogLevel.Fatal, "[Error]" => LogLevel.Error, "[Warning]" => LogLevel.Warning, "[Information]" => LogLevel.Information,
                    _ => LogLevel.Information
                },
                Message = logMessage
            };
        }
    }

    #endregion
}