using DataAnalysisService;
using Grpc.Net.Client;

namespace ContentAnalyzer.Gateway.GrpcClients;

public class DataAnalysisServiceClient
{
    private readonly DataAnalysis.DataAnalysisClient _dataAnalysisClient;

    public DataAnalysisServiceClient(IConfiguration configuration)
    {
        if (configuration is null)
            throw new ArgumentNullException(nameof(configuration));
        var hostFromEnvironment = Environment.GetEnvironmentVariable("DATA_ANALYSIS_SERVICE_HOST");
        var host = !string.IsNullOrEmpty(hostFromEnvironment) ? hostFromEnvironment : configuration["DataAnalysisServiceHost"];
        if (string.IsNullOrEmpty(host))
            throw new ArgumentNullException(nameof(host));
        var grpcChannel = GrpcChannel.ForAddress(host);
        _dataAnalysisClient = new DataAnalysis.DataAnalysisClient(grpcChannel);
    }

    public async Task<StartAnalysisServiceReply> StartAnalysisServiceAsync(StartAnalysisServiceRequest request)
    {
        return await _dataAnalysisClient.StartAnalysisServiceAsync(request);
    }

    public async Task<StopAnalysisServiceReply> StopAnalysisServiceAsync(StopAnalysisServiceRequest request)
    {
        return await _dataAnalysisClient.StopAnalysisServiceAsync(request);
    }

    public async Task<EvaluatedCommentsReply> GetEvaluatedComments(EvaluatedCommentsRequest request)
    {
        return await _dataAnalysisClient.GetEvaluatedCommentsAsync(request);
    }

    public async Task<ClearEvaluatedDatabaseReply> ClearEvaluatedCommentsDatabase(ClearEvaluatedDatabaseRequest request)
    {
        return await _dataAnalysisClient.ClearEvaluatedDatabaseAsync(request);
    }

    public async Task<LogReply> GetAnalysisServiceLogs(LogRequest request)
    {
        return await _dataAnalysisClient.GetLogsAsync(request);
    }

    public async Task<SetConfigurationReply> SetAnalysisServiceConfiguration(SetConfigurationRequest request)
    {
        return await _dataAnalysisClient.SetConfigurationAsync(request);
    }
}