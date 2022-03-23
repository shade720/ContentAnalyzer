using VkDataCollector.Data;
using VkDataCollector.Scanners;
using Interfaces;

namespace VkDataCollector;

public class VkDataCollector : IDataCollector
{
    private readonly VkApi _vkApi;
    private readonly List<Scanner> _postScanners;
    private readonly DataManager _dataSender;
    private Config? _config;

    public VkDataCollector() => (_vkApi, _postScanners, _dataSender) = (new VkApi(), new List<Scanner>(), new DataManager());
    

    public void Subscribe(Action<ICommentData> handler)
    {
        _dataSender.OnNewDataArrivedEvent += handler.Invoke;
    }

    public void Configure(Config configure) => _config = configure;

    public void AddCommunity(long communityId)
    {
        if (_config is null) throw new ArgumentException("Configuration is missing");
        _postScanners.Add(new PostScanner(communityId, _vkApi, _dataSender, _config));
    }

    public void StartCollecting()
    {
        if (_config is null) throw new ArgumentException("Configuration is missing");
        var a = _vkApi.Auth(_config.ApplicationId, _config.SecureKey, _config.ServiceAccessKey);
        foreach (var scanner in _postScanners) scanner.StartScan();
        Console.WriteLine("Data collection has begun");
    }

    public void StopCollecting()
    {
        foreach (var scanner in _postScanners) scanner.StopScan();
        var a = _vkApi.LogOut();
        Console.WriteLine("Data collection has stopped");
    }
}