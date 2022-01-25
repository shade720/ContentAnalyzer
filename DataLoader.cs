using VkNet.Enums;
using VkNet.Model;

namespace VkAPITester
{
    public class DataLoader
    {
        public long PostId { get; }
        private readonly long _sourceId;
        
        private readonly ApiClient _client;
        private readonly Dictionary<long, long?> _receivedCommentIds = new();
        public CancellationTokenSource UnsubscribeToken = new();

        public DataLoader(ApiClient client, long sourceId, long postId) => (_client, _sourceId, PostId) = (client, sourceId, postId);

        public List<Comment> GetPresentComments()
        {
            var comments = GetBranch();

            var additionalComments = new List<Comment>();
            foreach (var comment in comments.Where(comment => comment.Thread.Count > 0)) additionalComments.AddRange(GetBranch(comment.Id));
            comments.AddRange(additionalComments);

            foreach (var comment in comments) Console.WriteLine($"add {comment.Text} {comment.Date} {comment.Id}");
            return comments;
        }

        //public async Task DownloadIncomeComments(AnalyzedDataStorage storageForRealtimeAddition)
        //{
        //    await Task.Run(() =>
        //    {
        //        while (!UnsubscribeToken.IsCancellationRequested)
        //        {
        //            Thread.Sleep(60000);
        //            Console.WriteLine($"ping pong {PostId}");
        //            Console.WriteLine($"comments {_receivedCommentIds.Count}");
        //            if (_receivedCommentIds.Count >= _client.GetCommentsCount(_sourceId, PostId)) continue;

        //            Console.WriteLine($"opa new comment");
        //            var mainBranch = _client.GetComments(PostId, 100, _sourceId, 0, SortOrderBy.Asc);
        //            foreach (var comment in mainBranch.Items)
        //            {
        //                var currentCommentId = comment.Id;
        //                if (!_receivedCommentIds.ContainsKey(currentCommentId))
        //                {
        //                    storageForRealtimeAddition.AddEntry(comment);
        //                    _receivedCommentIds.Add(comment.Id, comment.Thread.Count);
        //                    Console.WriteLine($"add {comment.Text}");
        //                    continue;
        //                }
        //                if (comment.Thread.Count <= _receivedCommentIds[currentCommentId]) continue;
        //                var otherBranch = _client.GetComments(PostId, 100, _sourceId, 0, SortOrderBy.Asc, currentCommentId);
        //                foreach (var commentOtherBranch in otherBranch.Items)
        //                {
        //                    var currentOtherCommentId = commentOtherBranch.Id;
        //                    if (_receivedCommentIds.ContainsKey(currentOtherCommentId)) continue;
        //                    storageForRealtimeAddition.AddEntry(commentOtherBranch);
        //                    _receivedCommentIds.Add(currentOtherCommentId, 0);
        //                    _receivedCommentIds[currentCommentId]++;
        //                    Console.WriteLine($"add {commentOtherBranch.Text}");
        //                }
        //            }
        //        }
        //        Console.WriteLine($"exit cycle");
        //    }, UnsubscribeToken.Token);
        //    Console.WriteLine($"exit from method");
        //}


        public async Task WaitOtherComments(AnalyzedDataStorage storageForRealtimeAddition)
        {
            await Task.Run(() =>
            {
                while (!UnsubscribeToken.IsCancellationRequested)
                {
                    Console.WriteLine($"ping pong");
                    Thread.Sleep(60000);

                    if (UnsubscribeToken.IsCancellationRequested || _receivedCommentIds.Count >= _client.GetCommentsCount(_sourceId, PostId).Result) continue;

                    FinishBranch(storageForRealtimeAddition, out var mainBranch);
                    foreach (var comment in mainBranch.Items.Where(x => x.Thread.Count <= _receivedCommentIds[x.Id]))
                        FinishBranch(storageForRealtimeAddition, out _, comment.Id);

                }
                Console.WriteLine($"Unsubscribe {PostId}");
            }, UnsubscribeToken.Token);
            Console.WriteLine($"exit from method");
        }

        private void FinishBranch(AnalyzedDataStorage storageForRealtimeAddition, out WallGetCommentsResult branch, long? commentId = null)
        {
            branch = _client.GetComments(PostId, 100, _sourceId, 0, SortOrderBy.Asc, commentId).Result;
            var sortedBranch = branch.Items.Reverse().ToArray();
            for (var i = 0; sortedBranch.Length > 0 && i < _receivedCommentIds.Count && !_receivedCommentIds.ContainsKey(sortedBranch[i].Id); i++)
            {
                var comment = sortedBranch[i];
                storageForRealtimeAddition.AddEntry(comment);
                Console.WriteLine($"add {comment.Text} {comment.Date} {comment.Id}");
                _receivedCommentIds.TryAdd(comment.Id, comment.Thread.Count);
            }
        }

        private List<Comment> GetBranch(long? commentId = null)
        {
            var comments = new List<Comment>();
            long count = 100;
            for (var i = 0; i < count; i += 100)
            {
                var reply = _client.GetComments(PostId, 100, _sourceId, i, SortOrderBy.Asc, commentId).Result;
                comments.AddRange(reply.Items);
                foreach (var comment in reply.Items) _receivedCommentIds.TryAdd(comment.Id, comment.Thread?.Count ?? 0);
                count = reply.Count;
            }
            return comments;
        }
    }
}
