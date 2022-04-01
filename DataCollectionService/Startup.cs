using DataCollectionService.Databases.SqlServer;
using VkDataCollector;
using System.Configuration;
using Common;

namespace DataCollectionService;

public static class Startup
{
    public static void ConfigureService()
    {
        DataCollectionService.RegisterSaveDatabase(new AllCommentsDatabaseClient(ConfigurationManager.ConnectionStrings["AllCommentsDatabase"].ConnectionString));

        DataCollectionService.AddDataCollector(() =>
        {
            var vkDataCollector = new VkDataCollector.VkDataCollector();
            vkDataCollector.Configure(new Config
            {
                ApplicationId = Convert.ToUInt64(ConfigurationManager.AppSettings["VkApplicationId"]),
                SecureKey = ConfigurationManager.AppSettings["VkSecureKey"],
                ServiceAccessKey = ConfigurationManager.AppSettings["VkServiceAccessKey"],
                ScanCommentsDelay = Convert.ToInt32(ConfigurationManager.AppSettings["ScanCommentsDelay"]),
                ScanPostDelay = Convert.ToInt32(ConfigurationManager.AppSettings["ScanPostDelay"]),
                QueueSize = Convert.ToInt32(ConfigurationManager.AppSettings["PostQueueSize"])
            });
            vkDataCollector.AddCommunity(Convert.ToInt64(ConfigurationManager.AppSettings["NRGroupId"]));
            vkDataCollector.AddCommunity(Convert.ToInt64(ConfigurationManager.AppSettings["LentachGroupId"]));
            vkDataCollector.AddCommunity(Convert.ToInt64(ConfigurationManager.AppSettings["CSGOHS"]));
            vkDataCollector.Subscribe(dataFrame => Logger.Write($"Add {dataFrame.Id} {dataFrame.Id} {dataFrame.PostId} {dataFrame.GroupId} {dataFrame.AuthorId} {dataFrame.Text} {dataFrame.PostDate}"));
            return vkDataCollector;
        });
    }
}
