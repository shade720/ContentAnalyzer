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

    public List<VkPostObserver> PostObservers { get; }
    public FixedQueue<VkCommentObserver> CommentsObservers { get; }

    public event ICollector.OnCommentCollected? OnCommentCollectedEvent;

    public VkCollector(IVkApi vkApi, IConfiguration configuration)
    {
        _vkApi = vkApi;
        _configuration = configuration;

        PostObservers = new List<VkPostObserver>();
        CommentsObservers = new FixedQueue<VkCommentObserver>(int.Parse(configuration["PostQueueSize"]));
    }

    public void StartCollecting()
    {
        var communitiesIds = _configuration.GetSection("VkSettings").GetSection("Communities").Get<List<string>>();
        foreach (var communityId in communitiesIds)
        {
            var postObserver = new VkPostObserver(long.Parse(communityId), _vkApi, _configuration);
            postObserver.OnNewInfoEvent += OnNewPostEventHandler;
            PostObservers.Add(postObserver);
        }
        Log.Logger.Information("Data collection has started");
    }

    public void StopCollecting()
    {
        foreach (var commentObserver in CommentsObservers)
        {
            commentObserver.OnNewInfoEvent -= OnNewCommentEventHandler;
            commentObserver.Dispose();
        }
        CommentsObservers.Clear();

        foreach (var postObserver in PostObservers)
        {
            postObserver.OnNewInfoEvent -= OnNewPostEventHandler;
            postObserver.Dispose();
        }
        PostObservers.Clear();

        Log.Logger.Information("Data collection has stopped");
    }

    private void OnNewPostEventHandler(object? sender, long postId)
    {
        if (sender is not VkPostObserver postObserver)
            throw new ArgumentException($"Incorrect sender {nameof(OnNewPostEventHandler)}, expected VkPostObserver");
        var commentObserver = new VkCommentObserver(postObserver.CommunityId, postId, _vkApi, _configuration);
        commentObserver.OnNewInfoEvent += OnNewCommentEventHandler;
        CommentsObservers.Enqueue(commentObserver);
    }

    private void OnNewCommentEventHandler(object? sender, VkComment comment)
    {
        if (OnCommentCollectedEvent is null)
        {
            Log.Warning($"{nameof(VkCollector)} doesn't have a handler");
            return;
        }
        OnCommentCollectedEvent(comment);
    }

    public async ValueTask DisposeAsync()
    {
        await _vkApi.LogOutAsync();
    }
}