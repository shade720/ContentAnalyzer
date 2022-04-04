using VkDataCollector.Data;
using VkDataCollector.Scanners;
using Common;

namespace VkDataCollector;

public class VkDataCollector : IDataCollector
{
    private readonly VkApi _vkApi;
    private readonly List<Scanner> _postScanners;
    private readonly CommentDataManager _commentManager;
    private Config? _config;

    public VkDataCollector()
    {
        _vkApi = new VkApi();
        _postScanners = new List<Scanner>();
        _commentManager = new CommentDataManager();
    } 
    
    public void Subscribe(Action<ICommentData> handler)
    {
        _commentManager.OnNewCommentFoundEvent += handler.Invoke;
    }

    public void Configure(Config configure) => _config = configure;

    public void AddCommunity(long communityId)
    {
        if (_config is null) throw new ArgumentException("Configuration is missing");

        //Each community has a post scanner.
        _postScanners.Add(new PostScanner(communityId, _vkApi, _commentManager, _config));
    }

    public void StartCollecting()
    {
        if (_config is null) throw new ArgumentException("Configuration is missing");
        var result = _vkApi.AuthAsync(_config.ApplicationId, _config.SecureKey, _config.ServiceAccessKey);
        foreach (var scanner in _postScanners) 
            scanner.StartScan();
        Logger.Write("Data collection has begun");
    }

    public void StopCollecting()
    {
        foreach (var scanner in _postScanners) scanner.StopScan();
        var result = _vkApi.LogOutAsync();
        Logger.Write("Data collection has stopped");
    }
}