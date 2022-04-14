using System.Configuration;
using Common;
using DataAnalysisService.AnalyzeModels.ModelImplementations;
using DataAnalysisService.DatabaseClients;

namespace DataAnalysisService;

public static class Startup
{
    public static void ConfigureService()
    {
        DataAnalysisService.RegisterSourceDatabase(new AllCommentsDb(ConfigurationManager.ConnectionStrings["Database"].ConnectionString, 60000));
        DataAnalysisService.RegisterSaveDatabase(new SuspiciousCommentsDb(ConfigurationManager.ConnectionStrings["Database"].ConnectionString));
        DataAnalysisService.AddModel("InsultThreatObscenityCategories", () => CreateModel(
            ConfigurationManager.AppSettings["Interpreter"],
            ConfigurationManager.AppSettings["Predict1"],
            ConfigurationManager.AppSettings["Train1"],
            ConfigurationManager.AppSettings["Dataset1"],
            ConfigurationManager.AppSettings["Model1"],
            new[] {"Normal", "Insult", "Threat", "Obscenity"}));
        DataAnalysisService.AddModel("ToxicCategory", () => CreateModel(
            ConfigurationManager.AppSettings["Interpreter"],
            ConfigurationManager.AppSettings["Predict2"],
            ConfigurationManager.AppSettings["Train2"],
            ConfigurationManager.AppSettings["Dataset2"],
            ConfigurationManager.AppSettings["Model2"],
            new[] {"Normal", "Toxic"}));
    }

    private static MultilingualUniversalSentenceEncoderModel CreateModel(string interpreter, string predict, string train, string dataset, string model, string [] categories)
    {
        var neuralModel = new MultilingualUniversalSentenceEncoderModel(interpreter, predict, train, dataset, model, categories);
        neuralModel.Subscribe(
            predictResult => Logger.Log($"Predict {predictResult.CommentData.Text} ----> {predictResult}\n\n", Logger.LogLevel.Information),
            evaluateResult => { },
            error => { });
        return neuralModel;
    }
}