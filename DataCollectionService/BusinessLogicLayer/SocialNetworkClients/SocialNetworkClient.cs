using Common.EntityFramework;

namespace DataCollectionService.BusinessLogicLayer.SocialNetworkClients;

public abstract class SocialNetworkClient
{
    public abstract void AddCommunity(long communityId);
    public abstract void StartCollecting();
    public abstract void StopCollecting();
    public abstract void Subscribe(Action<Comment> handler);
    public abstract void Unsubscribe(Action<Comment> handler);
}