namespace DevTool.Models;

public class ServiceManager
{
    private CollectionServiceClient _collectionServiceClient;
    private AnalysisServiceClient _analysisServiceClient;

    private readonly ServiceInfo _collectionServiceInfo = new();
    private readonly ServiceInfo _analysisServiceInfo = new();

    private DateTime _lastPollingTime = DateTime.Now;

    #region Public

    public void SetCollectionServiceHost(string host)
    {
        _collectionServiceClient?.Dispose();
        _collectionServiceClient = new CollectionServiceClient(host);
    }
    public void SetAnalysisServiceHost(string host)
    {
        _analysisServiceClient?.Dispose();
        _analysisServiceClient = new AnalysisServiceClient(host);
    }

    public void StartDataCollectionService()
    {
        _collectionServiceClient.StartService();
    }

    public void StopDataCollectionService()
    {
        _collectionServiceClient.StopService();
    }

    public void StartDataAnalysisService()
    {
        _analysisServiceClient.StartService();
    }

    public void StopDataAnalysisService()
    {
        _analysisServiceClient.StopService();
    }

    public ServiceInfo PollCollectionService() => Poll(_collectionServiceClient, _collectionServiceInfo);
    public ServiceInfo PollAnalysisService() => Poll(_analysisServiceClient, _analysisServiceInfo);

    #endregion

    #region Private

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
        var logs = LogFileParser(logFile);
        _lastPollingTime = logs.Last().Date;
        var newServiceInfo = GenerateServiceInfo(logs);
        UpdateServiceInfo(serviceInfo, newServiceInfo);
        return serviceInfo;
    }

    private static void UpdateServiceInfo(ServiceInfo updatedServiceInfo, ServiceInfo newServiceInfo)
    {
        updatedServiceInfo.State = newServiceInfo.State;
        updatedServiceInfo.Uptime = newServiceInfo.Uptime;
        updatedServiceInfo.ErrorsCount += newServiceInfo.ErrorsCount;
        updatedServiceInfo.WarningsCount += newServiceInfo.WarningsCount;
        updatedServiceInfo.CollectedCommentsCount += newServiceInfo.CollectedCommentsCount;
        updatedServiceInfo.EvaluatedCommentsCount += newServiceInfo.EvaluatedCommentsCount;
    }

    private static ServiceInfo GenerateServiceInfo(IEnumerable<LogInfo> logInfos)
    {
        var newServiceInfo = new ServiceInfo();
        foreach (var logInfo in logInfos)
        {
            if (logInfo.Level == LogLevel.Error) newServiceInfo.ErrorsCount++;
            if (logInfo.Level == LogLevel.Warning) newServiceInfo.WarningsCount++;
            if (logInfo.Message.Contains("stopped")) newServiceInfo.State = State.Down;
            if (logInfo.Message.Contains("collected")) newServiceInfo.CollectedCommentsCount += int.Parse(logInfo.Message.Split(" ")[1]);
            if (logInfo.Message.Contains("evaluated")) newServiceInfo.CollectedCommentsCount += int.Parse(logInfo.Message.Split(" ")[1]);
        }
        return newServiceInfo;
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
                Level = level switch { "Fatal" => LogLevel.Fatal, "Error" => LogLevel.Error, "Warning" => LogLevel.Warning, "Information" => LogLevel.Information },
                Message = logMessage
            };
        }
    }

    #endregion
}