using Common.SharedDomain;
using DataAnalysisService.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace DataAnalysisService.Application;

public class CommentsDatabaseObserver : ICommentsObserver
{
    public event ICommentsObserver.OnNewInfo? OnNewInfoEvent;

    private readonly ICommentsRepository _commentsRepository;
    private readonly IConfiguration _configuration;
    private CancellationTokenSource? _cancellationTokenSource;

    public CommentsDatabaseObserver(ICommentsRepository commentsRepository, IConfiguration configuration)
    {
        _commentsRepository = commentsRepository;
        _configuration = configuration;
    }

    public void StartObserving()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        Task.Run(CommentsLoadingLoop);
    }

    public void StopObserving()
    {
        _cancellationTokenSource?.Cancel();
    }

    private async Task CommentsLoadingLoop()
    {
        Log.Logger.Information("Loading started on with delay {delay}", _configuration["ObserveDelayMs"]);
        while (!_cancellationTokenSource.IsCancellationRequested)
        {
            await Task.Delay(int.Parse(_configuration["ObserveDelayMs"]), _cancellationTokenSource.Token);
            Log.Logger.Information("Loading loop started new iteration");

            foreach (var comment in await _commentsRepository.GetRange())
            {
                OnNewInfoEvent?.Invoke(comment);
            }
        }
        Log.Logger.Information("Loading stopped");
    }
}