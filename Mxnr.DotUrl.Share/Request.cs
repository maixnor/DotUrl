namespace Mxnr.DotUrl.Share;

public record Request(string Uri, HttpMethod Method, Response? Response);