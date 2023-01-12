namespace Omg.Lol.Net.Clients;

using Omg.Lol.Net.Infrastructure;

public static class OmgLolClientBuilder
{
    /// <summary>
    /// This method is for client code use without DI. Note a new instance is crated on every call. Client code needs to implement cache if desired. The method uses a singleton HttpClient internally as well.
    /// </summary>
    /// <para name="apiKey">The omg.lol API key.</para>
    /// <returns>A function <see cref="OmgLolClient"/> client.</returns>
    public static IOmgLolClient CreateDefault(string apiKey)
        => new OmgLolClient(new ApiServerCommunicationHandler(new HttpClientFactory()))
        {
            Token = apiKey,
            Url = Constants.API_SERVER_ADDRESS,
        };

    /// <summary>
    ///
    /// </summary>
    /// <param name="apiKey"></param>
    /// <param name="httpClientFactory"></param>
    /// <returns></returns>
    public static IOmgLolClient Create(string apiKey, IHttpClientFactory httpClientFactory)
        => new OmgLolClient(new ApiServerCommunicationHandler(httpClientFactory))
        {
            Token = apiKey,
            Url = Constants.API_SERVER_ADDRESS,
        };
}
