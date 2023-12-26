using System.Diagnostics.Contracts;

namespace RedditStoryDl.Helpers;

public class DirHelpers
{
    public static string GetFilePath(string directory, string title)
    {
        var filename = $"{title}-{Random.Shared.Next()}.md";
        return FullPath(directory, filename);
    }

    [Pure]
    public static string FullPath(params string[] segments) => Path.GetFullPath(Path.Join(segments));

}