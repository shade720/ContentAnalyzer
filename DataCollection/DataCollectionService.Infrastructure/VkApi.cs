using System.Text.RegularExpressions;
using DataCollectionService.Domain;
using DataCollectionService.Domain.Abstractions;
using Serilog;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace DataCollectionService.Infrastructure;

public class VkApi : IVkApi
{
    private readonly VkNet.VkApi _api = new();

    /// <summary>
    /// Performs asynchronous API authentication.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<bool> LogInAsync(VkApiCredentials credentials)
    {
        if (_api.IsAuthorized)
        {
            Log.Logger.Error("User already authorized");
            return false;
        }

        if (credentials.ApplicationId <= 0 || string.IsNullOrEmpty(credentials.SecureKey) || string.IsNullOrEmpty(credentials.ServiceAccessKey))
            throw new ArgumentException($"Incorrect input data {nameof(LogInAsync)}");

        return await SafeConnect(async () =>
        {
            await _api.AuthorizeAsync(new ApiAuthParams
            {
                ClientSecret = credentials.SecureKey,
                AccessToken = credentials.ServiceAccessKey,
                ApplicationId = credentials.ApplicationId,
                Settings = Settings.All
            });

            Log.Logger.Information("Auth completed successfully");

            return true;
        });
    }

    /// <summary>
    /// Performs asynchronous API log out.
    /// </summary>
    /// <returns></returns>
    public async Task<bool> LogOutAsync()
    {
        if (!_api.IsAuthorized)
        {
            Log.Logger.Error("User is already logged out!");
            return false;
        }

        return await SafeConnect(async () =>
        {
            await _api.LogOutAsync();
            Log.Logger.Information("Log out completed success");
            return true;
        });
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
            throw new ArgumentException($"Incorrect input data {nameof(GetCommentsCountAsync)}");

        return await SafeConnect(async () =>
        {
            var post = await _api.Wall.GetByIdAsync(new[] { $"{groupId}_{postId}" });
            return post.WallPosts[0].Comments.Count;
        });
    }

    /// <summary>
    /// Asynchronously receives the identificator of post by groupId and offset.
    /// </summary>
    /// <param name="groupId">Group id (starts with "-").</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<long> GetLastPostIdAsync(long groupId)
    {
        if (groupId >= 0)
            throw new ArgumentException($"Incorrect input data {nameof(GetLastPostIdAsync)}");

        return await SafeConnect(async () =>
        {
            var id = await _api.Wall.GetAsync(new WallGetParams
            {
                OwnerId = groupId,
                Count = 1,
                Extended = false,
                Offset = 1
            });
            return id.WallPosts[0].Id.GetValueOrDefault(0);
        });
    }

    /// <summary>
    /// Asynchronously receives (count) comments from post by post id and group id with offset and special sort.
    /// Also can get branch of comments from branch id.
    /// /// </summary>
    /// <param name="postId"></param>
    /// <param name="count"></param>
    /// <param name="groupId"></param>
    /// <param name="offset"></param>
    /// <param name="branchId"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<IEnumerable<VkComment>> GetCommentsAsync(long groupId, long postId, int count, int offset, long? branchId = null)
    {
        if (count <= 0 || offset < 0 || groupId >= 0 || postId <= 0)
            throw new ArgumentException($"Incorrect input data {nameof(GetCommentsAsync)}");

        return await SafeConnect(async () =>
        {
            var wallGetCommentResult = await _api.Wall.GetCommentsAsync(new WallGetCommentsParams()
            {
                PostId = postId,
                Count = count,
                OwnerId = groupId,
                Offset = offset,
                CommentId = branchId
            });

            return Convert(wallGetCommentResult);
        });
    }

    private static IEnumerable<VkComment> Convert(WallGetCommentsResult wallGetCommentResult)
    {
        return
            from wallComment in wallGetCommentResult.Items
            where !IsCommentInvalid(wallComment)
            select new VkComment
            {
                CommentId = wallComment.Id,
                PostId = wallComment.PostId!.Value,
                GroupId = wallComment.OwnerId!.Value,
                AuthorId = wallComment.FromId!.Value,
                Text = ClearText(wallComment.Text),
                PostDate = wallComment.Date!.Value.ToLocalTime(),
                ThreadCommentsCount = wallComment.Thread.Count!.Value
            };
    }

    private static string ClearText(string text)
    {
        var result = Regex.Replace(text, @"\p{Cs}", "");
        if (result.StartsWith('[')) result = result.Remove(result.IndexOf('['), result.IndexOf(']') - result.IndexOf('[') + 2).Trim();
        return result;
    }

    private static bool IsCommentInvalid(Comment comment)
    {
        return comment.PostId is null ||
               comment.OwnerId is null ||
               comment.FromId is null ||
               comment.Date is null || 
               comment.Thread?.Count is null || 
               comment.Id <= 0 || 
               comment.PostId <= 0 || 
               comment.OwnerId > 0 || 
               string.IsNullOrEmpty(comment.Text) || 
               comment.Text.Length <= 5;
    }

    /// <summary>
    /// Performs safe access to vk API with few attempts of retry.
    /// </summary>
    /// <typeparam name="T">A function that should be performed with safe access.</typeparam>
    /// <param name="apiFunc"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private static async Task<T> SafeConnect<T>(Func<Task<T>> apiFunc)
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