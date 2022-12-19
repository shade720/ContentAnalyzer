using Common.EntityFramework;

namespace DataAnalysisService.AnalyzeModels.DomainClasses;

public abstract class AnalyzeModel
{
    protected PythonRunner? Runner;
    protected readonly AnalyzeModelInfo AnalyzeModelInfo;
    
    protected delegate void OnError(string error);
    protected delegate void OnPrediction(PredictResult warning);
    protected delegate void OnEvaluation(EvaluatedComment warning);

    protected OnError? OnErrorEvent;
    protected OnPrediction? OnPredictionEvent;
    protected OnEvaluation? OnEvaluationEvent;

    public abstract bool IsRunning { get; protected set; }

    protected AnalyzeModel(AnalyzeModelInfo modelInfo)
    {
        AnalyzeModelInfo = modelInfo;
    }

    public void Subscribe(Action<PredictResult>? predictionResultsHandler, Action<EvaluatedComment>? evaluateResultsHandler, Action<string>? scriptMessagesHandler)
    {
        if (scriptMessagesHandler is not null) OnErrorEvent += scriptMessagesHandler.Invoke;
        if (predictionResultsHandler is not null) OnPredictionEvent += predictionResultsHandler.Invoke;
        if (evaluateResultsHandler is not null) OnEvaluationEvent += evaluateResultsHandler.Invoke;
    }

    public void Unsubscribe(Action<PredictResult>? predictionResultsHandler, Action<EvaluatedComment>? evaluateResultsHandler, Action<string>? scriptMessagesHandler)
    {
        if (scriptMessagesHandler is not null) OnErrorEvent -= scriptMessagesHandler.Invoke;
        if (predictionResultsHandler is not null) OnPredictionEvent -= predictionResultsHandler.Invoke;
        if (evaluateResultsHandler is not null) OnEvaluationEvent -= evaluateResultsHandler.Invoke;
    }

    public abstract void StartPredictiveModel();

    public abstract void StartTrainModel();

    public abstract void Predict(Comment comment);

    public abstract void StopModel();
}