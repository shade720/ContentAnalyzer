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
            vkDataCollector.Configure(new Config
            {
                ApplicationId = Convert.ToUInt64(configuration["VkApplicationId"]),
                SecureKey = configuration["VkSecureKey"],
                ServiceAccessKey = configuration["VkServiceAccessKey"],
                ScanCommentsDelay = Convert.ToInt32(configuration["ScanCommentsDelay"]),
                ScanPostDelay = Convert.ToInt32(configuration["ScanPostDelay"]),
                ObservedPostQueueSize = Convert.ToInt32(configuration["PostQueueSize"]),
            });
            vkDataCollector.AddCommunity(Convert.ToInt64(configuration["NRGroupId"]));
            vkDataCollector.AddCommunity(Convert.ToInt64(configuration["LentachGroupId"]));
            vkDataCollector.AddCommunity(Convert.ToInt64(configuration["CSGOHSGroupId"]));
            vkDataCollector.Subscribe(commentData => 
                Log.Logger.Information("Add {0} {1} {2} {3} {4} {5}",commentData.CommentId, commentData.PostId, commentData.GroupId, commentData.AuthorId, commentData.Text, commentData.PostDate));
            return vkDataCollector;
        });
    }
}
