namespace Omg.Lol.Net.Clients;

using System;
using System.Threading.Tasks;
using Omg.Lol.Net.Infrastructure;

public static class OmgLolClientBuilder
{
    /// <summary>
    /// Create an <see cref="IOmgLolClient"/> instance with API key. Every call made to the method will create a new instance.
    /// </summary>
    /// <param name="apiKey">The omg.lol API key.</param>
    /// <returns>A functional <see cref="IOmgLolClient"/> client.</returns>
    public static IOmgLolClient Create(string apiKey)
        => Create(apiKey, new HttpClientFactory());

    /// <summary>
    /// Create an <see cref="IOmgLolClient"/> instance with API key and a custom httpClient factory. You can inject your instance of <see cref="IHttpClient"/> here.
    /// </summary>
    /// <param name="apiKey">The omg.lol API key.</param>
    /// <param name="httpClientFactory">Custom httpClient factory.</param>
    /// <returns>A functional <see cref="IOmgLolClient"/> client.</returns>
    public static IOmgLolClient Create(string apiKey, IHttpClientFactory httpClientFactory)
        => new OmgLolClient(new ApiServerCommunicationHandler(httpClientFactory))
        {
            Token = apiKey,
            Url = Constants.API_SERVER_ADDRESS,
        };

    /// <summary>
    /// Create an <see cref="IOmgLolClient"/> instance with API key provider. You can customize the key fetching logic here. Default <see cref="IHttpClient"/> is used internally.
    /// </summary>
    /// <param name="provider">The key provider to fetch API key.</param>
    /// <returns>A functional <see cref="IOmgLolClient"/> client.</returns>
    public static Task<IOmgLolClient> Create(IApiKeyProvider provider)
        => Create(async () => await provider.GetApiKeyAsync().ConfigureAwait(false));

    /// <summary>
    /// Create an <see cref="IOmgLolClient"/> instance with API key provisioning callback. You can customize the key fetching logic here. Default <see cref="IHttpClient"/> is used internally.
    /// </summary>
    /// <param name="apiKeyProvisioningCallback">A delegate to fetch api key.</param>
    /// <returns>A functional <see cref="IOmgLolClient"/> client.</returns>
    public static async Task<IOmgLolClient> Create(Func<Task<string>> apiKeyProvisioningCallback)
        => Create(await apiKeyProvisioningCallback().ConfigureAwait(false));

    /// <summary>
    /// Create an <see cref="IOmgLolClient"/> instance with API key provider. You can customize the key fetching logic and injet your own <see cref="IHttpClient"/> instance here.
    /// </summary>
    /// <param name="provider">The key provider to fetch API key.</param>
    /// <param name="httpClientFactory">Custom httpClient factory.</param>
    /// <returns>A functional <see cref="IOmgLolClient"/> client.</returns>
    public static async Task<IOmgLolClient> Create(IApiKeyProvider provider, IHttpClientFactory httpClientFactory)
        => Create(await provider.GetApiKeyAsync().ConfigureAwait(false), httpClientFactory);

    /// <summary>
    /// Create an <see cref="IOmgLolClient"/> instance with API key provisioning call back. You can customize the key fetching logic and inject your own <see cref="IHttpClient"/> instance here.
    /// </summary>
    /// <param name="apiKeyProvisioningCallback">A delegate to fetch api key.</param>
    /// <param name="httpClientFactory">Custom httpClient factory.</param>
    /// <returns>A functional <see cref="IOmgLolClient"/> client.</returns>
    public static async Task<IOmgLolClient> Create(
        Func<Task<string>> apiKeyProvisioningCallback,
        IHttpClientFactory httpClientFactory)
        => Create(await apiKeyProvisioningCallback().ConfigureAwait(false), httpClientFactory);
}
