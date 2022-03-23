namespace DataCollectionService;

public class Program
{
    public static void Main()
    {
        Startup.ConfigureService();

        DataCollectionService.Start();

        while (Console.ReadLine() != "+")
        {
            Thread.Sleep(5000);
        }
        Console.WriteLine("Service stops work...");

        DataCollectionService.Stop();
    }
}