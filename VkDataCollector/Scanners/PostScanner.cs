using VkDataCollector.ScannerManager;
using Common;

namespace VkDataCollector.Scanners;

internal class PostScanner : Scanner
{
    private readonly CommentScannersQueue _commentScannersQueue;

    public PostScanner(long communityId, VkApi vkApi, Data.DataManager dataManager, Config configuration) : base(communityId, vkApi,
        dataManager, configuration) =>
        _commentScannersQueue = new CommentScannersQueue(configuration.QueueSize);

    public override void StartScan()
    {
        var result = StartScanAsync();
    }

    private async Task StartScanAsync()
    {
        StopScanToken = new CancellationTokenSource();
        await Task.Run( async () =>
        {
            while (!StopScanToken.Token.IsCancellationRequested)
            {
                if (IsNewPost(out var postId))
                { 
                    Logger.Write($"New post released {postId} group {CommunityId}");
                    _commentScannersQueue.AddScanner(new CommentScanner(CommunityId, postId, ClientApi, DataManager, Configuration));
                }
                await Task.Delay(Configuration.ScanPostDelay, StopScanToken.Token);
            }
            StopScan();
        }, StopScanToken.Token);
    }

    private bool IsNewPost(out long postId)
    {
        postId = ClientApi.GetPostId(1, CommunityId).Result;
        return !_commentScannersQueue.Contains(postId) && postId != 0;
    }

    public override void StopScan()
    {
        StopScanToken.Cancel();
        Logger.Write("Stop post scanning");
        _commentScannersQueue.Clear();
    }
}