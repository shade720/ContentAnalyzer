namespace VkAPITester;

public class VkDataCollector
{
    private readonly ApiClient _apiClient;
    private readonly List<CommunityAnalyzer> _analyzers;
    private readonly Storage _storage;

    public VkDataCollector(ApiClient vkApi, Storage storage) => (_apiClient, _analyzers, _storage) = (vkApi, new List<CommunityAnalyzer>(), storage);

    public void AddCommunity(long communityId)
    {
        _analyzers.Add(new CommunityAnalyzer(communityId, _apiClient, _storage, 3));
    }

    public void StartCollecting()
    {
        foreach (var analyzer in _analyzers) analyzer.Start();
    }

    public void StopCollecting()
    {
        foreach (var analyzer in _analyzers) analyzer.Stop();
    }

}