using DataCollectionService.Databases.MSSQLDB;
using VkDataCollector;

namespace DataCollectionService;

public static class Startup
{
    public static void Main()
    {
        var collectionService = new DataCollectionService();
        var database = new MSSQLDB();

        collectionService.AddDataCollector(() =>
        {
            var vkDataCollector = new VkDataCollector.VkDataCollector();
            vkDataCollector.Configure(new Config
            {
                ApplicationId = 8046073,
                SecureKey = "rvSXQVVe9QI7Xq1hjKNm",
                ServiceAccessKey = "041d6301041d6301041d6301940467a6f80041d041d630165c7f58fa7908b5e485a8377",
                ScanCommentsDelay = 60000,
                ScanPostDelay = 60000,
                QueueSize = 3
            });
            vkDataCollector.AddCommunity(-29573241);
            vkDataCollector.AddCommunity(-29534144);
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
