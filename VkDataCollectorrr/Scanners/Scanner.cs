using VkDataCollector.Data;

namespace VkDataCollector.Scanners;

internal abstract class Scanner
{
    protected readonly long CommunityId;
    protected readonly VkApi ClientApi;
    protected readonly DataManager DataManager;
    protected readonly CancellationTokenSource StopScanToken;
    protected readonly Config Configuration;

    protected Scanner(long communityId, VkApi clientApi, DataManager dataManager, CancellationTokenSource stopScanToken, Config configuration)
    {
        ClientApi = clientApi;
        DataManager = dataManager;
        StopScanToken = stopScanToken;
        Configuration = configuration;
        CommunityId = communityId;
    }

    public abstract void StartScan();
    public abstract void StopScan();
}