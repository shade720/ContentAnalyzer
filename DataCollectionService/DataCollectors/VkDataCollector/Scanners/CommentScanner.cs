using System.Runtime.ExceptionServices;
using DataCollectionService.DataCollectors.VkDataCollector.Data;
using Serilog;
using VkNet.Enums;
using VkNet.Model;

namespace DataCollectionService.DataCollectors.VkDataCollector.Scanners;

internal class CommentScanner : Scanner
{
    public long PostId { get; }

    private readonly Dictionary<long, long?> _receivedCommentIds = new();

    public CommentScanner(long communityId, long postId, VkApi vkApi, CommentDataManager dataManager, Config configuration) : base(
        communityId, vkApi, dataManager, configuration) => PostId = postId;

    public override void StartScan()
    {
        try
        {
            StopScanToken = new CancellationTokenSource();
            var presentComments = GetPresentComments();
            CommentManager.SendAllData(presentComments);
            var scanResult = ScanComments();
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

    private IEnumerable<Comment> GetPresentComments()
    {
        try
        {
            var comments = GetBranch();
            var additionalComments = new List<Comment>();
            foreach (var comment in comments.Where(comment => comment.Thread.Count > 0))
                additionalComments.AddRange(GetBranch(comment.Id));
            comments.AddRange(additionalComments);
            Log.Logger.Information("Added presents comments on post {0}", PostId);
            return comments;
        }
        catch (Exception e)
        {
            ExceptionDispatchInfo.Capture(e.InnerException).Throw();
            throw;
        }
    }

    private async Task ScanComments()
    {
        try
        {
            Log.Logger.Information("Start scanning comments on post {0}", PostId);
            await Task.Run(async () =>
            {
                while (!StopScanToken.Token.IsCancellationRequested)
                {
                    await Task.Delay(Configuration.ScanCommentsDelay, StopScanToken.Token);
                    if (StopScanToken.IsCancellationRequested || !AnyNewComments()) continue;

                    ScanBranch(out var mainBranch);
                    foreach (var comment in mainBranch.Items.Where(x => x.Thread.Count > _receivedCommentIds[x.Id]))
                        ScanBranch(out _, comment.Id);
                }
            }, StopScanToken.Token);
            Log.Logger.Information("Scan comments stopped on post {0}", PostId);
        }
        catch (Exception e)
        {
            ExceptionDispatchInfo.Capture(e.InnerException).Throw();
            throw;
        }
    }

    private bool AnyNewComments()
    {
        return _receivedCommentIds.Count < ClientApi.GetCommentsCountAsync(CommunityId, PostId).Result;
    }

    private void ScanBranch(out WallGetCommentsResult branch, long? commentId = null)
    {
        branch = ClientApi.GetCommentsAsync(PostId, 100, CommunityId, 0, SortOrderBy.Asc, commentId).Result;
        if (branch.Count == 0) return;

        var sortedBranch = branch.Items.Reverse().ToArray();
        for (var i = 0; i < sortedBranch.Length && !_receivedCommentIds.ContainsKey(sortedBranch[i].Id); i++)
        {
            var comment = sortedBranch[i];
            CommentManager.SendData(comment);
            _receivedCommentIds.TryAdd(comment.Id, comment.Thread is null ? 0 : comment.Thread.Count);
        }
    }

    private List<Comment> GetBranch(long? commentId = null)
    {
        var comments = new List<Comment>();
        long startCount = 100;
        for (var offset = 0; offset < startCount; offset += 100)
        {
            var reply = ClientApi.GetCommentsAsync(PostId, 100, CommunityId, offset, SortOrderBy.Asc, commentId).Result;
            comments.AddRange(reply.Items);
            foreach (var comment in reply.Items) _receivedCommentIds.TryAdd(comment.Id, comment.Thread?.Count ?? 0);
            startCount = reply.Count;
        }
        return comments;
    }
}