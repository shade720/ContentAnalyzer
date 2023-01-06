using Common.EntityFramework;
using DataCollectionService.BusinessLogicLayer.SocialNetworkClients.VkClient.Data;
using DataCollectionService.BusinessLogicLayer.SocialNetworkClients.VkClient.Scanners;
using Serilog;

namespace DataCollectionService.BusinessLogicLayer.SocialNetworkClients.VkClient;

public class VkClient : SocialNetworkClient
{
    private readonly VkApi _vkApi;
    private readonly List<PostScanner> _postScanners;
    private readonly CommentDataManager _commentManager;
    private IConfiguration _configuration;

    public VkClient()
    {
        _vkApi = new VkApi();
        _postScanners = new List<PostScanner>();
        _commentManager = new CommentDataManager();
    }

    public override void Subscribe(Action<Comment> handler) => _commentManager.OnNewCommentFoundEvent += handler.Invoke;
    public override void Unsubscribe(Action<Comment> handler) => _commentManager.OnNewCommentFoundEvent -= handler.Invoke;

    public void Configure(IConfiguration configuration) => _configuration = configuration;

    public override void AddCommunity(long communityId)
    {
        //Each community has a post scanner.
        _postScanners.Add(new PostScanner(communityId, _vkApi, _commentManager, _configuration));
    }

    public override void StartCollecting()
    {
        var vkSettings = _configuration.GetSection("VkSettings");
        Task.Run(() => _vkApi.AuthAsync(ulong.Parse(vkSettings["ApplicationId"]), vkSettings["SecureKey"], vkSettings["ServiceAccessKey"]));
        foreach (var scanner in _postScanners)
        {
            scanner.StartScan();
        }
        Log.Logger.Information("Data collection has started");
    }

    public override void StopCollecting()
    {
        foreach (var scanner in _postScanners) scanner.StopScan();
        while (_postScanners.Any(x => x.IsScanning))
            Thread.Sleep(1000);
        var result = _vkApi.LogOutAsync();
        Log.Logger.Information("Data collection has stopped");
    }
}