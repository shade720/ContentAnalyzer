using Interfaces;

namespace DataAnalysisService.AnalyzeModelController;

internal class AnalyzeModel : IAnalyzeModel
{
    private PythonRunner _runner;

    private readonly string _interpreter;
    private readonly string[] _categories;

    private const int EvaluateThreshold = 70;

    private readonly Action<string> _errorHandler;
    private readonly Action<PredictResult> _predictResultHandler;
    private readonly Action<PredictResult> _evaluateResultHandler;

    #region PublicInterface

    public bool IsRunning { get; private set; }

    public AnalyzeModel(string interpreter, string[] categories, Action<string> errorHandler, Action<PredictResult> predictResultHandler, Action<PredictResult> evaluateResultHandler)
    {
        _interpreter = Path.GetFullPath(interpreter);
        _categories = categories;

        _errorHandler = errorHandler;
        _predictResultHandler = predictResultHandler;
        _evaluateResultHandler = evaluateResultHandler;
    }

    public async Task StartPredictiveListenerScriptAsync(string predictScript, string model)
    {
        if (IsRunning) throw new Exception("Runner is already using script");
        IsRunning = true;

        Console.WriteLine("Start model for predict... ");
        _runner = new PythonRunner(_interpreter);

        _runner.OnErrorReceivedEvent += RunnerOnErrorReceivedEventHandler;
        _runner.OnExitEvent += RunnerOnExitEventHandler;

        await Task.Run(() => _runner.Run(Path.GetFullPath(predictScript), Path.GetFullPath(model), true));

        Console.WriteLine("Predict model is stopped");
    }

    public async Task StartTrainModelScriptAsync(string trainScript, string dataSet)
    {
        if (IsRunning) throw new Exception("Runner is already using script");
        IsRunning = true;
        Console.WriteLine("Start training model... ");

        _runner = new PythonRunner(_interpreter);

        _runner.OnErrorReceivedEvent += RunnerOnErrorReceivedEventHandler;
        _runner.OnExitEvent += RunnerOnExitEventHandler;

        await Task.Run(() => _runner.Run(Path.GetFullPath(trainScript), Path.GetFullPath(dataSet), false));

        Console.WriteLine("Model is trained");
    }

    public void Predict(IDataFrame dataFrame)
    {
        if (!IsRunning) throw new Exception("Predict model not initialized");

        _runner.WriteToScript(dataFrame.Text);
        var predictResult = new PredictResult(dataFrame, _runner.ReadFromScript(), _categories);

        _predictResultHandler.Invoke(predictResult);
        if (Evaluate(predictResult))
            _evaluateResultHandler.Invoke(predictResult);
    }

    public void AbortScript()
    {
        _runner.OnErrorReceivedEvent -= RunnerOnErrorReceivedEventHandler;
        _runner.OnExitEvent -= RunnerOnExitEventHandler;
        _runner.Abort();
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
        _errorHandler.Invoke(errorMessage);
        if (errorMessage.Contains("Trace")) AbortScript();
    }

    #endregion
}