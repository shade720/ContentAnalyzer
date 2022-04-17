using Common;
using Common.EntityFramework;
using DataAnalysisService.AnalyzeModels.DomainClasses;
using Serilog;

namespace DataAnalysisService.AnalyzeModels.ModelImplementations;

internal class UniversalSentenceEncoderModel : AnalyzeModel
{
    private static int _evaluateThreshold;
    private readonly AutoResetEvent _scriptInitialize = new(false);

    #region PublicInterface

    public override bool IsRunning { get; protected set; }

    public UniversalSentenceEncoderModel(
        string interpreter,
        string predictScript,
        string trainScript,
        string dataSet,
        string model,
        string[] categories,
        int evaluateThresholdPercent) :
        base(interpreter, predictScript, trainScript, dataSet, model, categories)
    {
        _evaluateThreshold = evaluateThresholdPercent;
    }

    public override void StartPredictiveModel() => StartModel(PredictScript, Model);

    public override void StartTrainModel() => StartModel(TrainScript, DataSet);

    private void StartModel(string scriptModel, string resourcePath)
    {
        if (IsRunning) throw new Exception("Runner is already using script");
        Runner = new PythonRunner(Interpreter);

        Runner.OnErrorReceivedEvent += RunnerOnErrorReceivedEventHandler;
        Runner.OnExitedEvent += RunnerOnExitEventHandler;
        Runner.OnStartedEvent += RunnerOnStartedEventHandler;

        var result = Runner.RunAsync(Path.GetFullPath(scriptModel), Path.GetFullPath(resourcePath));

        _scriptInitialize.WaitOne();
    }

    public override void Predict(CommentData dataFrame)
    {
        if (!IsRunning) throw new Exception("Predict model not initialized");
        if (Runner is null) throw new Exception("Script not running");

        Runner.WriteToScript(dataFrame.Text);
        var predictFromScript = Runner.ReadFromScript();
        var predictResult = new PredictResult(dataFrame, predictFromScript, Categories);
        OnPredictionEvent?.Invoke(predictResult);
        if (ExceedsThreshold(predictResult))
        {
            var maxValue = predictResult.Predicts.MaxBy(x => x.PredictValue);
            var evaluateResult = new EvaluateResult { CommentDataId = predictResult.CommentData.Id, CommentData = predictResult.CommentData, EvaluateCategory = maxValue.Title, EvaluateProbability = maxValue.PredictValue };
            OnEvaluationEvent?.Invoke(evaluateResult);
        }
    }

    public override void StopModel()
    {
        if (!IsRunning) throw new Exception("Predict model not initialized");
        if (Runner is null) throw new Exception("Script not running");
        IsRunning = false;
        Runner.OnErrorReceivedEvent -= RunnerOnErrorReceivedEventHandler;
        Runner.OnExitedEvent -= RunnerOnExitEventHandler;
        Runner.OnStartedEvent -= RunnerOnStartedEventHandler;
        Runner.Abort();
        Log.Logger.Information("Model stopped");
    }

    #endregion

    #region Private

    private static bool ExceedsThreshold(PredictResult predictResult)
    {
        for (var i = 1; i < predictResult.Predicts.Length; i++)
            if (predictResult.Predicts[i].PredictValue > _evaluateThreshold / (double)100)
                return true;
        return false;
    }

    private void RunnerOnExitEventHandler()
    {
        _scriptInitialize.Reset();
        IsRunning = false;
        Runner.OnErrorReceivedEvent -= RunnerOnErrorReceivedEventHandler;
        Runner.OnExitedEvent -= RunnerOnExitEventHandler;
        Runner.OnStartedEvent -= RunnerOnStartedEventHandler;
        Log.Logger.Information("Script ended");
    }

    private void RunnerOnErrorReceivedEventHandler(string errorMessage)
    {
        OnErrorEvent.Invoke(errorMessage);
    }

    private void RunnerOnStartedEventHandler()
    {
        _scriptInitialize.Set();
        IsRunning = true;
        Log.Logger.Information("Script started");
    }

    #endregion
}