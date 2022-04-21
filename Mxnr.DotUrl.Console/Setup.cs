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
            // request table
            node.AddNode(new Table()
                    .RoundedBorder()
                    .AddColumn("Field")
                    .AddColumn("Value")
                    .AddRow("URI", uri)
                    .AddRow("Method", httpMethod.Method)
            );
            // response table
            if (response is not null)
            {
                node.AddNode(new Table()
                    .RoundedBorder()
                    .AddColumn("Field")
                    .AddColumn("Value")
                    .AddRow("Time", response.Milliseconds.ToString()) // use humanoid here
                    .AddRow("Result", response.Result)
                    .AddRow("Headers", response.Headers)
                );
            }
        }

        // TODO better logic here
        root.Nodes[expandedIndex].Expand();

        return root;
    }
}