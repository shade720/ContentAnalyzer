using Common;
using DataAnalysisService.AnalyzeModels.DomainClasses;

namespace DataAnalysisService;

public static class DataAnalysisService
{
    private static readonly Dictionary<string, AnalyzeModel> AnalyzeModels = new();
    private static MsSqlServerObserver _sourceDatabase;
    private static MsSqlServerClient _targetDatabase;

    public static List<IEvaluateResult> GetAllComments(int startIndex)
    {
        return _targetDatabase.GetRange<IEvaluateResult>(startIndex);
    }

    public static void StartService()
    {
        if (_targetDatabase is null) throw new ArgumentException($"Target database is not registered {nameof(StartService)}");
        if (AnalyzeModels.Count == 0) throw new ArgumentException($"At least one analysis model must be added {nameof(StartService)}");
        _targetDatabase.Connect();
        _targetDatabase.Clear();
        Logger.Log("Service started, target database is ready", Logger.LogLevel.Information);
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
            Logger.Log($"Model {modelName} not running", Logger.LogLevel.Error);
            return;
        }
        AnalyzeModels[modelName].Predict(dataFrame);
    }
    public static void StartModel(string modelName)
    {
        if (_sourceDatabase is null || _targetDatabase is null) throw new ArgumentException($"Not all databases is registered {nameof(StartModel)}");
        if (AnalyzeModels[modelName].IsRunning)
        {
            Logger.Log($"Model {modelName} already in work", Logger.LogLevel.Error);
            return;
        }
        Logger.Log($"Starting model {modelName} to listen predicts...", Logger.LogLevel.Information);
        AnalyzeModels[modelName].Subscribe(
            predictionResult => { },
            _targetDatabase.Add,
            error =>
            {
                if (!error.Contains("NameError")) return;
                Logger.Log($"Model {modelName} has stopped by script exception {error}", Logger.LogLevel.Fatal);
                AnalyzeModels[modelName].StopModel();
            }
        );
        AnalyzeModels[modelName].StartPredictiveModel();
        Logger.Log($"Model {modelName} started listen predicts", Logger.LogLevel.Information);

        if (_sourceDatabase.IsLoadingStarted) return;
        _sourceDatabase.OnDataArrived(AnalyzeByAny);
        _sourceDatabase.StartLoading();
        Logger.Log("Source database started sending data", Logger.LogLevel.Information);
    }

    public static void StopService()
    {
        if (_targetDatabase is null) throw new ArgumentException($"Target database is not registered {nameof(StopService)}");
        _targetDatabase.Disconnect();
        Logger.Log("Service stopped", Logger.LogLevel.Information);
    }

    public static void StopAll()
    {
        foreach (var (modelName, _) in AnalyzeModels) Stop(modelName);
        Logger.Log("All model are stopped", Logger.LogLevel.Information);
    }

    public static void Stop(string modelName)
    {
        if (_sourceDatabase is null) throw new ArgumentException($"Source database is not registered {nameof(Stop)}");
        if (!AnalyzeModels[modelName].IsRunning)
        {
            Logger.Log($"Model {modelName} already stopped", Logger.LogLevel.Error);
            return;
        }
        if (IsLastRunningModel() && _sourceDatabase.IsLoadingStarted) 
            _sourceDatabase.StopLoading();
        AnalyzeModels[modelName].StopModel();
        Logger.Log($"{modelName} executing stopped", Logger.LogLevel.Information);
    }

    private static bool IsLastRunningModel()
    {
        return AnalyzeModels.Count(model => model.Value.IsRunning) == 1;
    }

    public static void RegisterSourceDatabase(MsSqlServerObserver database) => _sourceDatabase = database;
    public static void RegisterSaveDatabase(MsSqlServerClient database) => _targetDatabase = database;
    public static void AddModel(string modelName, Func<AnalyzeModel> modelConfiguration)
    {
        if (AnalyzeModels.ContainsKey(modelName))
        {
            Logger.Log($"Model {modelName} already added to service", Logger.LogLevel.Error);
            return;
        }
        AnalyzeModels.Add(modelName, modelConfiguration.Invoke());
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
            Logger.Log($"Model {modelName} already in work", Logger.LogLevel.Error);
            return;
        }
        AnalyzeModels[modelName].StartTrainModel();
        Logger.Log($"Model {modelName} started training", Logger.LogLevel.Information);
    }
}