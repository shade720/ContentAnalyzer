using System.Configuration;
using System.Globalization;
using System.Reflection;
using DataAnalysisService.Databases.SqlServer;
using DataAnalysisService.Judgments;
using M_USE_Toxic;

namespace DataAnalysisService;

public static class Startup
{
    public static void Main()
    {
        var service = new DataAnalysisService();
        var sourceDatabase = new AllCommentsDatabaseObserver(ConfigurationManager.ConnectionStrings["AllCommentsDatabase"].ConnectionString, 60000); 
        //var targetDatabase = new ToxicCommentsDatabaseClient(ConfigurationManager.ConnectionStrings["ToxicCommentsDatabase"].ConnectionString);
        var judge = new JudgmentToxicityEvaluator();
        
        service.AddDataAnalyzer("M_USE_Analysis", () =>
        {
            var museAnalyzer = new M_USE_Analyzer();
            museAnalyzer.Configure(
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ConfigurationManager.AppSettings["PredictScriptPath"]),
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ConfigurationManager.AppSettings["InterpreterPath"]),
                predictionResult => Console.WriteLine($"Analyze {predictionResult.DataFrame.Text} -> {ResultParser(predictionResult.Toxicity)}%"),
                Console.WriteLine);
            return museAnalyzer;
        });
        DataAnalysisService.SetCurrentAnalyzer("M_USE_Analysis");

        sourceDatabase.SetIncomingDataHandler(service.Analyze);
        //targetDatabase.Connect();

        service.Start();
        sourceDatabase.StartLoading();
        
        for (var i = 0; i < 1440; i++)
        {
            Thread.Sleep(60000);
        }

        sourceDatabase.StopLoading();
        service.Stop();
        //targetDatabase.Disconnect();
    }

    private static string ResultParser(string result)
    {
        if (result.Length == 1) return result;
        var str = result.Replace("[", "").Replace("]", "");
        var values = str.Split(" ");
        var clearValues = values.Where(x => x.Length > 1).ToArray();
        return "Нормал: "+ double.Parse(clearValues[0], CultureInfo.InvariantCulture)+ 
               " Оскорбление: "+ double.Parse(clearValues[1], CultureInfo.InvariantCulture) + 
               " Угроза: "+ double.Parse(clearValues[2], CultureInfo.InvariantCulture) + 
               " Непристойность: " + double.Parse(clearValues[3], CultureInfo.InvariantCulture);
    }

}