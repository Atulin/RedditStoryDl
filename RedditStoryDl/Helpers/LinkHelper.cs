namespace RedditStoryDl.Helpers;

public static class LinkHelper
{
    public static string? FindNextLink(string body, string nextText = "[Next]")
    {
        var span = body.ToLowerInvariant().AsSpan();
        var nextTextSpan = nextText.ToLowerInvariant().AsSpan();
        var buffer = new List<char>();

        var found = false;
        var nextTextIndex = 0;
        var parsingLink = false;
        
        foreach (var ch in span)
        {
            if (ch == '\\') continue;

            if (ch == '[' && !found)
            {
                // AnsiConsole.MarkupInterpolated($"[purple]{Markup.Escape(ch.ToString())}[/]");
                found = true;
            }
            else if (found && nextTextIndex == nextTextSpan.Length - 1)
            {
                if (ch == ']') continue;
                if (ch == '(')
                {
                    // AnsiConsole.MarkupInterpolated($"[blue]{Markup.Escape(ch.ToString())}[/]");
                    parsingLink = true;
                }
                else if (ch == ')')
                {
                    // AnsiConsole.MarkupInterpolated($"[blue]{Markup.Escape(ch.ToString())}[/]");
                    break;
                }
                else if (parsingLink)
                {
                    // AnsiConsole.MarkupInterpolated($"[green]{Markup.Escape(ch.ToString())}[/]");
                    buffer.Add(ch);
                }
            }
            else if (found && ch == nextTextSpan[nextTextIndex])
            {
                // AnsiConsole.MarkupInterpolated($"[yellow]{Markup.Escape(ch.ToString())}[/]");
                nextTextIndex++;
            }
            else
            {
                // AnsiConsole.MarkupInterpolated($"[red]{Markup.Escape(ch.ToString())}[/]");
                found = false;
                nextTextIndex = 0;
            }
        }

        return buffer.Count > 0 ? string.Join("", buffer) : null;
    }
}