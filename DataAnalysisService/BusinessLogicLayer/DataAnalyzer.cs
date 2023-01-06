using Common;
using Common.EntityFramework;
using DataAnalysisService.BusinessLogicLayer.DatabaseClients;
using DataAnalysisService.BusinessLogicLayer.NeuralModels.Base;
using DataAnalysisService.BusinessLogicLayer.NeuralModels.BERT;
using DataAnalysisService.BusinessLogicLayer.NeuralModels.USE.Base;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Serilog;

namespace DataAnalysisService.BusinessLogicLayer;

public class DataAnalyzer
{
    private readonly List<NeuralModel> _neuralModels;

    private readonly DatabaseObserver _sourceDatabase;
    private readonly DatabaseClient<EvaluatedComment> _targetDatabase;

    private readonly Stopwatch _workTimer;
    private readonly IConfiguration _configuration;

    #region Public

    public bool IsRunning => _workTimer.ElapsedTicks != 0;
    public bool IsContainsModels => _neuralModels.Count != 0;
    public string CurrentWorkingTime => _workTimer.Elapsed.ToString(@"hh\:mm\:ss");

    public DataAnalyzer(IDbContextFactory<CommentsContext> contextFactory, IConfiguration configuration)
    {
        _configuration = configuration;
        _sourceDatabase = new CommentsDatabaseObserver(contextFactory, _configuration);
        _targetDatabase = new EvaluatedCommentsDatabaseClient(contextFactory);
        _workTimer = new Stopwatch();
        _neuralModels = new List<NeuralModel>(GetConfiguredModels(_configuration));
    }
    public void StartService()
    {
        if (_sourceDatabase is null || _targetDatabase is null)
            throw new ArgumentException($"Not all databases is registered {nameof(StartService)}");
        InitializeNeuralModels();
        EnsureLoading();
        _workTimer.Start();
    }

    public void Restart()
    {
        StopService();
        _neuralModels.Clear();
        _neuralModels.AddRange(GetConfiguredModels(_configuration));
        StartService();
    }


    public void StopService()
    {
        if (_sourceDatabase is null) throw new ArgumentException($"Source database is not registered {nameof(StopService)}");
        EnsureStopped();
        DisposeNeuralModels();
        _workTimer.Stop();
        _workTimer.Reset();
    }

    public List<EvaluatedComment> GetProcessedComments(CommentsQueryFilter filter)
    {
        return _targetDatabase.GetRange(filter);
    }

    #endregion

    #region Private

    private static IEnumerable<NeuralModel> GetConfiguredModels(IConfiguration configuration)
    {
        var insultTopicsUSEModel = configuration.GetSection("InsultTopicsUSEModel");
        var modelInfo1 = new USEModelInfo
        {
            Interpreter = insultTopicsUSEModel["Interpreter"],
            PredictScript = insultTopicsUSEModel["Predict1"],
            TrainScript = insultTopicsUSEModel["Train1"],
            DataSet = insultTopicsUSEModel["Dataset1"],
            Model = insultTopicsUSEModel["Model1"],
            Categories = new[] { "Normal", "Insult", "Threat", "Obscenity" }
        };

        var toxicTopicsUSEModel = configuration.GetSection("ToxicTopicsUSEModel");
        var modelInfo2 = new USEModelInfo
        {
            Interpreter = toxicTopicsUSEModel["Interpreter"],
            PredictScript = toxicTopicsUSEModel["Predict2"],
            TrainScript = toxicTopicsUSEModel["Train2"],
            DataSet = toxicTopicsUSEModel["Dataset2"],
            Model = toxicTopicsUSEModel["Model2"],
            Categories = new[] { "Normal", "Toxic" }
        };

        var sensetiveTopicsUSEModel = configuration.GetSection("SensetiveTopicsUSEModel");
        var modelInfo3 = new USEModelInfo
        {
            Interpreter = sensetiveTopicsUSEModel["Interpreter"],
            PredictScript = sensetiveTopicsUSEModel["Predict3"],
            TrainScript = sensetiveTopicsUSEModel["Train3"],
            DataSet = sensetiveTopicsUSEModel["Dataset3"],
            Model = sensetiveTopicsUSEModel["Model3"],
            Categories = new[] { "Offline crime", "Online crime",
                "Drugs", "Gambling", "Pornography", "Prostitution", "Slavery", "Suicide", "Terrorism",
                "Weapons", "Body shaming", "Health shaming", "Politics", "Racism", "Religion", "Sexual minorities",
                "Sexism", "Social injustice" }
        };
        //Services.DataAnalysisApiImplementation.AddModel(() => new UniversalSentenceEncoderModel(modelInfo1));
        //Services.DataAnalysisApiImplementation.AddModel(() => new UniversalSentenceEncoderModel(modelInfo2));
        //Services.DataAnalysisApiImplementation.AddModel(() => new UniversalSentenceEncoderModel(modelInfo3));
        var sensetiveTopicsBERTModel = configuration.GetSection("SensetiveTopicsBERTModel");

        return new List<NeuralModel>
        {
            new BertModel(sensetiveTopicsBERTModel["Vocabulary"], sensetiveTopicsBERTModel["ONNXBertModel"], sensetiveTopicsBERTModel["LabelEncoding"])
        };
    }

    private void ProcessComment(Comment comment)
    {
        foreach (var model in _neuralModels)
        {
            if (!model.IsInitialized)
            {
                if (!_neuralModels.Any(m => m.IsInitialized))
                {
                    Log.Logger.Error("There are no running models. Stopping the work...");
                    EnsureStopped();
                    _workTimer.Stop();
                    _workTimer.Reset();
                    return;
                }
                Log.Logger.Error("Model {modelName} not running", model.Title);
                return;
            }
            try
            {
                var result = model.Predict(comment.Text);
                var evaluatedComment = new EvaluatedComment
                {
                    CommentId = comment.Id,
                    RelatedComment = comment,
                    EvaluateCategory = result.Category,
                    EvaluateProbability = result.Probability
                };
                _targetDatabase.Add(evaluatedComment);
            }
            catch (Exception e)
            {
                Log.Logger.Fatal("{message} {stackTrace}", e.Message, e.StackTrace);
                model.Dispose();
            }
        }
    }

    private void EnsureLoading()
    {
        if (_sourceDatabase.IsLoadingStarted) return;
        try
        {
            _sourceDatabase.OnDataEvent += ProcessComment;
            _sourceDatabase.StartLoading();
        }
        catch (Exception e)
        {
            Log.Logger.Fatal("{@message} {@stackTrace}", e.Message, e.StackTrace);
            _sourceDatabase.OnDataEvent -= ProcessComment;
            _sourceDatabase.StopLoading();
        }
    }
    private void EnsureStopped()
    {
        if (!_sourceDatabase.IsLoadingStarted) return;
        _sourceDatabase.OnDataEvent -= ProcessComment;
        _sourceDatabase.StopLoading();
    }

    private void InitializeNeuralModels()
    {
        foreach (var model in _neuralModels)
        {
            try
            {
                if (!model.IsInitialized)
                    model.Initialize();
            }
            catch (Exception e)
            {
                Log.Logger.Fatal("{@message} {@stackTrace}", e.Message, e.StackTrace);
                model.Dispose();
            }
        }
    }
    private void DisposeNeuralModels()
    {
        foreach (var model in _neuralModels)
        {
            try
            {
                if (model.IsInitialized)
                    model.Dispose();
            }
            catch (Exception e)
            {
                Log.Logger.Fatal("{@message} {@stackTrace}", e.Message, e.StackTrace);
            }
        }
    }

    #endregion
}