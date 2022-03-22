using DataAnalysisService.AnalyzeModelController;
using DataAnalysisService.Databases.SqlServer;
using Interfaces;

namespace DataAnalysisService;

public static class DataAnalysisService
{
    private static readonly Dictionary<string, AnalyzeModel> AnalyzeModels = new();
    private static IDatabaseObserver? _sourceDatabase;
    private static IDatabaseClient? _targetDatabase;

    public static async Task StartService()
    {
        if (_sourceDatabase is null || _targetDatabase is null) throw new Exception("Not all databases is registered");
        if (AnalyzeModels.Count == 0) throw new Exception("At least one analysis model must be added");

        await StartAll();

        _sourceDatabase.OnDataArrived(AnalyzeByAny);
        _sourceDatabase.StartLoading();
        _targetDatabase.Connect();
        _targetDatabase.Clear();
    }

    public async static Task StartAll()
    {
        foreach (var model in AnalyzeModels) await StartModel(model.Key);
    }

    public static void AnalyzeByAny(IDataFrame dataFrame)
    {
        foreach (var model in AnalyzeModels)
        {
            AnalyzeBy(model.Key, dataFrame);
        }
    }
    public static void AnalyzeBy(string modelName, IDataFrame dataFrame)
    {
        if (!AnalyzeModels[modelName].IsRunning)
        {
            Console.WriteLine("Model not running");
            return;
        }
        AnalyzeModels[modelName].Predict(dataFrame);
    }
    public async static Task StartModel(string modelName)
    {
        if (AnalyzeModels[modelName].IsRunning)
        {
            Console.WriteLine("Model already in work");
            return;
        }
        await AnalyzeModels[modelName].StartPredictiveListenerScriptAsync();
        Console.WriteLine($"Model {modelName} started listen predicts");
    }

    public static void StopService()
    {
        if (_sourceDatabase is null || _targetDatabase is null) throw new Exception("Not all databases is registered");
        _sourceDatabase.StopLoading();
        _targetDatabase.Disconnect();
        StopAll();
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