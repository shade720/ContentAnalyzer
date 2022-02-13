using Interfaces;

namespace VkDataCollector.Data
{
    internal class DataFrame : IDataFrame
    {
        public long Id { get; }
        public long PostId { get; }
        public long GroupId { get; }
        public long AuthorId { get; }
        public string Text { get; }
        public DateTime PostDate { get; }

        public DataFrame(long id, long postId, long groupId, long authorId, string text, DateTime postDate)
        {
            Id = id;
            PostId = postId;
            GroupId = groupId;
            AuthorId = authorId;
            Text = text;
            PostDate = postDate;
        }
    }
}
