using DataAnalysisService.AnalyzeModels.DomainClasses;
using DataAnalysisService.AnalyzeModels.ModelImplementations;
using Serilog;

namespace DataAnalysisService;

public static class Startup
{
    public static void ConfigureService(IConfiguration configuration)
    {
        var modelInfo1 = new AnalyzeModelInfo
        {
            EvaluateThresholdPercent = int.Parse(configuration["EvaluateThreshold"]),
            Interpreter = configuration["Interpreter"],
            PredictScript = configuration["Predict1"],
            TrainScript = configuration["Train1"],
            DataSet = configuration["Dataset1"],
            Model = configuration["Model1"],
            Categories = new[] { "Normal", "Insult", "Threat", "Obscenity" }
        };
        var modelInfo2 = new AnalyzeModelInfo
        {
            EvaluateThresholdPercent = int.Parse(configuration["EvaluateThreshold"]),
            Interpreter = configuration["Interpreter"],
            PredictScript = configuration["Predict2"],
            TrainScript = configuration["Train2"],
            DataSet = configuration["Dataset2"],
            Model = configuration["Model2"],
            Categories = new[] { "Normal", "Toxic" }
        };

        Services.DataAnalysisService.AddModel("InsultThreatObscenityCategories", () => CreateUniversalSentenceEncoderModel(modelInfo1));
        Services.DataAnalysisService.AddModel("ToxicCategory", () => CreateUniversalSentenceEncoderModel(modelInfo2));
    }

    private static UniversalSentenceEncoderModel CreateUniversalSentenceEncoderModel(AnalyzeModelInfo modelInfo)
    {
        var neuralModel = new UniversalSentenceEncoderModel(modelInfo);

        void Process(PredictResult predictResult)
        {
            Log.Logger.Information("Predict {0} ----> {1}\n\n", predictResult.CommentData.Text,
                predictResult.ToString());
        }

        neuralModel.Subscribe(Process
            , null, 
            null);
        return neuralModel;
    }
}