using System.Net;

namespace Mxnr.DotUrl.Share;

public record Response(string Result, long Milliseconds, HttpStatusCode Code, bool Success);
