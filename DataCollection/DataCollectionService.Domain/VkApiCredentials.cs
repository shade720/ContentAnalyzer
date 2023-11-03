namespace DataCollectionService.Domain;

public class VkApiCredentials
{
    public ulong ApplicationId { get; init; }
    public string SecureKey { get; init; }
    public string ServiceAccessKey { get; init; }
}