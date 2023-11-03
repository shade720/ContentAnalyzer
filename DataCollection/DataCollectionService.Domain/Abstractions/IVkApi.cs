namespace DataCollectionService.Domain.Abstractions;

public interface IVkApi
{
    public Task<bool> LogInAsync(VkApiCredentials credentials);

    public Task<bool> LogOutAsync();

    public Task<long> GetLastPostIdAsync(long groupId);

    public Task<int> GetCommentsCountAsync(long groupId, long postId);

    public Task<IEnumerable<VkComment>> GetCommentsAsync(long groupId, long postId, int count, int offset, long? branchId = null);
}