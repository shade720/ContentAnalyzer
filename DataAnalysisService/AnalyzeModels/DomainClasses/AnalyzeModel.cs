﻿using Common.EntityFramework;

namespace DataAnalysisService.AnalyzeModels.DomainClasses;

public abstract class AnalyzeModel
{
    protected PythonRunner? Runner;
    protected readonly string Interpreter;
    protected readonly string PredictScript;
    protected readonly string TrainScript;
    protected readonly string DataSet;
    protected readonly string Model;

    protected delegate void OnError(string error);
    protected delegate void OnPrediction(PredictResult warning);
    protected delegate void OnEvaluation(EvaluateResult warning);

    protected OnError? OnErrorEvent;
    protected OnPrediction? OnPredictionEvent;
    protected OnEvaluation? OnEvaluationEvent;

    protected string[] Categories { get; }
    public abstract bool IsRunning { get; protected set; }

    protected AnalyzeModel(string interpreter, string predictScript, string trainScript, string dataSet, string model, string[] categories)
    {
        Interpreter = Path.GetFullPath(interpreter);
        PredictScript = Path.GetFullPath(predictScript);
        TrainScript = Path.GetFullPath(trainScript);
        DataSet = Path.GetFullPath(dataSet);
        Model = Path.GetFullPath(model);
        Categories = categories;
    }
    public void Subscribe(Action<PredictResult> predictionResultHandler, Action<EvaluateResult> evaluateResultHandler, Action<string> scriptMessagesHandler)
    {
        OnErrorEvent += scriptMessagesHandler.Invoke;
        OnPredictionEvent += predictionResultHandler.Invoke;
        OnEvaluationEvent += evaluateResultHandler.Invoke;
    }

    public abstract void StartPredictiveModel();

    public abstract void StartTrainModel();

    public abstract void Predict(CommentData comment);

    public abstract void StopModel();
}