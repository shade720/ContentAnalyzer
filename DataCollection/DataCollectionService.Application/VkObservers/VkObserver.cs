using DataCollectionService.Domain.Abstractions;
using Microsoft.Extensions.Configuration;

namespace DataCollectionService.Application.VkObservers;

public abstract class VkObserver<T>
{
    protected readonly IVkApi VkApi;
    protected readonly IConfiguration Configuration;

    protected readonly HashSet<long> CollectedIds;
    protected readonly CancellationTokenSource CancellationTokenSource;

    protected VkObserver(IVkApi vkApi, IConfiguration configuration)
    {
        VkApi = vkApi;
        Configuration = configuration;
        CollectedIds = new HashSet<long>();
        CancellationTokenSource = new CancellationTokenSource();
    }

    public delegate Task OnVkNewInfo(object sender, T obj);
    public abstract event OnVkNewInfo? OnNewInfoEvent;
}