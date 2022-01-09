using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace VkAPITester;

public class ApiClient
{
    private readonly VkApi _api = new();
    private const ulong ApplicationId = 8046073;
    private const string SecureKey = "rvSXQVVe9QI7Xq1hjKNm";
    private const string ServiceAccessKey = "041d6301041d6301041d6301940467a6f80041d041d630165c7f58fa7908b5e485a8377";

    public void Auth()
    {
        var settings = Settings.All;
        try
        {
            _api.Authorize(new ApiAuthParams
            {
                ClientSecret = SecureKey, 
                AccessToken = ServiceAccessKey, 
                ApplicationId = ApplicationId, 
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
        _api.LogOut();
    }

    public void GetPosts()
    {
        var get = _api.Wall.Get(new WallGetParams { OwnerId = -29573241, Count = 5, Extended = false });

    }

}