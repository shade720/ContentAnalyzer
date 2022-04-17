using VkNet.Enums;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;
using Common;
using Serilog;

namespace VkDataCollector;

internal class VkApi
{
    private readonly VkNet.VkApi _api = new();

    /// <summary>
    /// Performs asynchronous API authentication.
    /// </summary>
    /// <param name="applicationId">Identificator of vk application.</param>
    /// <param name="secureKey">Secure key of vk application.</param>
    /// <param name="serviceAccessKey">Service access key of vk application.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task AuthAsync(ulong applicationId, string secureKey, string serviceAccessKey)
    {
        if (_api.IsAuthorized)
        {
            Log.Logger.Error("User already authorized");
            return;
        }
        if (applicationId <= 0 || string.IsNullOrEmpty(secureKey) || string.IsNullOrEmpty(serviceAccessKey))
        {
            Log.Logger.Fatal($"Incorrect input data {nameof(AuthAsync)}");
            throw new ArgumentException($"Incorrect input data {nameof(AuthAsync)}");
        }
        async Task<int> Func()
        {
            await _api.AuthorizeAsync(new ApiAuthParams
            {
                ClientSecret = secureKey,
                AccessToken = serviceAccessKey,
                ApplicationId = applicationId,
                Settings = Settings.All
            });
            Log.Logger.Information("Auth completed success");
            return 0;
        }
        await Try(Func);
    }

    /// <summary>
    /// Asynchronously receives the number of comments under the post.
    /// </summary>
    /// <param name="groupId">Group id (starts with "-").</param>
    /// <param name="postId"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<int> GetCommentsCountAsync(long groupId, long postId)
    {
        if (groupId == 0 || postId <= 0)
        {
            Log.Logger.Fatal("Incorrect input data {nameof(GetCommentsCountAsync)}", nameof(GetCommentsCountAsync));
            throw new ArgumentException($"Incorrect input data {nameof(GetCommentsCountAsync)}");
        }
        async Task<int> Func()
        {
            var post = await _api.Wall.GetByIdAsync(new[] { groupId + "_" + postId });
            return post.WallPosts[0].Comments.Count;
        }
        return await Try(Func);
    }
    /// <summary>
    /// Performs asynchronous API log out.
    /// </summary>
    /// <returns></returns>
    public async Task LogOutAsync()
    {
        if (!_api.IsAuthorized)
        {
            Log.Logger.Error("User is already logged out!");
            return;
        }
        async Task<int> Func()
        {
            await _api.LogOutAsync();
            Log.Logger.Information("Log out completed success");
            return 0;
        }
        await Try(Func);
    }

    /// <summary>
    /// Asynchronously receives the identificator of post by groupId and offset.
    /// </summary>
    /// <param name="groupId">Group id (starts with "-").</param>
    /// <param name="offset">Post number from the beginning.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<long> GetPostIdAsync(long groupId, ulong offset)
    {
        if (groupId >= 0)
        {
            Log.Logger.Fatal($"Incorrect input data {nameof(GetPostIdAsync)}");
            throw new ArgumentException($"Incorrect input data {nameof(GetPostIdAsync)}");
        }
        async Task<long> Func()
        {
            var id = await _api.Wall.GetAsync(new WallGetParams { OwnerId = groupId, Count = 1, Extended = false, Offset = offset });
            return id.WallPosts[0].Id.GetValueOrDefault(0);
        }
        return await Try(Func);
    }

    /// <summary>
    /// Asynchronously receives (count) comments from post by post id and group id with offset and special sort.
    /// Also can get branch of comments from branch id.
    /// /// </summary>
    /// <param name="postId"></param>
    /// <param name="count"></param>
    /// <param name="groupId"></param>
    /// <param name="offset"></param>
    /// <param name="sort"></param>
    /// <param name="branchId"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<WallGetCommentsResult> GetCommentsAsync(long postId, int count, long groupId, int offset, SortOrderBy sort, long? branchId = null)
    {
        if (count <= 0 || offset < 0 || groupId >= 0 || postId <= 0)
        {
            Log.Logger.Fatal($"Incorrect input data {nameof(GetCommentsAsync)}");
            throw new ArgumentException($"Incorrect input data {nameof(GetCommentsAsync)}");
        }

        async Task<WallGetCommentsResult> Func()
        {
            return await _api.Wall.GetCommentsAsync(new WallGetCommentsParams
            {
                PostId = postId,
                Count = count,
                OwnerId = groupId,
                Offset = offset,
                CommentId = branchId,
                Sort = sort
            });
        }
        return await Try(Func);
    }

    /// <summary>
    /// Performs safe access to vk API with few attempts of retry.
    /// </summary>
    /// <typeparam name="T">A function that should be performed with safe access.</typeparam>
    /// <param name="apiFunc"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private static async Task<T> Try<T>(Func<Task<T>> apiFunc)
    {
        const int retryDelayMs = 5000;
        int attempts;
        for (attempts = 0; attempts < 10; attempts++)
        {
            try
            {
                return await apiFunc().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Thread.Sleep(retryDelayMs);
                Log.Logger.Error($"{e.Message} {e.StackTrace}");
            }
        }
        Log.Logger.Fatal("Number of attempts was exceeded");
        throw new Exception("Number of attempts was exceeded");
    }
}