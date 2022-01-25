namespace VkAPITester;

public static class Program
{
    public static void Main()
    {
        var client = new ApiClient();
        var storage = new AnalyzedDataStorage();
        var queue = new ObservedPostsQueue(client, storage);
        var j = 1;
        var flag = true;
        var a = client.Auth(8046073, "rvSXQVVe9QI7Xq1hjKNm", "041d6301041d6301041d6301940467a6f80041d041d630165c7f58fa7908b5e485a8377");
        var i = 0;
        while (i < 120)
        {
            var postId = client.GetPostId((ulong)j, -29573241).Result;
            if (!queue.Contains(postId) && postId != 0)
            {
                Console.WriteLine($"Got new post {postId}");
                queue.AddDataSource(-29573241, postId);
            }
            Thread.Sleep(60000);
            i++;

            if (j != 3 && flag)
            {
                j++;
            }
            else
            {
                j = 1;
                flag = false;
            }
        }

        queue.Clear();
        client.LogOut();
        Console.ReadLine();
    }
}