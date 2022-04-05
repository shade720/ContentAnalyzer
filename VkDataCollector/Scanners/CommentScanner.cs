using VkDataCollector.Data;
using VkNet.Enums;
using VkNet.Model;
using Common;

namespace VkDataCollector.Scanners;

internal class CommentScanner : Scanner
{
    public long PostId { get; }
    
    private readonly Dictionary<long, long?> _receivedCommentIds = new();

    public CommentScanner(long communityId, long postId, VkApi vkApi, CommentDataManager dataManager, Config configuration) : base(
        communityId, vkApi, dataManager, configuration) => PostId = postId;

    public override void StartScan()
    {
        StopScanToken = new CancellationTokenSource();
        var presentComments = GetPresentComments();
        CommentManager.SendAllData(presentComments);
        var scanResult = ScanComments();
    }

    public override void StopScan()
    {
        StopScanToken.Cancel();
    }

    private IEnumerable<Comment> GetPresentComments()
    {
        var comments = GetBranch();

        var additionalComments = new List<Comment>();
        foreach (var comment in comments.Where(comment => comment.Thread.Count > 0))
            additionalComments.AddRange(GetBranch(comment.Id));
        comments.AddRange(additionalComments);
        foreach (var comment in comments) Logger.Log($"Add {comment.Id} {comment.PostId} {comment.OwnerId} {comment.FromId} {comment.Text} {comment.Date}", Logger.LogLevel.Information);
        Logger.Log("Added presents comments", Logger.LogLevel.Information);
        return comments;
    }

    private async Task ScanComments()
    {
        Logger.Log("Start scanning comments", Logger.LogLevel.Information);
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
        Logger.Log("Scan comments stopped", Logger.LogLevel.Information);
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