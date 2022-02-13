using System.Configuration;
using System.Reflection;
using DataAnalysisService.Databases;
using M_USE_Toxic;

namespace DataAnalysisService;

public static class Startup
{
    public static void Main()
    {
        var service = new DataAnalysisService();
        var database = new Database(ConfigurationManager.ConnectionStrings["AllCommentsDatabase"].ConnectionString);

        service.AddDataAnalyzer("M_USE_Analysis", () =>
        {
            var museAnalyzer = new M_USE_Analyzer();
            museAnalyzer.Configure(
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ConfigurationManager.AppSettings["PredictScriptPath"]),
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ConfigurationManager.AppSettings["InterpreterPath"]),
                result => Console.WriteLine($"Analyze {result.DataFrame.Text} -> {result.Toxicity}%"),
                Console.WriteLine);
            return museAnalyzer;
        });
        DataAnalysisService.SetCurrentAnalyzer("M_USE_Analysis");

        database.SetIncomingDataHandler(service.Analyze);

        database.Connect();
        service.Start();
        database.StartLoading();

        for (var i = 0; i < 720; i++)
        {
            Thread.Sleep(60000);
        }

        database.Disconnect();
        service.Stop();
    }

}