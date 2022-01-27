using VkAPITester.Models.Storages;

namespace VkAPITester.Models.VkDataCollector;

public class PostScanner
{
    private readonly long _communityId;
    private readonly CommentScannersQueue _commentScannersQueue;

    private readonly CancellationTokenSource _stopScanToken;

    private readonly IStorage _storage;
    private readonly VkApi _vkApi;
    private readonly Config _configuration;

    public PostScanner(long communityId, VkApi vkApi, IStorage storage, Config configuration) =>
        (_commentScannersQueue, _stopScanToken, _communityId, _vkApi, _storage, _configuration) = (new CommentScannersQueue(configuration.QueueSize),
            new CancellationTokenSource(), communityId, vkApi, storage, configuration);
    
    public async Task StartScan()
    {
        await Task.Run(() =>
        {
            while (!_stopScanToken.Token.IsCancellationRequested)
            {
                if (IsNewPost(out var postId))
                {
                    Console.WriteLine($"New post released {postId} group {_communityId}");
                    _commentScannersQueue.AddScanner(new CommentScanner(_communityId, postId, _vkApi, _storage, _configuration));
                }
                Thread.Sleep(_configuration.ScanPostDelay);
            }
            StopScan();
        }, _stopScanToken.Token);
    }

    private bool IsNewPost(out long postId)
    {
        postId = _vkApi.GetPostId(1, _communityId).Result;
        return !_commentScannersQueue.Contains(postId) && postId != 0;
    }

    public void StopScan()
    {
        _stopScanToken.Cancel();
        _commentScannersQueue.Clear();
    }
}