using DataAnalysisService.AnalyzeModels.DomainClasses;
using DataAnalysisService.AnalyzeModels.ModelImplementations;
using Serilog;
using static System.Net.Mime.MediaTypeNames;

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
        var modelInfo3 = new AnalyzeModelInfo
        {
            EvaluateThresholdPercent = int.Parse(configuration["EvaluateThreshold"]),
            Interpreter = configuration["Interpreter"],
            PredictScript = configuration["Predict3"],
            TrainScript = configuration["Train3"],
            DataSet = configuration["Dataset3"],
            Model = configuration["Model3"],
            Categories = new[] { "Offline crime", "Online crime", 
                "Drugs", "Gambling", "Pornography", "Prostitution", "Slavery", "Suicide", "Terrorism", 
                "Weapons", "Body shaming", "Health shaming", "Politics", "Racism", "Religion", "Sexual minorities", 
                "Sexism", "Social injustice" }
        };

        Services.DataAnalysisService.AddModel("InsultThreatObscenityCategories", () => CreateUniversalSentenceEncoderModel(modelInfo1));
        //Services.DataAnalysisService.AddModel("ToxicCategory", () => CreateUniversalSentenceEncoderModel(modelInfo2));
        Services.DataAnalysisService.AddModel("WorkingSet", () => CreateUniversalSentenceEncoderModel(modelInfo3));
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
            ,null,
            null);
        return neuralModel;
    }
}