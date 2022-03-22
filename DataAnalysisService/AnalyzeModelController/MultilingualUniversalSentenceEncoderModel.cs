using Interfaces;

namespace DataAnalysisService.AnalyzeModelController;

internal class MultilingualUniversalSentenceEncoderModel : AnalyzeModel
{
    private const int EvaluateThreshold = 70;
    private TaskCompletionSource<bool> _taskCompletion;
    #region PublicInterface

    public override bool IsRunning { get; protected set; }
    public MultilingualUniversalSentenceEncoderModel(
        string interpreter, 
        string predictScript, 
        string trainScript, 
        string dataSet, 
        string model, 
        string[] categories) : 
        base(interpreter, predictScript, trainScript, dataSet, model, categories) { }

    public override async Task StartPredictiveListenerScriptAsync()
    {
        if (IsRunning) throw new Exception("Runner is already using script");
        _taskCompletion = new TaskCompletionSource<bool>();

        Runner = new PythonRunner(Interpreter);
        Runner.OnErrorReceivedEvent += RunnerOnErrorReceivedEventHandler;
        Runner.OnExitedEvent += RunnerOnExitEventHandler;
        Runner.OnStartedEvent += RunnerOnStartedEventHandler;

        var result = Runner.RunAsync(Path.GetFullPath(PredictScript), Path.GetFullPath(Model));

        await _taskCompletion.Task;
    }

    public override async Task StartTrainModelScriptAsync()
    {
        if (IsRunning) throw new Exception("Runner is already using script");

        _taskCompletion = new TaskCompletionSource<bool>();

        Runner = new PythonRunner(Interpreter);
        Runner.OnErrorReceivedEvent += RunnerOnErrorReceivedEventHandler;
        Runner.OnExitedEvent += RunnerOnExitEventHandler;
        Runner.OnStartedEvent += RunnerOnStartedEventHandler;

        var result = Runner.RunAsync(Path.GetFullPath(TrainScript), Path.GetFullPath(DataSet));

        await _taskCompletion.Task;
    }

    public override void Predict(IDataFrame dataFrame)
    {
        if (!IsRunning) throw new Exception("Predict model not initialized");
        if (Runner is null) throw new Exception("Script not running");

        Runner.WriteToScript(dataFrame.Text);
        var predictFromScript = Runner.ReadFromScript();
        var predictResult = new PredictResult(dataFrame, predictFromScript, Categories);

        OnPredictResultArrivedEvent.Invoke(predictResult);
        if (Evaluate(predictResult)) OnEvaluateResultArrivedEvent.Invoke(predictResult);
    }

    public override void AbortScript()
    {
        if (!IsRunning) throw new Exception("Script already aborted");
        if (Runner is null) throw new Exception("Script not running");
        
        Runner.OnErrorReceivedEvent -= RunnerOnErrorReceivedEventHandler;
        Runner.OnExitedEvent -= RunnerOnExitEventHandler;
        Runner.OnStartedEvent -= RunnerOnStartedEventHandler;
        Runner.Abort();
    }

    #endregion

    #region Private

    private static bool Evaluate(PredictResult predictResult)
    {
        for (var i = 1; i < predictResult.Predicts.Length; i++)
            if (predictResult.Predicts[i].PredictValue > EvaluateThreshold / (double)100)
                return true;
        return false;
    }

    private void RunnerOnExitEventHandler()
    {
        IsRunning = false;
        Console.WriteLine("Script ended");
    }

    private void RunnerOnErrorReceivedEventHandler(string errorMessage)
    {
        OnErrorArrivedEvent.Invoke(errorMessage);
        if (errorMessage.Contains("Trace")) AbortScript();
    }

    private void RunnerOnStartedEventHandler(bool isStarted)
    {
        IsRunning = true;
        _taskCompletion.SetResult(isStarted);
        Console.WriteLine("Script started");
    }

    #endregion
}