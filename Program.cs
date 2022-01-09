namespace VkAPITester;

public static class Program
{
    public static void Main()
    {
        var client = new ApiClient();
        client.Auth();
        client.GetPosts();
    }
}