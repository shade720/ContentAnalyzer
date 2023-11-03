using DataCollectionService.Domain;

namespace DataCollectionService.Application;

public interface ICommentsRepository
{
    public Task Add(VkComment result);
    public Task<IQueryable<VkComment>> GetRange(CommentsQueryFilter filter);
    public Task Clear();
}