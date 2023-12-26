using System.Text.Json.Serialization;
using RedditStoryDl.JsonConverters;

namespace RedditStoryDl.JsonModels;

public sealed class Root
{
    [JsonPropertyName("data")]
    public required Data Data { get; init; }
}

public sealed class Data
{
    [JsonPropertyName("children")]
    public required List<Child> Children { get; init; }
}

public sealed class Child
{
    [JsonPropertyName("data")]
    public required Post Post { get; init; }
}

public sealed class Post
{
    [JsonPropertyName("subreddit")]
    public required string Subreddit { get; init; }

    [JsonPropertyName("selftext")]
    [JsonConverter(typeof(NullStringToEmptyJsonConverter))]
    public required string Selftext { get; init; }

    [JsonPropertyName("ups")]
    public required int Upvotes { get; init; }

    [JsonPropertyName("downs")]
    public required int Downvotes { get; init; }

    [JsonIgnore]
    public double UpvotePercentage => Upvotes * 1.0 / (Upvotes + Downvotes);

    [JsonPropertyName("created")]
    [JsonConverter(typeof(UnixTimestampJsonConverter))]
    public required DateTimeOffset Created { get; init; }
    
    [JsonPropertyName("edited")]
    [JsonConverter(typeof(UnixTimestampJsonConverter))]
    public required DateTimeOffset Edited { get; init; }

    [JsonPropertyName("author")]
    public required string Author { get; init; }

    [JsonPropertyName("title")]
    public required string Title { get; init; }

    [JsonPropertyName("num_comments")]
    public required int CommentCount { get; init; }

    [JsonPropertyName("over_18")]
    public required bool IsNsfw { get; init; }
}