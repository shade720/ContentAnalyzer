using VkAPITester.Models.Storages;

namespace VkAPITester.Models.VkDataCollector;

public abstract class Scanner
{
    protected readonly long CommunityId;
    protected readonly VkApi ClientApi;
    protected readonly IStorage Storage;
    protected readonly CancellationTokenSource StopScanToken;
    protected readonly Config Configuration;

    protected Scanner(long communityId, VkApi clientApi, IStorage storage, CancellationTokenSource stopScanToken, Config configuration)
    {
        ClientApi = clientApi;
        Storage = storage;
        StopScanToken = stopScanToken;
        Configuration = configuration;
        CommunityId = communityId;
    }

    public abstract void StartScan();
    public abstract void StopScan();
}