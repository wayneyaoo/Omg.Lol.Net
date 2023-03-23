namespace Omg.Lol.Net.Infrastructure;

public sealed class HttpClientFactory : IHttpClientFactory
{
    /// <inheritdoc/>
    public IHttpClient GetHttpClient() => new DefaultHttpClient();
}
