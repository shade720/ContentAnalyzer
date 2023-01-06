using DataCollectionService.BusinessLogicLayer.SocialNetworkClients.VkClient.Data;

namespace DataCollectionService.BusinessLogicLayer.SocialNetworkClients.VkClient.Scanners;

internal abstract class Scanner
{
    protected readonly long CommunityId;

    protected readonly IConfiguration Configuration;
    protected readonly VkApi ClientApi;
    protected readonly CommentDataManager CommentManager;
    protected CancellationTokenSource StopScanToken;

    protected Scanner(long communityId, VkApi clientApi, CommentDataManager dataManager, IConfiguration configuration)
    {
        ClientApi = clientApi;
        CommentManager = dataManager;
        Configuration = configuration;
        CommunityId = communityId;
    }

    public abstract void StartScan();
    public abstract void StopScan();
}