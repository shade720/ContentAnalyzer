using DataAnalysisService.Databases.SqlServer;
using Common;
using DataAnalysisService.AnalyzeModels.DomainClasses;

namespace DataAnalysisService;

public static class DataAnalysisService
{
    private static readonly Dictionary<string, AnalyzeModel> AnalyzeModels = new();
    private static IDatabaseObserver? _sourceDatabase;
    private static IDatabaseClient? _targetDatabase;

    public static List<IEvaluateResult> GetAllComments(int startIndex)
    {
        return _targetDatabase?.GetRange<IEvaluateResult>(startIndex);
    }

    public static void StartService()
    {
        if (_targetDatabase is null) throw new ArgumentException($"Target database is not registered {nameof(StartService)}");
        if (AnalyzeModels.Count == 0) throw new ArgumentException($"At least one analysis model must be added {nameof(StartService)}");
        _targetDatabase.Connect();
        _targetDatabase.Clear();
        Logger.Write("Service started, target database is ready");
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
            Logger.Write($"Model {modelName} not running");
            return;
        }
        AnalyzeModels[modelName].Predict(dataFrame);
    }
    public static void StartModel(string modelName)
    {
        if (_sourceDatabase is null || _targetDatabase is null) throw new ArgumentException($"Not all databases is registered {nameof(StartModel)}");
        if (AnalyzeModels[modelName].IsRunning)
        {
            Logger.Write($"Model {modelName} already in work");
            return;
        }

        Logger.Write($"Starting model {modelName} to listen predicts...");
        AnalyzeModels[modelName].Subscribe(
            predictionResult => { },
            _targetDatabase.Add,
            error =>
            {
                if (!error.Contains("NameError")) return;
                Logger.Write($"Model {modelName} has stopped by script exception {error}");
                AnalyzeModels[modelName].StopModel();
            }
        );
        AnalyzeModels[modelName].StartPredictiveModel();
        Logger.Write($"Model {modelName} started listen predicts");

        if (_sourceDatabase.IsLoadingStarted) return;
        _sourceDatabase.OnDataArrived(AnalyzeByAny);
        _sourceDatabase.StartLoading();
        Logger.Write("Source database started sending data");
    }

    public static void StopService()
    {
        if (_targetDatabase is null) throw new ArgumentException($"Target database is not registered {nameof(StopService)}");
        _targetDatabase.Disconnect();
        Logger.Write("Service stopped");
    }

    public static void StopAll()
    {
        foreach (var (modelName, _) in AnalyzeModels) Stop(modelName);
        Logger.Write("All model are stopped");
    }

    public static void Stop(string modelName)
    {
        if (_sourceDatabase is null) throw new ArgumentException($"Source database is not registered {nameof(Stop)}");
        if (!AnalyzeModels[modelName].IsRunning)
        {
            Logger.Write($"Model {modelName} already stopped");
            return;
        }
        if (IsLastRunningModel() && _sourceDatabase.IsLoadingStarted) 
            _sourceDatabase.StopLoading();
        AnalyzeModels[modelName].StopModel();
        Logger.Write($"{modelName} executing stopped");
    }

    private static bool IsLastRunningModel()
    {
        return AnalyzeModels.Count(model => model.Value.IsRunning) == 1;
    }

    public static void RegisterSourceDatabase(IDatabaseObserver database) => _sourceDatabase = database;
    public static void RegisterSaveDatabase(IDatabaseClient database) => _targetDatabase = database;
    public static void AddModel(string modelName, Func<AnalyzeModel> modelConfiguration)
    {
        if (AnalyzeModels.ContainsKey(modelName))
        {
            Logger.Write($"Model {modelName} already added to service");
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
            Logger.Write($"Model {modelName} already in work");
            return;
        }
        AnalyzeModels[modelName].StartTrainModel();
        Logger.Write($"Model {modelName} started training");
    }

    //public static void SafeExecute(Action action)
    //{
    //    while (true)
    //    {
    //        try
    //        {
    //            action.Invoke();
    //            break;
    //        }
    //        catch (Exception e)
    //        {
    //            Console.WriteLine($"Message {e.Message} Inner {e.InnerException}");
    //            Console.WriteLine("Service crashed. Restarting...");
    //            Thread.Sleep(5000);
    //        }
    //    }
    //}
}