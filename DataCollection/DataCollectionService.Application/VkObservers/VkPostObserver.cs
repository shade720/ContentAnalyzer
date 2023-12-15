using DataCollectionService.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace DataCollectionService.Application.VkObservers;

public class VkPostObserver : VkObserver<long>, IDisposable
{
    public override event EventHandler<long>? OnNewInfoEvent;
    public long CommunityId { get; }

    public VkPostObserver(
        long communityId, 
        IVkApi vkApi, 
        IConfiguration configuration) 
        : base(vkApi, configuration)
    {
        CommunityId = communityId;
        Task.Run(PullingLastPostLoop);
        Log.Logger.Information("Post observer created for community: {0}", CommunityId);
    }

    private async Task PullingLastPostLoop()
    {
        if (Configuration["ScanPostDelay"] is null)
            throw new ApplicationException($"ScanPostDelay in {nameof(PullingLastPostLoop)} was null!");

        var scanningPeriod = int.Parse(Configuration["ScanPostDelay"]!);

        while (!CancellationTokenSource.IsCancellationRequested)
        {
            await Task.Delay(scanningPeriod, CancellationTokenSource.Token)
                .ContinueWith(_ => { }); //to avoid exception from cancellation
            Log.Logger.Information("Post observer started new iteration after {0}ms delay. Community: {1}", scanningPeriod, CommunityId);

            var lastPostId = await VkApi.GetLastPostIdAsync(CommunityId);

            if (CancellationTokenSource.IsCancellationRequested || CollectedIds.Contains(lastPostId) || OnNewInfoEvent is null)
                continue;

            Log.Logger.Information("New post released! Community/post: {0}/{1}", 
                CommunityId, lastPostId);

            CollectedIds.Add(lastPostId);

            OnNewInfoEvent?.Invoke(this, lastPostId);
        }
        Log.Logger.Information("Post observing stopped. Community: {0}", CommunityId);
    }

    public void Dispose()
    {
        CancellationTokenSource?.Cancel();
        OnNewInfoEvent = null;
        Log.Logger.Information("Post observer disposed. Community: {0}", CommunityId);
    }
}