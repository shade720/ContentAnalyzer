﻿using DataCollectionService.Domain;
using DataCollectionService.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace DataCollectionService.Application.VkObservers;

public class VkCommentObserver : VkObserver<VkComment>, IDisposable
{
    private readonly long _communityId;
    private readonly long _postId;

    private readonly Dictionary<long, long> _threadsCounts;

    public override event OnVkNewInfo? OnNewInfoEvent;

    public VkCommentObserver(
        long communityId, long postId, 
        IVkApi vkApi, 
        IConfiguration configuration)
        : base(vkApi, configuration)
    {
        _communityId = communityId;
        _postId = postId;
        _threadsCounts = new Dictionary<long, long>();
        Task.Run(async () => await PullingCommentsLoop());
        Log.Logger.Information("Comments observer has been created for post {0}", _postId);
    }

    private async Task PullingCommentsLoop()
    {
        if (Configuration["ScanCommentsDelay"] is null)
            throw new ApplicationException($"ScanCommentsDelay in {nameof(PullingCommentsLoop)} was null!");
        
        while (!CancellationTokenSource.IsCancellationRequested)
        {
            await Task.Delay(int.Parse(Configuration["ScanCommentsDelay"]!), CancellationTokenSource.Token)
                .ContinueWith(_ => { }); //to avoid exception

            Log.Logger.Information("Comment observer has started new iteration for post {0}", _postId);

            if (CancellationTokenSource.IsCancellationRequested || !await IsThereNewComments() || OnNewInfoEvent is null)
                continue;

            await foreach (var comment in GetBranch())
            {
                if (!_threadsCounts.ContainsKey(comment.CommentId))
                    _threadsCounts.Add(comment.CommentId, 0);

                await OnNewInfoEvent(this, comment);

                if (comment.ThreadCommentsCount <= _threadsCounts[comment.CommentId])
                    continue;

                await foreach (var threadComment in GetBranch(comment.CommentId))
                {
                    _threadsCounts[comment.CommentId] += 1;
                    await OnNewInfoEvent(this, threadComment);
                }
            }
        }

        Log.Logger.Information("Comments observing stopped on post {0}", _postId);
    }

    private async IAsyncEnumerable<VkComment> GetBranch(long? commentId = null)
    {
        const int batchSize = 100;

        for (var offset = 0; ; offset += batchSize)
        {
            var commentsBatch = (await VkApi.GetCommentsAsync(_communityId, _postId, batchSize, offset, commentId)).ToList();

            if (commentsBatch.Count == 0)
                yield break;

            foreach (var comment in commentsBatch.Where(comment => !CollectedIds.Contains(comment.CommentId)))
            {
                CollectedIds.Add(comment.CommentId);
                yield return comment;
            }
        }
    }

    private async Task<bool> IsThereNewComments()
    {
        var receivedCommentsCount = CollectedIds.Count + _threadsCounts.Sum(info => info.Value);
        var currentCommentsCount = await VkApi.GetCommentsCountAsync(_communityId, _postId);
        return receivedCommentsCount < currentCommentsCount;
    }

    public void Dispose()
    {
        CancellationTokenSource?.Cancel();
        OnNewInfoEvent = null;
        Log.Logger.Information("Comments observer have been disposed (post {0})", _postId);
    }
}