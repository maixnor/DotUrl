using Mxnr.DotUrl.Share;

namespace Mxnr.DotUrl.Persistence.Tests;

public class FileStoreTests
{
    private List<Request> _requests = new List<Request>
    {
        new("https://google.com", HttpMethod.Get, null),
        new("https://bing.com", HttpMethod.Get, null),
        new("https://yahoo.com", HttpMethod.Get, null)
    };

    
    [Fact]
    public void SaveAndRetrieveRequests()
    {
        FileStore.SaveRequests(_requests);
        var actual = FileStore.RetrieveRequests();
        actual.Should().Equal(_requests);
    }
}