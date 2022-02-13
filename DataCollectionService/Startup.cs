using DataCollectionService.Databases.SqlServer;
using VkDataCollector;
using System.Configuration;

namespace DataCollectionService;

public static class Startup
{
    public static void Main()
    {
        var collectionService = new DataCollectionService();
        var database = new Database(ConfigurationManager.ConnectionStrings["AllCommentsDatabase"].ConnectionString);

        collectionService.AddDataCollector(() =>
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
            vkDataCollector.Subscribe(entry =>
            {
                database.Add(entry);
            });
                
            return vkDataCollector;
        });
        database.Connect();
        database.Clear();
        collectionService.Start();
        
        for (var i = 0; i < 720; i++) 
        {
            Thread.Sleep(60000);
        }

        collectionService.Stop();
        database.Clear();
        database.Disconnect();
    }
}
