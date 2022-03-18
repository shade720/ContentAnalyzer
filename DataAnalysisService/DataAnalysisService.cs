using System.Configuration;
using Interfaces;
namespace DataAnalysisService;

public class DataAnalysisService
{
    private readonly List<IAnalyzeModel> _analyzeModels = new();

    public void AddModel(IAnalyzeModel model)
    {
        _analyzeModels.Add(model);
    }

    public void Start()
    {
        foreach (var model in _analyzeModels)
        {
            model.StartPredictiveListenerScriptAsync(ConfigurationManager.AppSettings["Predict1"], ConfigurationManager.AppSettings["Model1"]);
        }
        Console.WriteLine("Service started");
    }

    public void Analyze(IDataFrame dataFrame)
    {
        foreach (var model in _analyzeModels)
        {
            model.Predict(dataFrame);
        }
    }

    public void Stop()
    {
        foreach (var model in _analyzeModels)
        {
            model.AbortScript();
        }
        Console.WriteLine("Service stopped");
    }
}