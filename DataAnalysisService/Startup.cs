using System.Configuration;
using Common;
using Common.EntityFramework;
using DataAnalysisService.AnalyzeModels.ModelImplementations;
using DataAnalysisService.DatabaseClients;
using Microsoft.EntityFrameworkCore;

namespace DataAnalysisService;

public static class Startup
{
    public static void ConfigureService()
    {
        DataAnalysisService.SetDatabaseContextOption(new DbContextOptionsBuilder<CommentsContext>()
            .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ContentAnalyzerDatabase;Integrated Security=True;MultipleActiveResultSets=True;")
            .Options);
        
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

    private static MultilingualUniversalSentenceEncoderModel CreateModel(string interpreter, string predict, string train, string dataSet, string model, string [] categories)
    {
        var neuralModel = new MultilingualUniversalSentenceEncoderModel(interpreter, predict, train, dataSet, model, categories, int.Parse(ConfigurationManager.AppSettings["EvaluateThreshold"]));
        neuralModel.Subscribe(
            predictResult => Logger.Log($"Predict {predictResult.CommentData.Text} ----> {predictResult}\n\n", Logger.LogLevel.Information),
            evaluateResult => { },
            error => Logger.Log(error));
        return neuralModel;
    }
}