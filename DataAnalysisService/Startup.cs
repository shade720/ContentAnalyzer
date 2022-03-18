using System.Configuration;
using DataAnalysisService.AnalyzeModelController;
using DataAnalysisService.Databases.SqlServer;

namespace DataAnalysisService;

public static class Startup
{
    public static void Main()
    {
        var service = new DataAnalysisService();
        var sourceDatabase = new AllCommentsDatabaseObserver(ConfigurationManager.ConnectionStrings["AllCommentsDatabase"].ConnectionString, 60000); 
        var dangerCommentsDatabase = new DangerCommentsDatabaseClient(ConfigurationManager.ConnectionStrings["DangerCommentsDatabase"].ConnectionString);

        service.AddModel(new AnalyzeModel
            (
            ConfigurationManager.AppSettings["Interpreter"],
            new []{"Нормально","Оскорбление","Угроза","Домогательство"},
            //Console.WriteLine,
            val => { },
            predictResult => Console.WriteLine($"Predict {predictResult.DataFrame.Text} ----> {predictResult}\n\n"),
            evaluateResult => dangerCommentsDatabase.Add(evaluateResult)
            )
        );
        
        sourceDatabase.OnDataArrived(service.Analyze);

        dangerCommentsDatabase.Connect();
        dangerCommentsDatabase.Clear();

        service.Start();
        sourceDatabase.StartLoading();

        for (var i = 0; i < 1440; i++)
        {
            Thread.Sleep(60000);
        }

        sourceDatabase.StopLoading();
        service.Stop();
        dangerCommentsDatabase.Disconnect();
    }
}