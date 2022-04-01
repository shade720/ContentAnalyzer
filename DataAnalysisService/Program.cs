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

        Logger.Write("Service stops work...");

        DataAnalysisService.StopAll();
        DataAnalysisService.StopService();
    }
}