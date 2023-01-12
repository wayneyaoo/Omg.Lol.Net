namespace Omg.Lol.Net.Clients;

using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Clients.Implementation;
using Omg.Lol.Net.Infrastructure;

internal class OmgLolClient : IOmgLolClient, ITokenBearer
{
    public string Token { get; set; }

    public string Url { get; set; }

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

        this.AddressClient = new AddressClient(this.apiServerCommunicationHandler)
        {
            Token = this.Token,
            Url = this.Url,
        };

        this.ServiceClient = new ServiceClient(this.apiServerCommunicationHandler)
        {
            Token = this.Token,
            Url = this.Url,
        };
    }
}
