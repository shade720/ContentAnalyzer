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
            return vkDataCollector;
        });
    }
}
