using Common;

namespace DataCollectionService;

public static class Program
{
    public static void Main()
    {
        Startup.ConfigureService();

        DataCollectionService.Start();

        while (Console.ReadLine() != "+")
        {
            Thread.Sleep(5000);
        }
        Logger.Log("Service stops work...", Logger.LogLevel.Information);

        DataCollectionService.Stop();
    }
}