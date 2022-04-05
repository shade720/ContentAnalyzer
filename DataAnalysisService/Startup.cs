using System.Configuration;
using Common;
using DataAnalysisService.AnalyzeModels.ModelImplementations;
using DataAnalysisService.DatabaseClients.SqlServer;

namespace DataAnalysisService;

public static class Startup
{
    public static void ConfigureService()
    {
        DataAnalysisService.RegisterSourceDatabase(new AllCommentsDatabaseObserver(ConfigurationManager.ConnectionStrings["Database"].ConnectionString, 60000));
        DataAnalysisService.RegisterSaveDatabase(new SuspiciousCommentsDatabaseClient(ConfigurationManager.ConnectionStrings["Database"].ConnectionString));

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
                    predictResult => Logger.Log($"Predict {predictResult.CommentData.Text} ----> {predictResult}\n\n", Logger.LogLevel.Information),
                    evaluateResult => { },
                    error => { });
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
                    predictResult => Logger.Log($"Predict {predictResult.CommentData.Text} ----> {predictResult}\n\n", Logger.LogLevel.Information),
                    evaluateResult => { },
                    error => { });
                return toxicModel;
            }
        );
    }
}