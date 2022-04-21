﻿using System.Diagnostics;
using Mxnr.DotUrl.Console;
using Mxnr.DotUrl.Share;
using Spectre.Console;

var selectedIndex = 0;

var requests = new List<Request>
{
    new("https://google.com", HttpMethod.Get),
    new("https://bing.com", HttpMethod.Get),
    new("https://yahoo.com", HttpMethod.Get)
};

Redraw();
GameLoop();

void Redraw()
{
    var tree = Setup.Tree(requests, selectedIndex);
    Console.Clear();
    AnsiConsole.Write(tree);
}

void GameLoop()
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
                requests.Add(new Request("newly inserted", HttpMethod.Get));
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