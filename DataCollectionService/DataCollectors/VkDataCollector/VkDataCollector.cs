using Common;
using Common.EntityFramework;
using DataCollectionService.DataCollectors.VkDataCollector.Data;
using DataCollectionService.DataCollectors.VkDataCollector.Scanners;
using Serilog;

namespace DataCollectionService.DataCollectors.VkDataCollector;

public class VkDataCollector : IDataCollector
{
    private readonly VkApi _vkApi;
    private readonly List<PostScanner> _postScanners;
    private readonly CommentDataManager _commentManager;
    private Config? _config;

    public VkDataCollector()
    {
        _vkApi = new VkApi();
        _postScanners = new List<PostScanner>();
        _commentManager = new CommentDataManager();
    }

    public void Subscribe(Action<CommentData> handler) => _commentManager.OnNewCommentFoundEvent += handler.Invoke;
    public void Unsubscribe(Action<CommentData> handler) => _commentManager.OnNewCommentFoundEvent -= handler.Invoke;

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
        Task.Run(() => _vkApi.AuthAsync(_config.ApplicationId, _config.SecureKey, _config.ServiceAccessKey));
        foreach (var scanner in _postScanners)
        {
            scanner.StartScan();
        }
        Log.Logger.Information("Data collection has begun");
    }

    public void StopCollecting()
    {
        foreach (var scanner in _postScanners) scanner.StopScan();
        while (_postScanners.Any(x => x.IsScanning))
            Thread.Sleep(1000);
        var result = _vkApi.LogOutAsync();
        Log.Logger.Information("Data collection has stopped");
    }
}