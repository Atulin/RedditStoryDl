using System.Net.Http.Json;
using System.Text.Json;
using RedditStoryDl.JsonModels;

namespace RedditStoryDl.Clients;

public sealed class RedditClient
{
    private const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:122.0) Gecko/20100101 Firefox/122.0";
    
    private readonly HttpClient _client;

    public RedditClient(HttpClient client)
    {
        _client = client;
        _client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
    }

    public async Task<Post?> GetPost(string url)
    {
        // TODO: No bueno for AOT and trimming, tracked by #2
        var data = await _client.GetFromJsonAsync<List<JsonElement>>(url);

        var postData = data?[0].Deserialize(RootJsonContext.Default.Root);

        return postData?.Data.Children[0].Post;
    }
}