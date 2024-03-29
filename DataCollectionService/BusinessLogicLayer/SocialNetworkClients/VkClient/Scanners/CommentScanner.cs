﻿using Serilog;
using VkNet.Enums;
using VkNet.Model;
using DataCollectionService.BusinessLogicLayer.SocialNetworkClients.VkClient.Data;
using DataCollectionService.BusinessLogicLayer.SocialNetworkClients.VkClient;

namespace DataCollectionService.BusinessLogicLayer.SocialNetworkClients.VkClient.Scanners;

internal class CommentScanner : Scanner
{
    public long PostId { get; }
    private readonly List<CommentInfo> _receivedCommentInfos = new();

    public CommentScanner(long communityId, long postId, VkApi vkApi, CommentDataManager dataManager, IConfiguration configuration) : base(communityId, vkApi, dataManager, configuration)
    {
        PostId = postId;
    }
    public override void StartScan()
    {
        Task.Run(StartScanAsync);
    }

    public async Task StartScanAsync()
    {
        try
        {
            StopScanToken = new CancellationTokenSource();
            var presentComments = await GetPresentComments();
            CommentManager.SendAllCommentData(presentComments);
            await StartCommentScanning();
        }
        catch (Exception e)
        {
            Log.Logger.Error("Comment scanner stopped with error {0}", e.Message + "\r\n" + e.InnerException);
            StopScan();
        }
    }

    public override void StopScan()
    {
        StopScanToken.Cancel();
    }

    private async Task<IEnumerable<Comment>> GetPresentComments()
    {
        var comments = await GetBranch();
        var additionalComments = new List<Comment>();
        foreach (var comment in comments.Where(CommentHasThreadComments))
        {
            var threadComments = await GetBranch(comment.Id);
            additionalComments.AddRange(threadComments);
        }
        comments.AddRange(additionalComments);
        Log.Logger.Information("Added presents comments on post {0}", PostId);
        return comments;
    }

    private static bool CommentHasThreadComments(Comment comment) => comment.Thread.Count > 0;

    private async Task StartCommentScanning()
    {
        Log.Logger.Information("Start scanning comments on post {0}", PostId);
        while (!StopScanToken.Token.IsCancellationRequested)
        {
            Log.Logger.Information("Comment scanner post {@PostId} started new iteration", PostId);
            await Task.Delay(int.Parse(Configuration["ScanCommentsDelay"]), StopScanToken.Token).ContinueWith(_ => { }); //to avoid exception
            var anyNewComments = await AnyNewComments();
            if (StopScanToken.IsCancellationRequested || !anyNewComments) continue;
            var mainBranch = await ScanBranchAsync();
            CommentManager.SendAllCommentData(mainBranch);
            foreach (var comment in mainBranch.Where(CommentsCountOfThreadIncreased))
            {
                var threadComments = await ScanBranchAsync(comment.Id);
                CommentManager.SendAllCommentData(threadComments);
            }
        }
        _receivedCommentInfos.Clear();
        Log.Logger.Information("Scan comments stopped on post {0}", PostId);
    }

    private bool CommentsCountOfThreadIncreased(Comment comment)
    {
        var info = _receivedCommentInfos.SingleOrDefault(info => info.CommentId == comment.Id);
        if (info == default(CommentInfo)) return false;
        return comment.Thread.Count > info.CommentsInThreadCount;
    }

    private async Task<bool> AnyNewComments()
    {
        var receivedCommentsCount = _receivedCommentInfos.Count + _receivedCommentInfos.Count(info => info.CommentsInThreadCount > 0);
        var currentCommentsCount = await ClientApi.GetCommentsCountAsync(CommunityId, PostId);
        return receivedCommentsCount < currentCommentsCount;
    }
    private bool IsNewComment(long commentId)
    {
        return !_receivedCommentInfos.Exists(info => info.CommentId == commentId);
    }

    private async Task<Comment[]> ScanBranchAsync(long? commentId = null)
    {
        var branch = await ClientApi.GetCommentsAsync(PostId, 100, CommunityId, 0, SortOrderBy.Asc, commentId);
        if (branch.Count == 0) return Array.Empty<Comment>();
        var sortedBranch = branch.Items.Reverse().ToArray();
        var resultBranch = new List<Comment>();
        foreach (var comment in sortedBranch)
        {
            if (!IsNewComment(comment.Id)) break;
            _receivedCommentInfos.Add(new CommentInfo { CommentId = comment.Id, CommentsInThreadCount = comment.Thread?.Count ?? 0 });
            resultBranch.Add(comment);
        }
        return resultBranch.ToArray();
    }

    private async Task<List<Comment>> GetBranch(long? commentId = null)
    {
        var comments = new List<Comment>();
        long startCount = 100;
        for (var offset = 0; offset < startCount; offset += 100)
        {
            var reply = await ClientApi.GetCommentsAsync(PostId, 100, CommunityId, offset, SortOrderBy.Asc, commentId);
            comments.AddRange(reply.Items);
            foreach (var comment in reply.Items)
            {
                _receivedCommentInfos.Add(new CommentInfo { CommentId = comment.Id, CommentsInThreadCount = comment.Thread?.Count ?? 0 });
            }
            startCount = reply.Count;
        }
        return comments;
    }
    private class CommentInfo
    {
        public long CommentId { get; init; }
        public long CommentsInThreadCount { get; init; }
    }
}

