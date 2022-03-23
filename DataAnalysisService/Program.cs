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

        Console.WriteLine("Service stops work...");

        DataAnalysisService.StopAll();
        DataAnalysisService.StopService();
    }
}