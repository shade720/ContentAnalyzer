using DataAnalysisService.AnalyzeModelController;
using DataAnalysisService.Databases.SqlServer;
using System.Configuration;

namespace DataAnalysisService;

public static class Startup
{
    public static void Main()
    {
        DataAnalysisService.RegisterSourceDatabase(new AllCommentsDatabaseObserver(ConfigurationManager.ConnectionStrings["AllCommentsDatabase"].ConnectionString, 60000));
        DataAnalysisService.RegisterSaveDatabase(new DangerCommentsDatabaseServerClient(ConfigurationManager.ConnectionStrings["DangerCommentsDatabase"].ConnectionString));

        DataAnalysisService.AddModel("InsultThreatObscenityCategories",
            () =>
            {
                var insultThreatObscenityModel = new MultilingualUniversalSentenceEncoderModel
                (
                    ConfigurationManager.AppSettings["Interpreter"],
                    ConfigurationManager.AppSettings["Predict1"],
                    ConfigurationManager.AppSettings["Train1"],
                    ConfigurationManager.AppSettings["Dataset1"],
                    ConfigurationManager.AppSettings["Model1"],
                    new[] {"Normal", "Insult", "Threat", "Obscenity"}
                );
                insultThreatObscenityModel.Subscribe(
                    predictResult => Console.WriteLine($"Predict {predictResult.DataFrame.Text} ----> {predictResult}\n\n"),
                    evaluateResultHandler => Console.WriteLine($"Predict {evaluateResultHandler.DataFrame.Text} ----> {evaluateResultHandler}!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"),
                    errorHandler => { });
                return insultThreatObscenityModel;
            }
        
        );
        DataAnalysisService.AddModel("ToxicCategory",
            () =>
            {
                var toxicModel = new MultilingualUniversalSentenceEncoderModel
                (
                    ConfigurationManager.AppSettings["Interpreter"],
                    ConfigurationManager.AppSettings["Predict2"],
                    ConfigurationManager.AppSettings["Train2"],
                    ConfigurationManager.AppSettings["Dataset2"],
                    ConfigurationManager.AppSettings["Model2"],
                    new[] {"Normal", "Toxic"}
                );
                toxicModel.Subscribe(
                    predictResult => Console.WriteLine($"Predict {predictResult.DataFrame.Text} ----> {predictResult}\n\n"),
                    evaluateResultHandler => Console.WriteLine($"Predict {evaluateResultHandler.DataFrame.Text} ----> {evaluateResultHandler}!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"),
                    errorHandler => { });
                return toxicModel;
            }
        );

        DataAnalysisService.StartService();

        while (Console.ReadLine() != "+")
        {
            Thread.Sleep(5000);
        }

        Console.WriteLine("Service stops work...");

        DataAnalysisService.StopService();
    }
}