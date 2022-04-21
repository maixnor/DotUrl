using Mxnr.DotUrl.Share;

namespace Mxnr.DotUrl.Requests.Tests;

public class RequestServiceTests
{
    private readonly RequestService _sut;

    public RequestServiceTests()
    {
        _sut = new RequestService();
    }
    
    [Fact]
    public async Task SendRequest_GetsAResult()
    {
        var (result, milliseconds, headers) = await _sut.SendRequest(new Request("https://www.google.com", HttpMethod.Get, null));
        milliseconds.Should().BeGreaterThan(0);
        result.Should().NotBeNullOrEmpty().And.Contain("google");
        headers.Should().NotBeNullOrEmpty();
    }
}