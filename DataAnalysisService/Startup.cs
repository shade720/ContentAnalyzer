using DataAnalysisService.AnalyzeModels.ModelImplementations;
using Serilog;

namespace DataAnalysisService;

public static class Startup
{
    public static void ConfigureService(IConfiguration configuration)
    {
        Services.DataAnalysisService.AddModel("InsultThreatObscenityCategories", () => CreateUniversalSentenceEncoderModel(
            int.Parse(configuration["EvaluateThreshold"]),
            configuration["Interpreter"],
            configuration["Predict1"],
            configuration["Train1"],
            configuration["Dataset1"],
            configuration["Model1"],
            new[] { "Normal", "Insult", "Threat", "Obscenity" }));
        Services.DataAnalysisService.AddModel("ToxicCategory", () => CreateUniversalSentenceEncoderModel(
            int.Parse(configuration["EvaluateThreshold"]),
            configuration["Interpreter"],
            configuration["Predict2"],
            configuration["Train2"],
            configuration["Dataset2"],
            configuration["Model2"],
            new[] { "Normal", "Toxic" }));
    }

    private static UniversalSentenceEncoderModel CreateUniversalSentenceEncoderModel(int threshold, string interpreter, string predict, string train, string dataSet, string model, string[] categories)
    {
        var neuralModel = new UniversalSentenceEncoderModel(interpreter, predict, train, dataSet, model, categories, threshold);
        neuralModel.Subscribe(
            predictResult => Log.Logger.Information("Predict {0} ----> {1}\n\n", predictResult.CommentData.Text, predictResult.ToString()),
            evaluateResult => { },
            error => { });
        return neuralModel;
    }
}