using System.Runtime.ExceptionServices;
using DataCollectionService.DataCollectors.VkDataCollector.Data;
using Serilog;
using VkNet.Enums;
using VkNet.Model;

namespace DataCollectionService.DataCollectors.VkDataCollector.Scanners;

internal class CommentScanner : Scanner
{
    public long PostId { get; }
    private static readonly List<CommentInfo> ReceivedCommentsInfos = new();

    public CommentScanner(long communityId, long postId, VkApi vkApi, CommentDataManager dataManager,
        Config configuration) : base(
        communityId, vkApi, dataManager, configuration)
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
            CommentManager.SendAllData(presentComments);
            await ScanComments();
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
        try
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
        catch (Exception e)
        {
            ExceptionDispatchInfo.Capture(e.InnerException ?? e).Throw();
            throw;
        }
    }

    private static bool CommentHasThreadComments(Comment comment)
    {
        return comment.Thread.Count > 0;
    }

    private async Task ScanComments()
    {
        try
        {
            Log.Logger.Information("Start scanning comments on post {0}", PostId);
            while (!StopScanToken.Token.IsCancellationRequested)
            {
                Log.Logger.Information("Comment scanner post {@PostId} started new iteration", PostId);
                await Task.Delay(Configuration.ScanCommentsDelay, StopScanToken.Token).ContinueWith(_ => { }); //to avoid exception
                if (StopScanToken.IsCancellationRequested || !await AnyNewComments()) continue;
                var mainBranch = await ScanBranchAsync();
                CommentManager.SendAllData(mainBranch);
                foreach (var comment in mainBranch.Where(CommentsCountOfThreadIncreased))
                {
                    var threadComments = await ScanBranchAsync(comment.Id);
                    CommentManager.SendAllData(threadComments);
                }
            }
            ReceivedCommentsInfos.Clear();
            Log.Logger.Information("Scan comments stopped on post {0}", PostId);
        }
        catch (Exception e)
        {
            ExceptionDispatchInfo.Capture(e.InnerException ?? e).Throw();
            throw;
        }
    }

    private static bool CommentsCountOfThreadIncreased(Comment comment)
    {
        return comment.Thread.Count > ReceivedCommentsInfos.Single(info => info.CommentId == comment.Id).CommentsInThreadCount;
    }

    private async Task<bool> AnyNewComments()
    {
        var receivedCommentsCount = ReceivedCommentsInfos.Count + ReceivedCommentsInfos.Count(info => info.CommentsInThreadCount > 0);
        return receivedCommentsCount < await ClientApi.GetCommentsCountAsync(CommunityId, PostId);
    }
    private static bool IsNewComment(long commentId)
    {
        return ReceivedCommentsInfos.Exists(info => info.CommentId == commentId);
    }

    private async Task<Comment[]> ScanBranchAsync(long? commentId = null)
    {
        var  branch = await ClientApi.GetCommentsAsync(PostId, 100, CommunityId, 0, SortOrderBy.Asc, commentId);
        if (branch.Count == 0) return Array.Empty<Comment>();
        var sortedBranch = branch.Items.Reverse().ToArray();
        for (var i = 0; i < sortedBranch.Length && IsNewComment(sortedBranch[i].Id); i++)
        {
            var comment = sortedBranch[i];
            ReceivedCommentsInfos.Add(new CommentInfo{ CommentId = comment.Id, CommentsInThreadCount = comment.Thread?.Count ?? 0 });
        }
        return sortedBranch;
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
                ReceivedCommentsInfos.Add(new CommentInfo { CommentId = comment.Id, CommentsInThreadCount = comment.Thread?.Count ?? 0 });
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

