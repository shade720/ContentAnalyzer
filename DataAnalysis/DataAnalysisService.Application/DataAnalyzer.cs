using Common.SharedDomain;
using DataAnalysisService.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace DataAnalysisService.Application;

public class DataAnalyzer : IDisposable
{
    private Dictionary<string, IArtificialIntelligenceModel> _artificialIntelligenceModels;

    private readonly IConfiguration _configuration;
    private readonly IArtificialIntelligenceModelFactory _artificialIntelligenceModelFactory;

    private readonly ICommentsObserver _commentsObserver;
    private readonly IEvaluatedCommentsRepository _evaluatedCommentsRepository;

    public bool IsAnalysisStarted => _artificialIntelligenceModels.Count > 0;

    public DataAnalyzer(
        IConfiguration configuration,
        IArtificialIntelligenceModelFactory artificialIntelligenceModelFactory,
        ICommentsObserver commentsObserver,
        IEvaluatedCommentsRepository evaluatedCommentsRepository)
    {
        _configuration = configuration;
        _artificialIntelligenceModelFactory = artificialIntelligenceModelFactory;
        _artificialIntelligenceModels = new Dictionary<string, IArtificialIntelligenceModel>();

        _commentsObserver = commentsObserver;
        _commentsObserver.OnNewInfoEvent += ProcessComment;

        _evaluatedCommentsRepository = evaluatedCommentsRepository;
    }

    public void StartAnalysis()
    {
        _artificialIntelligenceModels = _configuration
            .GetSection("BertModels")
            .GetChildren()
            .ToDictionary(
                x => x.Key,
                x => _artificialIntelligenceModelFactory.CreateArtificialIntelligenceModel(x.Key));

        _commentsObserver.StartObserving();
    }

    public void StopAnalysis()
    {
        if (_artificialIntelligenceModels.Count > 0)
        {
            foreach (var model in _artificialIntelligenceModels.Values)
            {
                model.Dispose();
            }
            _artificialIntelligenceModels.Clear();
        }
        _commentsObserver.StopObserving();
    }

    private async Task ProcessComment(Comment comment)
    {
        foreach (var (key, model) in _artificialIntelligenceModels)
        {
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
                await _evaluatedCommentsRepository.Add(evaluatedComment);
            }
            catch (Exception e)
            {
                Log.Logger.Fatal("{message} {stackTrace}", e.Message, e.StackTrace);
                model.Dispose();
                _artificialIntelligenceModels[key] = _artificialIntelligenceModelFactory.CreateArtificialIntelligenceModel(key);
            }
        }
    }

    public void Dispose()
    {
        StopAnalysis();
    }
}