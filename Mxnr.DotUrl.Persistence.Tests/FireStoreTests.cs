using Mxnr.DotUrl.Share;

namespace Mxnr.DotUrl.Persistence.Tests;

public class FileStoreTests
{
    private List<Request> _requests = new List<Request>
    {
        new("https://google.com", HttpMethod.Get),
        new("https://bing.com", HttpMethod.Get),
        new("https://yahoo.com", HttpMethod.Get)
    };

    
    [Fact]
    public void SaveAndRetrieveRequests()
    {
        FileStore.SaveRequests(_requests);
        var actual = FileStore.RetrieveRequests();
        actual.Should().Equal(_requests);
    }
}