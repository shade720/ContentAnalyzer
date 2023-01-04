using Common.EntityFramework;
using DataAnalysisService.AnalyzeModels.DomainClasses;
using Serilog;

namespace DataAnalysisService.AnalyzeModels.ModelImplementations;

internal class UniversalSentenceEncoderModel : AnalyzeModel
{
    private readonly AutoResetEvent _scriptInitialize = new(false);

    #region PublicInterface

    public override bool IsRunning { get; protected set; }

    public UniversalSentenceEncoderModel(AnalyzeModelInfo modelInfo) : base(modelInfo) { }

    public override void StartPredictiveModel() => StartModel(AnalyzeModelInfo.PredictScript, AnalyzeModelInfo.Model);

    public override void StartTrainModel() => StartModel(AnalyzeModelInfo.TrainScript, AnalyzeModelInfo.DataSet);

    private void StartModel(string scriptModel, string resourcePath)
    {
        if (IsRunning) throw new Exception("Runner is already using script");
        Runner = new PythonRunner(AnalyzeModelInfo.Interpreter);

        Runner.OnErrorReceivedEvent += RunnerOnErrorReceivedEventHandler;
        Runner.OnExitedEvent += RunnerOnExitEventHandler;
        Runner.OnStartedEvent += RunnerOnStartedEventHandler;

        var result = Runner.RunAsync(Path.GetFullPath(scriptModel), Path.GetFullPath(resourcePath));

        _scriptInitialize.WaitOne();
        IsRunning = true;
    }

    public override void Predict(Comment comment)
    {
        if (!IsRunning) throw new Exception("Predict model not initialized");
        if (Runner is null) throw new Exception("Script not running");

        Runner.WriteToScript(comment.Text);
        var predictFromScript = Runner.ReadFromScript();
        var predictResult = new PredictResult(comment, predictFromScript, AnalyzeModelInfo.Categories);
        OnPredictionEvent?.Invoke(predictResult);
        var maxPredict = predictResult.Predicts.MaxBy(x => x.PredictValue);
        if (maxPredict is null) throw new Exception("Exception due evaluating");
        var evaluateResult = new EvaluatedComment
        {
            CommentId = predictResult.Comment.Id, 
            RelatedComment = predictResult.Comment, 
            EvaluateCategory = maxPredict.Title, 
            EvaluateProbability = maxPredict.PredictValue
        };
        OnEvaluationEvent?.Invoke(evaluateResult);
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

    private void RunnerOnExitEventHandler()
    {
        _scriptInitialize.Reset();
        IsRunning = false;
        Runner!.OnErrorReceivedEvent -= RunnerOnErrorReceivedEventHandler;
        Runner.OnExitedEvent -= RunnerOnExitEventHandler;
        Runner.OnStartedEvent -= RunnerOnStartedEventHandler;
        Log.Logger.Information("Script ended");
    }

    private void RunnerOnErrorReceivedEventHandler(string errorMessage)
    {
        OnErrorEvent?.Invoke(errorMessage);
    }

    private void RunnerOnStartedEventHandler()
    {
        _scriptInitialize.Set();
        IsRunning = true;
        Log.Logger.Information("Script started");
    }

    #endregion
}