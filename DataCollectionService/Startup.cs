using DataCollectionService.DataCollectors.VkDataCollector;
using Serilog;

namespace DataCollectionService;

public static class Startup
{
    public static void ConfigureService(IConfiguration configuration)
    {
        Services.DataCollectionService.AddDataCollector(() =>
        {
            var vkDataCollector = new VkDataCollector();
            var vkSection = configuration.GetSection("VkSettings");
            vkDataCollector.Configure(new Config
            {
                ApplicationId = Convert.ToUInt64(vkSection["ApplicationId"]),
                SecureKey = vkSection["SecureKey"],
                ServiceAccessKey = vkSection["ServiceAccessKey"],
                ScanCommentsDelay = Convert.ToInt32(configuration["ScanCommentsDelay"]),
                ScanPostDelay = Convert.ToInt32(configuration["ScanPostDelay"]),
                ObservedPostQueueSize = Convert.ToInt32(configuration["PostQueueSize"]),
            });
            foreach (var community in vkSection.GetSection("Communities").Get<List<string>>())
            {
                vkDataCollector.AddCommunity(Convert.ToInt64(community));
            }
            vkDataCollector.Subscribe(commentData => Log.Logger.Information("Add {0} {1} {2} {3} {4} {5}",commentData.CommentId, commentData.PostId, commentData.GroupId, commentData.AuthorId, commentData.Text, commentData.PostDate));
            return vkDataCollector;
        });
    }
}
