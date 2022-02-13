using System.Configuration;
using System.Reflection;
using M_USE_Toxic;

namespace DataAnalysisService;

public static class Startup
{
    public static void Main()
    {
        var service = new DataAnalysisService();
        var database = new MSSQLDB();

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
                var a = data.Text;
                if (a.Contains('[')) a = a.Remove(a.IndexOf('['), a.IndexOf(']') - a.IndexOf('[') + 2).Trim();
                if (!string.IsNullOrEmpty(a)) museAnalyzer.Analyze(a);
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