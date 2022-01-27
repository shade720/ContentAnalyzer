﻿using VkAPITester.Models.Storages;
using VkNet.Enums;
using VkNet.Model;

namespace VkAPITester.Models.VkDataCollector;

public class CommentScanner : Scanner
{
    public long PostId { get; }
    
    private readonly Dictionary<long, long?> _receivedCommentIds = new();

    public CommentScanner(long communityId, long postId, VkApi vkApi, IStorage storage, Config configuration) : base(
        communityId, vkApi, storage, new CancellationTokenSource(), configuration) =>
        PostId = postId;

    public override void StartScan()
    {
        Storage.AddRange(DictionaryStorage.ConvertAll(GetPresentComments()));
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
        foreach (var comment in comments) Console.WriteLine($"Add {comment.Id} {comment.PostId} {comment.OwnerId} {comment.FromId} {comment.Text} {comment.Date}");
        return comments;
    }

    private async Task ScanComments()
    {
        await Task.Run(() =>
        {
            while (!StopScanToken.IsCancellationRequested)
            {
                Thread.Sleep(Configuration.ScanCommentsDelay);
                if (StopScanToken.IsCancellationRequested ||
                    _receivedCommentIds.Count >= ClientApi.GetCommentsCount(CommunityId, PostId).Result) continue;

                ScanBranch(out var mainBranch);
                foreach (var comment in mainBranch.Items.Where(x => x.Thread.Count > _receivedCommentIds[x.Id]))
                    ScanBranch(out _, comment.Id);
            }
        }, StopScanToken.Token);
        Console.WriteLine("Scan comments stopped");
    }

    private void ScanBranch(out WallGetCommentsResult branch, long? commentId = null)
    {
        branch = ClientApi.GetComments(PostId, 100, CommunityId, 0, SortOrderBy.Asc, commentId).Result;
        if (branch.Count == 0) return;

        var sortedBranch = branch.Items.Reverse().ToArray();
        for (var i = 0; i < sortedBranch.Length && !_receivedCommentIds.ContainsKey(sortedBranch[i].Id); i++)
        {
            var comment = sortedBranch[i];
            Storage.Add(DictionaryStorage.Convert(comment));
            Console.WriteLine($"Add {comment.Id} {comment.PostId} {comment.OwnerId} {comment.FromId} {comment.Text} {comment.Date}");
            _receivedCommentIds.TryAdd(comment.Id, comment.Thread is null ? 0 : comment.Thread.Count);
        }
    }

    private List<Comment> GetBranch(long? commentId = null)
    {
        var comments = new List<Comment>();
        long count = 100;
        for (var i = 0; i < count; i += 100)
        {
            var reply = ClientApi.GetComments(PostId, 100, CommunityId, i, SortOrderBy.Asc, commentId).Result;
            comments.AddRange(reply.Items);
            foreach (var comment in reply.Items) _receivedCommentIds.TryAdd(comment.Id, comment.Thread?.Count ?? 0);
            count = reply.Count;
        }
        return comments;
    }
}