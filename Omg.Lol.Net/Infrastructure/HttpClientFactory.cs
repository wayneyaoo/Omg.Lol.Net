namespace Omg.Lol.Net.Infrastructure;

public class HttpClientFactory : IHttpClientFactory
{
    public IHttpClient GetHttpClient() => new DefaultHttpClient();
}
