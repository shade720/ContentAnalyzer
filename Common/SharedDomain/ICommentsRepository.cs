namespace Common.SharedDomain;

public interface ICommentsRepository
{
    public Task Add(Comment result);
    public Task<List<Comment>> GetRange(CommentsQueryFilter? filter = null);
    public Task Clear();
}