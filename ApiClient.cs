using VkNet;
using VkNet.Enums;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace VkAPITester;

public class ApiClient
{
    private readonly VkApi _api = new();

    public async Task Auth(ulong applicationId, string secureKey, string serviceAccessKey)
    {
        if (applicationId <= 0 || string.IsNullOrEmpty(secureKey) || string.IsNullOrEmpty(serviceAccessKey))
        {
            throw new ArgumentException("Incorrect input data");
        }

        if (_api.IsAuthorized)
        {
            Console.WriteLine("User already logged in");
            return;
        }
        
        async Task Func()
        {
            await _api.AuthorizeAsync(new ApiAuthParams
            {
                ClientSecret = secureKey,
                AccessToken = serviceAccessKey,
                ApplicationId = applicationId,
                Settings = Settings.All
            });
            Console.WriteLine("Auth completed success");
        }

        await Try(Func);
    }

    public async Task<int> GetCommentsCount(long sourceId, long postId)
    {
        if (sourceId == 0 || postId <= 0)
        {
            throw new ArgumentException("Incorrect input data");
        }
        async Task<int> Func()
        {
            var post= await _api.Wall.GetByIdAsync(new[] { sourceId + "_" + postId });
            return post.WallPosts[0].Comments.Count;
        }
        return await Try(Func);
    }

    public async Task LogOut()
    {
        if (!_api.IsAuthorized)
        {
            Console.WriteLine("User is already logged out!");
            return;
        }
        async Task Func()
        {
            await _api.LogOutAsync();
            Console.WriteLine("Log out completed success");
        }
        await Try(Func);
    }

    public async Task<long> GetPostId(ulong offset, long? ownerId)
    {
        if (ownerId is null)
        {
            throw new ArgumentException("Incorrect input data");
        }
        async Task<long> Func()
        {
            var id = await _api.Wall.GetAsync(new WallGetParams {OwnerId = ownerId, Count = 1, Extended = false, Offset = offset});
            return id.WallPosts[0].Id.GetValueOrDefault(0);
        }
        return await Try(Func);
    }

    public async Task<WallGetCommentsResult> GetComments(long postId, int count, long? ownerId, int offset, SortOrderBy sort, long? commentId = null)
    {
        if (count <= 0 || offset < 0 || ownerId is null || postId <= 0)
        {
            throw new ArgumentException("Incorrect input data");
        }

        async Task<WallGetCommentsResult> Func()
        {
            return await _api.Wall.GetCommentsAsync(new WallGetCommentsParams
            {
                PostId = postId,
                Count = count,
                OwnerId = ownerId,
                Offset = offset,
                CommentId = commentId,
                Sort = sort
            });
        } 
        return await Try(Func);
    }

    private static T Try<T>(Func<T> apiFunc)
    {
        var attempts = 0;
        const int retryDelayMs = 3000;
        while (true)
        {
            try
            {
                return apiFunc.Invoke();
            }
            catch (Exception e)
            {
                if (attempts > 3) throw;
                Thread.Sleep(retryDelayMs);
                Console.WriteLine(e.Message, e.StackTrace);
                attempts++;
            }
        }
    }
}