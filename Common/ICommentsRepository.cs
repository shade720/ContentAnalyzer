using Common.EntityFramework;

namespace Common;

public interface ICommentsRepository
{
    public Task Add(Comment result);
    public Task<IQueryable<Comment>> GetRange(CommentsQueryFilter? filter = null);
    public Task Clear();
}