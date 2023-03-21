namespace Omg.Lol.Net.Infrastructure;

using System.Net.Http;

public sealed class HttpClientFactory : IHttpClientFactory
{
    /// <inheritdoc/>
    public IHttpClient GetHttpClient() => new DefaultHttpClient();

    /// <inheritdoc/>
    public IHttpClient GetHttpClient(HttpClient httpClient) => new DefaultHttpClient(httpClient);
}
