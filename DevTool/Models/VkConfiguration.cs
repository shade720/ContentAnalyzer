namespace DevTool.Models;

public class VkConfiguration
{
    public List<int> Communities { get; init; } = new();
    public int ApplicationId { get; init; }
    public string SecureKey { get; init; } = string.Empty;
    public string ServiceAccessKey { get; init; } = string.Empty;
}