using DataCollectionService.DataCollectors.VkDataCollector.Data;

namespace DataCollectionService.DataCollectors.VkDataCollector.Scanners;

internal abstract class Scanner
{
    protected readonly long CommunityId;

    protected readonly Config Configuration;
    protected readonly VkApi ClientApi;
    protected readonly CommentDataManager CommentManager;
    protected CancellationTokenSource? StopScanToken;
    
    protected Scanner(long communityId, VkApi clientApi, CommentDataManager dataManager, Config configuration)
    {
        ClientApi = clientApi;
        CommentManager = dataManager;
        Configuration = configuration;
        CommunityId = communityId;
    }

    public abstract void StartScan();
    public abstract void StopScan();
}