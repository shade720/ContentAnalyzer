using VkDataCollector.ScannerManager;

namespace VkDataCollector.Scanners;

internal class PostScanner : Scanner
{
    private readonly CommentScannersQueue _commentScannersQueue;

    public PostScanner(long communityId, VkApi vkApi, Data.DataManager dataManager, Config configuration) : base(communityId, vkApi,
        dataManager, new CancellationTokenSource(), configuration) =>
        _commentScannersQueue = new CommentScannersQueue(configuration.QueueSize);

    public override void StartScan()
    {
        var result = StartScanAsync();
    }

    private async Task StartScanAsync()
    {
        await Task.Run(() =>
        {
            while (!StopScanToken.Token.IsCancellationRequested)
            {
                if (IsNewPost(out var postId))
                {
                    Console.WriteLine($"New post released {postId} group {CommunityId}");
                    _commentScannersQueue.AddScanner(new CommentScanner(CommunityId, postId, ClientApi, DataManager, Configuration));
                }
                Thread.Sleep(Configuration.ScanPostDelay);
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
        Console.WriteLine("Stop post scanning");
        _commentScannersQueue.Clear();
    }
}