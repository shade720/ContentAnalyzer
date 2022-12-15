using Grpc.Net.Client;

namespace DevTool.Models;

internal abstract class ServiceClient : IDisposable
{
    protected readonly GrpcChannel Channel;
    protected ServiceClient(string host)
    {
        Channel = GrpcChannel.ForAddress(host);
    }
    public abstract void StartService();
    public abstract void StopService();
    public abstract string GetLogFile(DateTime date);

    public void Dispose()
    {
        Channel.Dispose();
    }
}