using VkNet.Enums;
using VkNet.Model;

namespace VkAPITester
{
    public class DataCollector
    {
        public long PostId { get; }
        public long GroupId { get; }

        private readonly ApiClient _client;
        private readonly Dictionary<long, long?> _receivedCommentIds = new();
        public CancellationTokenSource UnsubscribeToken { get; } = new();

        public DataCollector(ApiClient client, long postId, long groupId) => (_client, PostId, GroupId) = (client, postId, groupId);

        public List<Comment> GetPresentComments()
        {
            var comments = GetBranch();

            var additionalComments = new List<Comment>();
            foreach (var comment in comments.Where(comment => comment.Thread.Count > 0)) additionalComments.AddRange(GetBranch(comment.Id));
            comments.AddRange(additionalComments);

            foreach (var comment in comments) Console.WriteLine($"add {comment.Id} {comment.PostId} {comment.OwnerId} {comment.FromId} {comment.Text} {comment.Date}");
            return comments;
        }

        public async Task WaitOtherComments(Storage storageForRealtimeAddition)
        {
            await Task.Run(() =>
            {
                while (!UnsubscribeToken.IsCancellationRequested)
                {
                    Thread.Sleep(60000);
                    if (UnsubscribeToken.IsCancellationRequested || _receivedCommentIds.Count >= _client.GetCommentsCount(GroupId, PostId).Result) continue;

                    FinishBranch(storageForRealtimeAddition, out var mainBranch);
                    foreach (var comment in mainBranch.Items.Where(x => x.Thread.Count > _receivedCommentIds[x.Id]))
                        FinishBranch(storageForRealtimeAddition, out _, comment.Id);
                }
                Console.WriteLine($"Unsubscribe {PostId}");
            }, UnsubscribeToken.Token);
            Console.WriteLine("Exit from method");
        }

        private void FinishBranch(Storage storageForRealtimeAddition, out WallGetCommentsResult branch, long? commentId = null)
        {
            branch = _client.GetComments(PostId, 100, GroupId, 0, SortOrderBy.Asc, commentId).Result;
            if (branch.Count == 0) return;

            var sortedBranch = branch.Items.Reverse().ToArray();
            for (var i = 0; i < sortedBranch.Length && !_receivedCommentIds.ContainsKey(sortedBranch[i].Id); i++)
            {
                var comment = sortedBranch[i];
                storageForRealtimeAddition.AddEntry(comment);
                Console.WriteLine($"add {comment.Id} {comment.PostId} {comment.OwnerId} {comment.FromId} {comment.Text} {comment.Date}");
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
}
