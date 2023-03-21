namespace Omg.Lol.Net.Infrastructure;

using System.Net.Http;

public interface IHttpClientFactory
{
    /// <summary>
    /// Create a default Http client.
    /// </summary>
    /// <returns>An <see cref="IHttpClient"/> instance.</returns>
    public IHttpClient GetHttpClient();

    /// <summary>
    /// Create a Http client using the given instance of <see cref="HttpClient"/> as the backing implementation.
    /// </summary>
    /// <param name="httpClient">External HttpClient instance.</param>
    /// <returns>An <see cref="IHttpClient"/> instance.</returns>
    public IHttpClient GetHttpClient(HttpClient httpClient);
}
