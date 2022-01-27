namespace VkAPITester;

public class VkDataCollector
{
    private readonly VkApi _vkApi;
    private readonly List<PostScanner> _postScanners;
    private readonly IStorage _storage;
    private Config? _config;

    public VkDataCollector(IStorage storage) => (_vkApi, _postScanners, _storage) = (new VkApi(), new List<PostScanner>(), storage);

    public void Configure(Config configure) => _config = configure;
    
    public void AddCommunity(long communityId)
    {
        if (_config is null) throw new ArgumentException("Configuration is missing");
        _postScanners.Add(new PostScanner(communityId, _vkApi, _storage, _config));
    }

    public void StartCollecting()
    {
        if (_config is null) throw new ArgumentException("Configuration is missing");
        var a = _vkApi.Auth(_config.ApplicationId, _config.SecureKey, _config.ServiceAccessKey);
        foreach (var scanner in _postScanners) scanner.StartScan();
    }

    public void StopCollecting()
    {
        foreach (var scanner in _postScanners) scanner.StopScan();
        var a = _vkApi.LogOut();
    }
}