namespace Omg.Lol.Net.Clients;

using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Clients.Implementation;
using Omg.Lol.Net.Infrastructure;

public class OmgLolClient : IOmgLolClient, ITokenBearer
{
    public string Token { get; set; }

    public IAccountClient AccountClient { get; }

    public IAddressClient AddressClient { get; }

    public IServiceClient ServiceClient { get; }

    public IDNSClient DnsClient { get; }

    public IPurlsClient PurlsClient { get; }

    public IPastebinClient PastebinClient { get; }

    public IStatusLogClient StatusLogClient { get; }

    private readonly IApiServerCommunicationHandler apiServerCommunicationHandler;

    public OmgLolClient(IApiServerCommunicationHandler apiServerCommunicationHandler)
    {
        this.apiServerCommunicationHandler = apiServerCommunicationHandler;
    }

    /// <summary>
    /// This method is for client code use without DI. Note a new instance is crated on every call. Client code needs to implement cache if desired.
    /// </summary>
    /// <para name="apiKey">The omg.lol API key.</para>
    /// <returns></returns>
    public static OmgLolClient CreateDefaultClient(string apiKey)
        => new (new ApiServerCommunicationHandler(new HttpClientFactory()))
        {
            Token = apiKey,
        };
}
