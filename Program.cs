namespace VkAPITester;

public static class Program
{
    public static void Main()
    {
        var storage = new DictionaryStorage();

        var dataCollector = new VkDataCollector(storage);
        dataCollector.Configure(new Config
        {
            ApplicationId = 8046073, SecureKey = "rvSXQVVe9QI7Xq1hjKNm",
            ServiceAccessKey = "041d6301041d6301041d6301940467a6f80041d041d630165c7f58fa7908b5e485a8377",
            ScanCommentsDelay = 60000, ScanPostDelay = 60000, QueueSize = 3
        });
        dataCollector.AddCommunity(-29573241);
        dataCollector.AddCommunity(-29534144);
        dataCollector.StartCollecting();
        for (var i = 0; i < 13; i++)
        {
            Thread.Sleep(60000);
        }

        dataCollector.StopCollecting();
    }
}