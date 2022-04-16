using Common;
using VkDataCollector.ScannerManager;

namespace VkDataCollector.Scanners;

internal class PostScanner : Scanner
{
    private readonly CommentScannersQueue _commentScannersQueue;

    public PostScanner(long communityId, VkApi vkApi, Data.CommentDataManager dataManager, Config configuration) : base(communityId, vkApi,
        dataManager, configuration) => _commentScannersQueue = new CommentScannersQueue(configuration.QueueSize);

    public override void StartScan()
    {
        var result = StartScanAsync();
    }

    private async Task StartScanAsync()
    {
        try
        {
            StopScanToken = new CancellationTokenSource();
            await Task.Run(async () =>
            {
                while (!StopScanToken.Token.IsCancellationRequested)
                {
                    if (IsThereNewPost(out var postId))
                    {
                        Logger.Log($"New post released {postId} community {CommunityId}", Logger.LogLevel.Information);
                        //Each post has a comment scanner, which is added to the queue which is added to the queue for convenient stopping and deleting 
                        _commentScannersQueue.AddScanner(new CommentScanner(CommunityId, postId, ClientApi, CommentManager, Configuration));
                    }
                    await Task.Delay(Configuration.ScanPostDelay, StopScanToken.Token);
                }
                StopScan();
            }, StopScanToken.Token);
        }
        catch (Exception e)
        {
            Logger.Log($"Error in post scanner {CommunityId}" + e.Message + "\r\n" + e.InnerException, Logger.LogLevel.Error);
            StopScan();
        }
    }

    private bool IsThereNewPost(out long postId)
    {
        postId = ClientApi.GetPostIdAsync(CommunityId, 1).Result;
        return !_commentScannersQueue.Contains(postId) && postId != 0;
    }

    public override void StopScan()
    {
        StopScanToken.Cancel();
        _commentScannersQueue.Clear();
        Logger.Log($"Post scanning {CommunityId} stopped", Logger.LogLevel.Information);
    }
}