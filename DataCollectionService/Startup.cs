using DataCollectionService.Databases.SqlServer;
using VkDataCollector;
using System.Configuration;

namespace DataCollectionService;

public static class Startup
{
    public static void Main()
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
            //vkDataCollector.AddCommunity(Convert.ToInt64(ConfigurationManager.AppSettings["NRGroupId"]));
            vkDataCollector.AddCommunity(Convert.ToInt64(ConfigurationManager.AppSettings["LentachGroupId"]));
            vkDataCollector.AddCommunity(Convert.ToInt64(ConfigurationManager.AppSettings["CSGOHS"]));
            return vkDataCollector;
        });
        
        DataCollectionService.Start();

        while (Console.ReadLine() != "+")
        {
            Thread.Sleep(5000);
        }
        Console.WriteLine("Service stops work...");

        DataCollectionService.Stop();
    }
}
