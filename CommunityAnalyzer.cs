namespace VkAPITester;

public class CommunityAnalyzer
{
    private readonly PostsQueue _queue;
    private readonly long _communityId;
    private readonly ApiClient _apiClient;
    private readonly CancellationTokenSource _tokenSource;
    private readonly Storage _storage;

    public CommunityAnalyzer(long communityId, ApiClient apiClient, Storage storage, int queueCapacity)
    {
        _queue = new PostsQueue(queueCapacity);
        _tokenSource = new CancellationTokenSource();
        _communityId = communityId;
        _apiClient = apiClient;
        _storage = storage;
    }

    public async Task Start()
    {
        await Task.Run(async () =>
        {
            while (!_tokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    var postId = _apiClient.GetPostId(1, _communityId).Result;
                    if (!_queue.Contains(postId) && postId != 0)
                    {
                        var dataCollector = new DataCollector(_apiClient, postId, _communityId);
                        _storage.AddRange(dataCollector.GetPresentComments());
                        await dataCollector.WaitOtherComments(_storage);
                        _queue.AddDataSource(dataCollector);
                    }
                    Thread.Sleep(60000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message, e.StackTrace);
                    break;
                }
            }
            _queue.Clear();
        }, _tokenSource.Token);
    }

    public void Stop()
    {
        _tokenSource.Cancel();
        _queue.Clear();
    }
}