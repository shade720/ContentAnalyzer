using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace VkAPITester;

public class ApiClient
{
    private readonly VkApi _api = new();
    
    public void Auth()
    {
        if (_api.IsAuthorized)
        {
            Console.WriteLine("Пользователь уже авторизирован");
            return;
        }
        const ulong applicationId = 8046073;
        const string secureKey = "rvSXQVVe9QI7Xq1hjKNm";
        const string serviceAccessKey = "041d6301041d6301041d6301940467a6f80041d041d630165c7f58fa7908b5e485a8377";
        var settings = Settings.All;
        try
        {
            _api.Authorize(new ApiAuthParams
            {
                ClientSecret = secureKey, 
                AccessToken = serviceAccessKey, 
                ApplicationId = applicationId, 
                Settings = settings
            });
            Console.WriteLine("Авторизация прошла успешно");
        }
        catch (Exception e)
        {
            Console.WriteLine("Авторизация прошла неудачно {0}, {1}", e.Message,e.StackTrace);
        }
    }

    public void LogOut()
    {
        if (!_api.IsAuthorized)
        {
            Console.WriteLine("Пользователь уже не в системе");
            return;
        }
        _api.LogOut();
        Console.WriteLine("Выход произведён успешно");
    }

    public long GetLastPostId()
    {
        var id = _api.Wall.Get(new WallGetParams {OwnerId = -29573241, Count = 1, Extended = false}).WallPosts[0].Id;
        if (id is not null)
        {
            return (long)id;
        }
        Console.WriteLine("Ошибка получения индекса");
        return 0;
    }

    public List<Comment> GetComments(long postId)
    {
        var comments = new List<Comment>();
        long count = 100;
        for (var i = 0; i < count; i += 100)
        {
            var reply = _api.Wall.GetComments(new WallGetCommentsParams
            {
                PostId = postId, 
                Count = 100,
                OwnerId = -29573241, 
                Offset = i
            });
            comments.AddRange(reply.Items);
            count = reply.Count;
        }

        var additionalComments = new List<Comment>();

        foreach (var comment in comments.Where(comment => comment.Thread.Count > 0))
        {
            for (var i = 0; i < count; i += 100)
            {
                var reply = _api.Wall.GetComments(new WallGetCommentsParams
                {
                    PostId = postId,
                    Count = 100,
                    OwnerId = -29573241,
                    Offset = i,
                    CommentId = comment.Id
                });
                additionalComments.AddRange(reply.Items);
                count = reply.Count;
            }
        }

        comments.AddRange(additionalComments);
        return comments;
    }
}