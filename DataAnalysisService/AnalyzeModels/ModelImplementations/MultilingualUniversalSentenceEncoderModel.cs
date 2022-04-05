using Common;
using DataAnalysisService.AnalyzeModels.DomainClasses;

namespace DataAnalysisService.AnalyzeModels.ModelImplementations;

internal class MultilingualUniversalSentenceEncoderModel : AnalyzeModel
{
    private static int _evaluateThreshold;
    private readonly AutoResetEvent _scriptInitialize = new(false);

    #region PublicInterface

    public override bool IsRunning { get; protected set; }

    public MultilingualUniversalSentenceEncoderModel(
        string interpreter,
        string predictScript,
        string trainScript,
        string dataSet,
        string model,
        string[] categories,
        int evaluateThresholdPercent = 70) :
        base(interpreter, predictScript, trainScript, dataSet, model, categories)
    {
        if (interpreter is null || predictScript is null || trainScript is null || dataSet is null || model is null) throw new ArgumentNullException();
        if (!File.Exists(interpreter) || !File.Exists(predictScript) || !File.Exists(trainScript) || !File.Exists(dataSet) || !File.Exists(model)) throw new FileNotFoundException();
        _evaluateThreshold = evaluateThresholdPercent;
    }

    public override void StartPredictiveModel()
    {
        if (IsRunning) throw new Exception("Runner is already using script");
        try
        {
            Runner = new PythonRunner(Interpreter);

            Runner.OnErrorReceivedEvent += RunnerOnErrorReceivedEventHandler;
            Runner.OnExitedEvent += RunnerOnExitEventHandler;
            Runner.OnStartedEvent += RunnerOnStartedEventHandler;

            var result = Runner.RunAsync(Path.GetFullPath(PredictScript), Path.GetFullPath(Model));

            _scriptInitialize.WaitOne();
        }
        catch (Exception e)
        {
            throw new Exception($"{nameof(StartPredictiveModel)}", e);
        }
    }

    public override void StartTrainModel()
    {
        if (IsRunning) throw new Exception("Runner is already using script");
        try
        {
            Runner = new PythonRunner(Interpreter);

            Runner.OnErrorReceivedEvent += RunnerOnErrorReceivedEventHandler;
            Runner.OnExitedEvent += RunnerOnExitEventHandler;
            Runner.OnStartedEvent += RunnerOnStartedEventHandler;

            var result = Runner.RunAsync(Path.GetFullPath(TrainScript), Path.GetFullPath(DataSet));

            _scriptInitialize.WaitOne();
        }
        catch (Exception e)
        {
            throw new Exception($"{nameof(StartTrainModel)}", e);
        }
    }

    public override void Predict(ICommentData dataFrame)
    {
        if (!IsRunning) throw new Exception("Predict model not initialized");
        if (Runner is null) throw new Exception("Script not running");
        try
        {
            Runner.WriteToScript(dataFrame.Text);
            var predictFromScript = Runner.ReadFromScript();
            var predictResult = new PredictResult(dataFrame, predictFromScript, Categories);

            OnPredictResultArrivedEvent.Invoke(predictResult);
            if (Evaluate(predictResult)) OnEvaluateResultArrivedEvent.Invoke(predictResult);
        }
        catch (Exception e)
        {
            throw new Exception($"{nameof(Predict)}", e);
        }
    }

    public override void StopModel()
    {
        if (!IsRunning) throw new Exception("Script already aborted");
        if (Runner is null) throw new Exception("Script not running");

        IsRunning = false;
        Runner.OnErrorReceivedEvent -= RunnerOnErrorReceivedEventHandler;
        Runner.OnExitedEvent -= RunnerOnExitEventHandler;
        Runner.OnStartedEvent -= RunnerOnStartedEventHandler;
        Runner.Abort();
        Logger.Log("Model stopped", Logger.LogLevel.Information);
    }

    #endregion

    #region Private

    private static bool Evaluate(PredictResult predictResult)
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
        Logger.Log("Script ended", Logger.LogLevel.Information);
    }

    private void RunnerOnErrorReceivedEventHandler(string errorMessage)
    {
        OnErrorArrivedEvent.Invoke(errorMessage);
    }

    private void RunnerOnStartedEventHandler()
    {
        _scriptInitialize.Set();
        IsRunning = true;
        Logger.Log("Script started", Logger.LogLevel.Information);
    }

    #endregion
}