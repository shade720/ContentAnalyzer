using DataCollectionService.DataCollectors.VkDataCollector.Data;
using DataCollectionService.DataCollectors.VkDataCollector.ScannerManager;
using Serilog;

namespace DataCollectionService.DataCollectors.VkDataCollector.Scanners;

internal class PostScanner : Scanner
{
    private readonly CommentScannersQueue _commentScannersQueue;
    public bool IsScanning { get; private set; }

    public PostScanner(long communityId, VkApi vkApi, CommentDataManager dataManager, Config configuration) : base(communityId, vkApi,
        dataManager, configuration) => _commentScannersQueue = new CommentScannersQueue(configuration.ObservedPostQueueSize);

    public override void StartScan()
    {
        Task.Run(StartScanAsync);
    }

    private async Task StartScanAsync()
    {
        try
        {
            StopScanToken = new CancellationTokenSource();
            IsScanning = true;
            while (!StopScanToken.Token.IsCancellationRequested)
            {
                var newPostId = await IsThereNewPost();
                if (newPostId is not null)
                {
                    Log.Logger.Information("New post released {@PostId} community {@CommunityId}", newPostId, CommunityId);
                    //Each post has a comment scanner, which is added to the queue which is added to the queue for convenient stopping and deleting 
                    _commentScannersQueue.AddScanner(new CommentScanner(CommunityId, newPostId.Value, ClientApi, CommentManager, Configuration));
                }
                await Task.Delay(Configuration.ScanPostDelay, StopScanToken.Token).ContinueWith(_ => { }); //to avoid exception
                Log.Logger.Information("Post scanner {@CommunityId} started new iteration", CommunityId);
            }
            Log.Logger.Information("Post scanning {@CommunityId} stopped", CommunityId);
            _commentScannersQueue.Clear();
            IsScanning = false;
        }
        catch (Exception e)
        {
            Log.Logger.Error("Error in post scanner {@CommunityId} {@Exception}", CommunityId, e.Message + "\r\n" + e.InnerException);
            _commentScannersQueue.Clear();
        }
    }

    private async Task<long?> IsThereNewPost()
    {
        var postId = await ClientApi.GetPostIdAsync(CommunityId, 1);
        return !_commentScannersQueue.Contains(postId) ? postId : null;
    }

    public override void StopScan()
    {
        StopScanToken.Cancel();
    }
}