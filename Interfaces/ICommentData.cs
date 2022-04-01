namespace Common
{
    public interface ICommentData
    {
        public long Id { get; }
        public long PostId { get; }
        public long GroupId { get; }
        public long AuthorId { get; }
        public string Text { get; }
        public DateTime PostDate { get; }
    }
}
