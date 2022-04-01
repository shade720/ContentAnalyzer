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
        Logger.Write("Service stops work...");

        DataCollectionService.Stop();
    }
}