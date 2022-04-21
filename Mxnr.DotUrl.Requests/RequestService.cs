using System.Diagnostics;
using Mxnr.DotUrl.Share;

namespace Mxnr.DotUrl.Requests;

public class RequestService
{
    private HttpClient _client = new();
    
    public async Task<Response> SendRequest(Request request)
    {
        var stopwatch = Stopwatch.StartNew();
        var message = _client.Send(new HttpRequestMessage(request.Method, request.Uri));
        var milliseconds = stopwatch.ElapsedMilliseconds;
        stopwatch.Stop();
        return new Response(
            await message.Content.ReadAsStringAsync(),
            milliseconds,
            message.Headers.ToString());
    }    
}

