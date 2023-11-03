using DataCollectionService.Application.VkObservers;
using DataCollectionService.Domain;
using DataCollectionService.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace DataCollectionService.Application;

public class VkCollector : ICollector, IAsyncDisposable
{
    private readonly IVkApi _vkApi;
    private readonly IConfiguration _configuration;

    private readonly List<VkPostObserver> _postObservers;
    private readonly FixedQueue<VkCommentObserver> _commentsObservers;

    public event ICollector.OnCommentCollected? OnCommentCollectedEvent;

    public VkCollector(IVkApi vkApi, IConfiguration configuration)
    {
        _vkApi = vkApi;
        _configuration = configuration;

        _postObservers = new List<VkPostObserver>();
        _commentsObservers = new FixedQueue<VkCommentObserver>(int.Parse(configuration["PostQueueSize"]));

        var vkSettings = _configuration.GetSection("VkSettings");
        _vkApi.LogInAsync(new VkApiCredentials
        {
            ApplicationId = ulong.Parse(vkSettings["ApplicationId"]),
            SecureKey = vkSettings["SecureKey"],
            ServiceAccessKey = vkSettings["ServiceAccessKey"]
        });
    }

    public void StartCollecting()
    {
        var communitiesIds = _configuration.GetSection("VkSettings").GetSection("Communities").Get<List<string>>();
        foreach (var communityId in communitiesIds)
        {
            var postObserver = new VkPostObserver(long.Parse(communityId), _vkApi, _configuration);
            postObserver.OnNewInfoEvent += OnNewPostEventHandler;
            _postObservers.Add(postObserver);
        }
        Log.Logger.Information("Data collection has started");
    }

    public void StopCollecting()
    {
        foreach (var commentObserver in _commentsObservers)
        {
            commentObserver.OnNewInfoEvent -= OnNewCommentEventHandler;
            commentObserver.Dispose();
        }

        _commentsObservers.Clear();

        foreach (var postObserver in _postObservers)
        {
            postObserver.OnNewInfoEvent -= OnNewPostEventHandler;
            postObserver.Dispose();
        }
        
        Log.Logger.Information("Data collection has stopped");
    }

    private Task OnNewPostEventHandler(object sender, long postId)
    {
        if (sender is not VkPostObserver postObserver)
            throw new ArgumentException($"Incorrect sender {nameof(OnNewPostEventHandler)}, expected VkPostObserver");
        var commentObserver = new VkCommentObserver(postObserver.CommunityId, postId, _vkApi, _configuration);
        commentObserver.OnNewInfoEvent += OnNewCommentEventHandler;
        _commentsObservers.Enqueue(commentObserver);
        return Task.CompletedTask;
    }

    private async Task OnNewCommentEventHandler(object sender, VkComment comment)
    {
        if (OnCommentCollectedEvent is null)
        {
            Log.Warning($"{nameof(VkCollector)} doesn't have a handler");
            return;
        }
        await OnCommentCollectedEvent(comment);
    }

    public async ValueTask DisposeAsync()
    {
        await _vkApi.LogOutAsync();
    }
}