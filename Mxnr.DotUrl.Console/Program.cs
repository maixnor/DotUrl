using System.Diagnostics;
using Mxnr.DotUrl.Console;
using Mxnr.DotUrl.Requests;
using Mxnr.DotUrl.Share;
using Spectre.Console;

var selectedIndex = 0;
var requestService = new RequestService(new HttpClient());

var requests = new List<Request>
{
    new("https://www.google.com", HttpMethod.Get, null),
    new("https://www.bing.com", HttpMethod.Get, null),
    new("https://www.yahoo.com", HttpMethod.Get, null)
};

Redraw();
await GameLoop();

void Redraw()
{
    var tree = Setup.Tree(requests, selectedIndex);
    Console.Clear();
    AnsiConsole.Write(tree);
}

async Task GameLoop()
{
    while (true)
    {
        var key = Console.ReadKey();
        var maxIndex = requests.Count - 1;
        switch (key.Key)
        {
            // exit the application
            case ConsoleKey.Escape:
                Environment.Exit(0);
                break;
            // start of *should not do anything as it is needed for typing*
            case ConsoleKey.Backspace:
                break;
            case ConsoleKey.Spacebar:
                break;
            // end of *should not do anything as it is needed for typing*
            // should trigger editing of request (at best in a modal style popup)
            case ConsoleKey.Tab:
                break;
            // should trigger the currently selected request
            case ConsoleKey.Enter:
                var request = requests.ElementAt(selectedIndex);
                requests[selectedIndex] = request with {Response = await requestService.SendRequestAsync(request)};
                Redraw();
                break;
            // end/home go to last and first item
            case ConsoleKey.End:
                selectedIndex = maxIndex;
                Redraw();
                break;
            case ConsoleKey.Home:
                selectedIndex = 0;
                Redraw();
                break;
            // left/right may handle folding/expanding of list(s)
            case ConsoleKey.LeftArrow:
                break;
            case ConsoleKey.RightArrow:
                break;
            // up/down navigate the tree
            case ConsoleKey.UpArrow:
                selectedIndex = selectedIndex <= 0 ? 0 : selectedIndex - 1;
                Redraw();
                break;
            case ConsoleKey.DownArrow:
                selectedIndex = selectedIndex == maxIndex ? maxIndex : selectedIndex + 1;
                Redraw();
                break;
            // adds a new request
            case ConsoleKey.Insert:
                requests.Add(new Request("newly inserted", HttpMethod.Get, null));
                Redraw();
                break;
            // deletes currently selected request
            case ConsoleKey.Delete:
                var temp = selectedIndex;
                selectedIndex--;
                requests.RemoveAt(temp);
                Redraw();
                break;
            default:
                throw new Exception("This button may not be pressed!!!");
        }
    }
}
