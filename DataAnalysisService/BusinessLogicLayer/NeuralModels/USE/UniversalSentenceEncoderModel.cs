using System.Globalization;
using DataAnalysisService.BusinessLogicLayer.NeuralModels.Base;
using DataAnalysisService.BusinessLogicLayer.NeuralModels.USE.Base;
using Serilog;

namespace DataAnalysisService.BusinessLogicLayer.NeuralModels.USE;

internal class UniversalSentenceEncoderModel : NeuralModel
{
    private readonly USEModelInfo _modelInfo;
    private readonly AutoResetEvent _scriptInitialize;
    private PythonRunner _runner;

    #region PublicInterface

    public UniversalSentenceEncoderModel(USEModelInfo modelInfo)
    {
        _modelInfo = modelInfo;
        _scriptInitialize = new AutoResetEvent(false);
        Title = "Sensetive-topics-MUSE";
    }

    public override void Initialize()
    {
        _runner = new PythonRunner(_modelInfo.Interpreter);

        _runner.OnErrorReceivedEvent += RunnerOnErrorReceivedEventHandler;
        _runner.OnExitedEvent += RunnerOnExitEventHandler;
        _runner.OnStartedEvent += RunnerOnStartedEventHandler;

        var result = _runner.RunAsync(Path.GetFullPath(_modelInfo.PredictScript), Path.GetFullPath(_modelInfo.Model));

        _scriptInitialize.WaitOne();
        IsInitialized = true;
        Log.Logger.Information("Model {title} started", Title);
    }

    public override global::DataAnalysisService.BusinessLogicLayer.NeuralModels.Base.PredictResult Predict(string sentence)
    {
        if (!IsInitialized) throw new Exception("Predict model not initialized");
        if (_runner is null) throw new Exception("Script not running");

        _runner.WriteToScript(sentence);
        var predictFromScript = _runner.ReadFromScript();
        var y = ParsePredict(predictFromScript);

        var maxValue = y.Max();
        var label = _modelInfo.Categories[y.IndexOf(maxValue)];

        Log.Logger.Information("{Model} predict {Text} to: {Category} - {Probability}", Title, sentence, label, maxValue);
        return new PredictResult
        {
            Probability = maxValue,
            Category = label
        };
    }

    public override void Dispose()
    {
        if (!IsInitialized) throw new Exception("Predict model not initialized");
        if (_runner is null) throw new Exception("Script not running");
        IsInitialized = false;
        _runner.OnErrorReceivedEvent -= RunnerOnErrorReceivedEventHandler;
        _runner.OnExitedEvent -= RunnerOnExitEventHandler;
        _runner.OnStartedEvent -= RunnerOnStartedEventHandler;
        _runner.Abort();
        Log.Logger.Information("Model {title} stopped", Title);
    }

    #endregion

    #region Private

    private static List<float> ParsePredict(string predict)
    {
        var clearResults = predict.Replace("[", "").Replace("]", "");
        var predictValues = clearResults.Split(' ');
        return predictValues.Where(x => x.Length > 1).Select(x => float.Parse(x, CultureInfo.InvariantCulture)).ToList();
    }

    private void RunnerOnExitEventHandler()
    {
        _scriptInitialize.Reset();
        IsInitialized = false;
        _runner.OnErrorReceivedEvent -= RunnerOnErrorReceivedEventHandler;
        _runner.OnExitedEvent -= RunnerOnExitEventHandler;
        _runner.OnStartedEvent -= RunnerOnStartedEventHandler;
        Log.Logger.Information("Script ended");
    }

    private void RunnerOnErrorReceivedEventHandler(string errorMessage)
    {
        if (errorMessage.Contains("NameError"))
            Log.Logger.Fatal("Model {@modelName} has stopped by script exception {@error}", Title, errorMessage);
        else Log.Logger.Warning(errorMessage);
    }

    private void RunnerOnStartedEventHandler()
    {
        _scriptInitialize.Set();
        IsInitialized = true;
        Log.Logger.Information("Script started");
    }

    #endregion
}