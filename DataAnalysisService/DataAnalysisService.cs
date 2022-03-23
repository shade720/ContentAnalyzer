using DataAnalysisService.AnalyzeModelController;
using DataAnalysisService.Databases.SqlServer;
using Interfaces;

namespace DataAnalysisService;

public static class DataAnalysisService
{
    private static readonly Dictionary<string, AnalyzeModel> AnalyzeModels = new();
    private static IDatabaseObserver _sourceDatabase;
    private static IDatabaseClient _targetDatabase;

    public static void StartService()
    {
        if (_sourceDatabase is null || _targetDatabase is null) throw new ArgumentException("Not all databases is registered");
        if (AnalyzeModels.Count == 0) throw new ArgumentException("At least one analysis model must be added");
        _sourceDatabase.OnDataArrived(AnalyzeByAny);
        _targetDatabase.Connect();
        _targetDatabase.Clear();
    }

    public static void StartAll()
    {
        foreach (var model in AnalyzeModels) StartModel(model.Key);
    }

    public static void AnalyzeByAny(ICommentData dataFrame)
    {
        foreach (var model in AnalyzeModels)
        {
            AnalyzeBy(model.Key, dataFrame);
        }
    }
    public static void AnalyzeBy(string modelName, ICommentData dataFrame)
    {
        if (!AnalyzeModels[modelName].IsRunning)
        {
            Console.WriteLine("Model not running");
            return;
        }
        AnalyzeModels[modelName].Predict(dataFrame);
    }
    public static void StartModel(string modelName)
    {
        Console.WriteLine($"Starting model {modelName} listen predicts...");
        if (AnalyzeModels[modelName].IsRunning)
        {
            Console.WriteLine("Model already in work");
            return;
        }
        AnalyzeModels[modelName].StartPredictiveListenerScriptAsync();
        Console.WriteLine($"Model {modelName} started listen predicts");
        if (_sourceDatabase.IsLoadingStarted) return;
        _sourceDatabase.StartLoading();
    }

    public static void StopService()
    {
        if (_sourceDatabase is null || _targetDatabase is null) throw new ArgumentException("Not all databases is registered");
        _sourceDatabase.StopLoading();
        _targetDatabase.Disconnect();
        Console.WriteLine("All models stopped");
    }

    public static void StopAll()
    {
        foreach (var model in AnalyzeModels) Stop(model.Key);
    }

    public static void Stop(string modelName)
    {
        if (!AnalyzeModels[modelName].IsRunning)
        {
            Console.WriteLine("Model already stopped");
            return;
        }
        AnalyzeModels[modelName].AbortScript();
        Console.WriteLine($"{modelName} executing stopped");
    }

    public static void RegisterSourceDatabase(IDatabaseObserver database) => _sourceDatabase = database;
    public static void RegisterSaveDatabase(IDatabaseClient database) => _targetDatabase = database;
    public static void AddModel(string modelName, Func<AnalyzeModel> modelConfiguration)
    {
        AnalyzeModels.Add(modelName, modelConfiguration.Invoke());
        AnalyzeModels[modelName].Subscribe(
            _ => { }, 
            evaluateResult => _targetDatabase.Add(evaluateResult),
            _ => { });
    } 

    public static void TrainAll()
    {
        foreach (var model in AnalyzeModels)
        { 
            TrainModel(model.Key);
        }
    }

    public static void TrainModel(string modelName)
    {
        if (AnalyzeModels[modelName].IsRunning)
        {
            Console.WriteLine("Model already in work");
            return;
        }
        AnalyzeModels[modelName].StartTrainModelScriptAsync();
        Console.WriteLine($"Model {modelName} started training");
    }
}