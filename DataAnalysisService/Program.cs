using Common;

namespace DataAnalysisService;

public static class Program
{
    public static void Main()
    {
        Startup.ConfigureService();

        DataAnalysisService.StartService();
        DataAnalysisService.StartAll();

        while (Console.ReadLine() != "+")
        {
            Thread.Sleep(5000);
        }

        Logger.Log("Service stops work...", Logger.LogLevel.Information);

        DataAnalysisService.StopAll();
        DataAnalysisService.StopService();
    }
}