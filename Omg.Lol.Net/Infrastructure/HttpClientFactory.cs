namespace Omg.Lol.Net.Infrastructure;

public sealed class HttpClientFactory : IHttpClientFactory
{
    public IHttpClient GetHttpClient() => new DefaultHttpClient();
}
