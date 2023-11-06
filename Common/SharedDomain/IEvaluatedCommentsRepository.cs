namespace Common.SharedDomain;

public interface IEvaluatedCommentsRepository
{
    public Task Add(EvaluatedComment result);
    public Task<IQueryable<EvaluatedComment>> GetRange(CommentsQueryFilter filter);
    public Task Clear();
}