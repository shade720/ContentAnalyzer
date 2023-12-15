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
    public Dictionary<string, Queue<VkCommentObserver>> CommentsObserversByPost { get; }

    public event ICollector.OnCommentCollected? OnCommentCollectedEvent;

    public VkCollector(IVkApi vkApi, IConfiguration configuration)
    {
        _vkApi = vkApi;
        _configuration = configuration;

        PostObservers = new List<VkPostObserver>();
        CommentsObserversByPost = new Dictionary<string, Queue<VkCommentObserver>>();
    }

    public void StartCollecting()
    {
        var communitiesIds = _configuration.GetSection("VkSettings").GetSection("Communities").Get<List<string>>();
        foreach (var communityId in communitiesIds)
        {
            var postObserver = new VkPostObserver(long.Parse(communityId), _vkApi, _configuration);
            postObserver.OnNewInfoEvent += OnNewPostEventHandler;
            PostObservers.Add(postObserver);
            CommentsObserversByPost.Add(communityId, new Queue<VkCommentObserver>());
        }
        Log.Logger.Information("Data collection has started");
    }

    public void StopCollecting()
    {
        foreach (var observersQueue in CommentsObserversByPost)
        {
            foreach (var vkCommentObserver in observersQueue.Value)
            {
                vkCommentObserver.OnNewInfoEvent -= OnNewCommentEventHandler;
                vkCommentObserver.Dispose();
            }
            observersQueue.Value.Clear();
        }
        CommentsObserversByPost.Clear();

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
        if (CommentsObserversByPost[postObserver.CommunityId.ToString()].Count == int.Parse(_configuration["PostQueueSize"]))
        {
            var commentObserverToStop = CommentsObserversByPost[postObserver.CommunityId.ToString()].Dequeue();
            commentObserverToStop.Dispose();
        }
        CommentsObserversByPost[postObserver.CommunityId.ToString()].Enqueue(commentObserver);
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