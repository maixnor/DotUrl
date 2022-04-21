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

        foreach (var (uri, httpMethod) in requests)
        {
            var node = root.AddNode(uri).Collapse();
            node.AddNode(new Table()
                    .RoundedBorder()
                    .AddColumn("Field")
                    .AddColumn("Value")
                    .AddRow("URI", uri)
                    .AddRow("Method", httpMethod.Method));
        }

        // TODO better logic here
        root.Nodes[expandedIndex].Expand();

        return root;
    }
}