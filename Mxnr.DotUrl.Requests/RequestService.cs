using System.Diagnostics;
using Mxnr.DotUrl.Share;

namespace Mxnr.DotUrl.Requests;

public class RequestService
{
    private readonly HttpClient _client;

    public RequestService(HttpClient client)
    {
        _client = client;
    }

    public async Task<Response> SendRequestAsync(Request request)
    {
        var stopwatch = Stopwatch.StartNew();
        var message = await _client.SendAsync(new HttpRequestMessage(request.Method, request.Uri));
        var milliseconds = stopwatch.ElapsedMilliseconds;
        stopwatch.Stop();
        return new Response(
            await message.Content.ReadAsStringAsync(),
            milliseconds,
            message.StatusCode,
            message.IsSuccessStatusCode);
    }    
}

