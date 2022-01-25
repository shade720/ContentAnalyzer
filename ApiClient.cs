using VkNet;
using VkNet.Enums;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace VkAPITester;

public class ApiClient
{
    /// <summary>
    ///async variant
    /// </summary>
    private readonly VkApi _api = new();
    private static long _requestCount;

    public void Auth(ulong applicationId, string secureKey, string serviceAccessKey)
    {
        if (applicationId <= 0 || string.IsNullOrEmpty(secureKey) || string.IsNullOrEmpty(serviceAccessKey))
        {
            throw new ArgumentException($"Incorrect input data {nameof(Auth)}");
        }

        if (_api.IsAuthorized)
        {
            Console.WriteLine("User already logged in");
            return;
        }
        
        int Func()
        {
            _api.AuthorizeAsync(new ApiAuthParams
            {
                ClientSecret = secureKey,
                AccessToken = serviceAccessKey,
                ApplicationId = applicationId,
                Settings = Settings.All
            });
            Console.WriteLine("Auth completed success");
            return 0;
        }
        Try(Func);
    }

    public int GetCommentsCount(long groupId, long postId)
    {
        if (groupId == 0 || postId <= 0)
        {
            throw new ArgumentException($"Incorrect input data {nameof(GetCommentsCount)}");
        }
        int Func()
        {
            var post= _api.Wall.GetById(new[] { groupId + "_" + postId });
            return post.WallPosts[0].Comments.Count;
        }
        return Try(Func);
    }

    public void LogOut()
    {
        if (!_api.IsAuthorized)
        {
            Console.WriteLine("User is already logged out!");
            return;
        }
        int Func()
        {
            _api.LogOut();
            Console.WriteLine("Log out completed success");
            return 0;
        }
        Try(Func);
    }

    public long GetPostId(ulong offset, long? ownerId)
    {
        if (ownerId is null)
        {
            throw new ArgumentException($"Incorrect input data {nameof(GetPostId)}");
        }
        long Func()
        {
            var id = _api.Wall.Get(new WallGetParams {OwnerId = ownerId, Count = 1, Extended = false, Offset = offset});
            return id.WallPosts[0].Id.GetValueOrDefault(0);
        }
        return Try(Func);
    }

    public WallGetCommentsResult GetComments(long postId, int count, long? ownerId, int offset, SortOrderBy sort, long? commentId = null)
    {
        if (count <= 0 || offset < 0 || ownerId is null || postId <= 0)
        {
            throw new ArgumentException($"Incorrect input data {nameof(GetComments)}");
        }

        WallGetCommentsResult Func()
        {
            return _api.Wall.GetComments(new WallGetCommentsParams
            {
                PostId = postId,
                Count = count,
                OwnerId = ownerId,
                Offset = offset,
                CommentId = commentId,
                Sort = sort
            });
        } 
        return Try(Func);
    }

    private static T Try<T>(Func<T> apiFunc)
    {
        var attempts = 0;
        const int retryDelayMs = 3000;
        while (true)
        {
            try
            {
                Console.WriteLine($"Request counter: {_requestCount++}");
                return apiFunc.Invoke();
            }
            catch (Exception e)
            {
                if (attempts > 10) throw;
                Thread.Sleep(retryDelayMs);
                Console.WriteLine(e.Message, e.StackTrace);
                attempts++;
            }
        }
    }
}