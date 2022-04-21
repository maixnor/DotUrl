using System.Text.Json;
using Mxnr.DotUrl.Share;

namespace Mxnr.DotUrl.Persistence;

public class FileStore
{
    private const string DefaultPath = "requests.json";

    public static void SaveRequests(List<Request> requests)
    {
        File.WriteAllText(DefaultPath,JsonSerializer.Serialize(requests));
    }

    public static List<Request> RetrieveRequests()
    {
        if (!File.Exists(DefaultPath)) return new List<Request>(); // no exception here as on first launch there is not such file
        var text = File.ReadAllText(DefaultPath);
        return JsonSerializer.Deserialize<List<Request>>(text) ?? throw new FileNotReadCorrectlyException("The file was not read correctly");
    }
}

public class FileNotReadCorrectlyException : Exception
{
    public FileNotReadCorrectlyException(string theFileWasNotReadCorrectly) : base(theFileWasNotReadCorrectly)
    {
        
    }
}