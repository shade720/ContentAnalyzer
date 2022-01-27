using VkAPITester.Models.Storages;
using VkNet.Enums;
using VkNet.Model;

namespace VkAPITester.Models.VkDataCollector;

public class CommentScanner
{
    public long PostId { get; }
    private long GroupId { get; }

    private readonly VkApi _client;
    private readonly IStorage _storage;

    private readonly CancellationTokenSource _stopScanToken;
    private readonly Config _configuration;

    private readonly Dictionary<long, long?> _receivedCommentIds = new();

    public CommentScanner(long groupId, long postId, VkApi client, IStorage storage, Config configuration) =>
        (GroupId, PostId, _client, _storage, _stopScanToken, _configuration) = (groupId, postId, client, storage,
            new CancellationTokenSource(), configuration);

    public void StartScan()
    {
        _storage.AddRange(DictionaryStorage.ConvertAll(GetPresentComments()));
        var scanResult = ScanComments();
    }

    public void StopScan()
    {
        _stopScanToken.Cancel();
    }

    private List<Comment> GetPresentComments()
    {
        var comments = GetBranch();

        var additionalComments = new List<Comment>();
        foreach (var comment in comments.Where(comment => comment.Thread.Count > 0))
            additionalComments.AddRange(GetBranch(comment.Id));
        comments.AddRange(additionalComments);

        foreach (var comment in comments)
            Console.WriteLine(
                $"add {comment.Id} {comment.PostId} {comment.OwnerId} {comment.FromId} {comment.Text} {comment.Date}");
        return comments;
    }

    private async Task ScanComments()
    {
        await Task.Run(() =>
        {
            while (!_stopScanToken.IsCancellationRequested)
            {
                Thread.Sleep(_configuration.ScanCommentsDelay);
                if (_stopScanToken.IsCancellationRequested ||
                    _receivedCommentIds.Count >= _client.GetCommentsCount(GroupId, PostId).Result) continue;

                ScanBranch(out var mainBranch);
                foreach (var comment in mainBranch.Items.Where(x => x.Thread.Count > _receivedCommentIds[x.Id]))
                    ScanBranch(out _, comment.Id);
            }

            Console.WriteLine($"Unsubscribe {PostId}");
        }, _stopScanToken.Token);
        Console.WriteLine("Exit from method");
    }

    private void ScanBranch(out WallGetCommentsResult branch, long? commentId = null)
    {
        branch = _client.GetComments(PostId, 100, GroupId, 0, SortOrderBy.Asc, commentId).Result;
        if (branch.Count == 0) return;

        var sortedBranch = branch.Items.Reverse().ToArray();
        for (var i = 0; i < sortedBranch.Length && !_receivedCommentIds.ContainsKey(sortedBranch[i].Id); i++)
        {
            var comment = sortedBranch[i];
            _storage.Add(DictionaryStorage.Convert(comment));
            Console.WriteLine(
                $"add {comment.Id} {comment.PostId} {comment.OwnerId} {comment.FromId} {comment.Text} {comment.Date}");
            _receivedCommentIds.TryAdd(comment.Id, comment.Thread is null ? 0 : comment.Thread.Count);
        }
    }

    private List<Comment> GetBranch(long? commentId = null)
    {
        var comments = new List<Comment>();
        long count = 100;
        for (var i = 0; i < count; i += 100)
        {
            var reply = _client.GetComments(PostId, 100, GroupId, i, SortOrderBy.Asc, commentId).Result;
            comments.AddRange(reply.Items);
            foreach (var comment in reply.Items) _receivedCommentIds.TryAdd(comment.Id, comment.Thread?.Count ?? 0);
            count = reply.Count;
        }

        return comments;
    }
}