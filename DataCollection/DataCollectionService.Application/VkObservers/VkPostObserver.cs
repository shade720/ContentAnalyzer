﻿using DataCollectionService.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace DataCollectionService.Application.VkObservers;

public class VkPostObserver : VkObserver<long>, IDisposable
{
    public long CommunityId { get; }

    public override event OnVkNewInfo? OnNewInfoEvent;

    public VkPostObserver(
        long communityId, 
        IVkApi vkApi, 
        IConfiguration configuration) 
        : base(vkApi, configuration)
    {
        CommunityId = communityId;
        Task.Run(async () => await PullingLastPostLoop());
        Log.Logger.Information("Post observer created for {0}", CommunityId);
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
            Log.Logger.Information("Post observer {0} started new iteration", CommunityId);

            var lastPostId = await VkApi.GetLastPostIdAsync(CommunityId);

            if (CollectedIds.Contains(lastPostId) || OnNewInfoEvent is null)
                continue;

            Log.Logger.Information("New post released {0} community {1}", 
                lastPostId, CommunityId);

            CollectedIds.Add(lastPostId);

            if (OnNewInfoEvent is not null)
                await OnNewInfoEvent.Invoke(this, lastPostId);
        }
        Log.Logger.Information("Post observing {0} stopped", CommunityId);
    }

    public void Dispose()
    {
        CancellationTokenSource?.Cancel();
        OnNewInfoEvent = null;
        Log.Logger.Information("Post observer {0} disposed", CommunityId);
    }
}