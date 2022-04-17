using Common;
using Serilog;

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
        Log.Logger.Information("Service stops work...");

        DataCollectionService.Stop();
    }
}