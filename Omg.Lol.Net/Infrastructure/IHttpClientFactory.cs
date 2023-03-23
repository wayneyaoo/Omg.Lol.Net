namespace Omg.Lol.Net.Infrastructure;

public interface IHttpClientFactory
{
    /// <summary>
    /// Create a default Http client.
    /// </summary>
    /// <returns>An <see cref="IHttpClient"/> instance.</returns>
    public IHttpClient GetHttpClient();
}
