using System.Net;
using VkNet.Enums;

namespace VkAPITester;

public static class Program
{
    public static void Main()
    {
        var vkApi = new ApiClient();
        var storage = new Storage();


        vkApi.Auth()
        var dataCollector = new VkDataCollector(vkApi, storage);
        dataCollector.AddCommunity(-29573241);
        dataCollector.StartCollecting();
    }
}