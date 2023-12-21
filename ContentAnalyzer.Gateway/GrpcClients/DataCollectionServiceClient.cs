using DataCollectionService;
using Grpc.Net.Client;

namespace ContentAnalyzer.Gateway.GrpcClients;

public class DataCollectionServiceClient
{
    private readonly DataCollection.DataCollectionClient _dataCollectionClient;

    public DataCollectionServiceClient(IConfiguration configuration)
    {
        if (configuration is null)
            throw new ArgumentNullException(nameof(configuration));
        var hostFromEnvironment = Environment.GetEnvironmentVariable("DATA_COLLECTION_SERVICE_HOST");
        var host = !string.IsNullOrEmpty(hostFromEnvironment) ? hostFromEnvironment : configuration["DataCollectionServiceHost"];
        if (string.IsNullOrEmpty(host))
            throw new ArgumentNullException(nameof(host));
        var grpcChannel = GrpcChannel.ForAddress(host);
        _dataCollectionClient = new DataCollection.DataCollectionClient(grpcChannel);
    }

    public async Task<StartCollectionServiceReply> StartCollectionServiceAsync(StartCollectionServiceRequest request)
    {
        return await _dataCollectionClient.StartCollectionServiceAsync(request);
    }

    public async Task<StopCollectionServiceReply> StopCollectionServiceAsync(StopCollectionServiceRequest request)
    {
        return await _dataCollectionClient.StopCollectionServiceAsync(request);
    }

    public async Task<GetCommentsReply> GetComments(GetCommentsRequest request)
    {
        return await _dataCollectionClient.GetCommentsAsync(request);
    }

    public async Task<ClearCommentsDatabaseReply> ClearCommentsDatabase(ClearCommentsDatabaseRequest request)
    {
        return await _dataCollectionClient.ClearCommentsDatabaseAsync(request);
    }

    public async Task<LogReply> GetCollectionServiceLogs(LogRequest request)
    {
        return await _dataCollectionClient.GetLogsAsync(request);
    }

    public async Task<SetConfigurationReply> SetCollectionServiceConfiguration(SetConfigurationRequest request)
    {
        return await _dataCollectionClient.SetConfigurationAsync(request);
    }
}