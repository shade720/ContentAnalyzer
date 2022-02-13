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

        database.Connect();
        service.AddDataAnalyzer(Analyzers.M_USE_Analysis, () =>
        {
            var museAnalyzer = new M_USE_Analyzer();
            museAnalyzer.Configure(
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ConfigurationManager.AppSettings["PredictScriptPath"]),
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ConfigurationManager.AppSettings["InterpreterPath"]),
                result => Console.WriteLine($"Analyze {result.Text} -> {result.Toxicity}%"),
                Console.WriteLine);
            database.OnDataReceivedEvent += data =>
            {
                museAnalyzer.Analyze(data.Text);
            };
            return museAnalyzer;
        });

        service.SetCurrentAnalyzer(Analyzers.M_USE_Analysis);
        

        service.Start();
        database.LoadData();

        while (true)
        {
            Thread.Sleep(5000);
        }

        database.Disconnect();
        service.Stop();
    }

}