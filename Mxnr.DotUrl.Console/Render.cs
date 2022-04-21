using Humanizer;
using Mxnr.DotUrl.Share;
using Spectre.Console;

namespace Mxnr.DotUrl.Console;

public static class Setup
{
    public static Tree Tree(List<Request> requests, int expandedIndex)
    {
        AnsiConsole.WriteLine();

        var root = new Tree("Mxnr.DotUrl")
            .Style(Style.Parse("red"))
            .Guide(TreeGuide.Line);

        foreach (var (uri, httpMethod, response) in requests)
        {
            // IDEA table caption (response table) for request time 
            
            var node = root.AddNode(uri).Collapse();
            var requestTable = new Table()
                .RoundedBorder()
                .AddColumn("Field")
                .AddColumn("Value")
                .AddRow("URI", uri)
                .AddRow("Method", httpMethod.Method);
            node.AddNode(requestTable);
            if (response is not null)
            {
                var responseTable = new Table()
                    .RoundedBorder()
                    .BorderColor(response.Success ? Color.LightGreen : Color.DarkRed)
                    .AddColumn("Field")
                    .AddColumn("Value")
                    .AddRow("Time", TimeSpan.FromMilliseconds(response.Milliseconds).Humanize()) // TODO use humanize here for time display
                    .AddRow("HTTP Status", $"({(int)response.Code}) {response.Code.ToString().Humanize()}")
                    .AddRow("Result", Markup.Escape(response.Result[..Math.Min(300, response.Result.Length)]));
                node.AddNode(responseTable);
            }
        }

        root.Nodes[expandedIndex].Expand();

        return root;
    }
}