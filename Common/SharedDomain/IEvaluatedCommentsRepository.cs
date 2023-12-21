namespace Common.SharedDomain;

public interface IEvaluatedCommentsRepository
{
    public Task Add(EvaluatedComment result);
    public Task<List<EvaluatedComment>> GetRange(CommentsQueryFilter filter);
    public Task Clear();
}