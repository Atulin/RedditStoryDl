using System.Text;
using RedditStoryDl.Clients;
using RedditStoryDl.Helpers;
using Spectre.Console;

Console.OutputEncoding = Encoding.UTF8;

var client = new RedditClient(new HttpClient());

Directory.CreateDirectory("./saved");

// https://www.reddit.com/r/NatureofPredators/comments/12ifo9e/persistence_journalism/
var url = AnsiConsole.Ask<string>("Enter the URL of the first post of the series:").Trim('/');

var post = await AnsiConsole.Status()
    .SpinnerStyle(Style.Parse("yellow bold"))
    .StartAsync("Fetching the post...", async _ => await client.GetPost($"{url}.json"));

if (post is null)
{
    AnsiConsole.MarkupLine("[red]Could not load the post[/]");
}
else
{
    AnsiConsole.Write(new Rule());
    AnsiConsole.MarkupLineInterpolated($"[green]Found post[/] [bold italic]{post.Title}[/] [green]by[/] [bold italic]{post.Author}[/]");
    var table = new Table().AddColumns("Property", "Value").HideHeaders()
        .Border(TableBorder.None)
        .AddRow("Subreddit:", post.Subreddit)
        .AddRow("Created:", post.Created.ToString("dd MMMM yyyy, hh:mm"))
        .AddRow("Last edit:", post.Edited.ToString("dd MMMM yyyy, hh:mm"))
        .AddRow(new Markup("Score:"), Markup.FromInterpolated($"[green]\u25b2 {post.Upvotes}[/]  [red]\u25bc {post.Downvotes}[/]  ({post.UpvotePercentage * 100}%)"))
        .AddRow("NSFW:", post.IsNsfw ? "yes" : "no");
    table.Columns[0].PadRight(3);
    AnsiConsole.Write(table);
    
    AnsiConsole.Write(new Rule());
    
    await AnsiConsole.Status()
        .SpinnerStyle(Style.Parse("yellow bold"))
        .StartAsync("Saving...", async _ => await File.WriteAllTextAsync($"./saved/{post.Title}-{Random.Shared.Next()}.md", post.Selftext));

    string? link = LinkHelper.FindNextLink(post.Selftext);

    var index = 2;
    while (link is not null)
    {
        AnsiConsole.MarkupLineInterpolated($"[green]Found next chapter: [/] {link}");
        post = await AnsiConsole.Status()
            .SpinnerStyle(Style.Parse("yellow bold"))
            .StartAsync($"({index++}) Fetching the post...", async _ => await client.GetPost($"{link}.json"));
        
        if (post is null)
        {
            AnsiConsole.MarkupLine("[red]Could not load the post[/]");
            break;
        }
        
        await AnsiConsole.Status()
            .SpinnerStyle(Style.Parse("yellow bold"))
            .StartAsync("Saving...", async _ => await File.WriteAllTextAsync($"./saved/{post.Title}-{Random.Shared.Next()}.md", post.Selftext));
        
        link = LinkHelper.FindNextLink(post.Selftext);
    }
}