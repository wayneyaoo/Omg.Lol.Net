namespace Omg.Lol.Net.Clients;

using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Clients.Implementation;
using Omg.Lol.Net.Infrastructure;

internal class OmgLolClient : IOmgLolClient
{
    public string Token { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public IAccountClient AccountClient { get; }

    public IAddressClient AddressClient { get; }

    public IServiceClient ServiceClient { get; }

    public IDNSClient DnsClient { get; }

    public IPurlsClient PurlsClient { get; }

    public IPastebinClient PastebinClient { get; }

    public IStatuslogClient StatuslogClient { get; }

    public OmgLolClient(IApiServerCommunicationHandler apiServerCommunicationHandler)
    {
        this.AddressClient = new AddressClient(apiServerCommunicationHandler)
        {
            Token = this.Token,
            Url = this.Url,
        };

        this.ServiceClient = new ServiceClient(apiServerCommunicationHandler)
        {
            Token = this.Token,
            Url = this.Url,
        };

        this.PastebinClient = new PastebinClient(apiServerCommunicationHandler)
        {
            Token = this.Token,
            Url = this.Url,
        };

        this.StatuslogClient = new StatuslogClient(apiServerCommunicationHandler)
        {
            Token = this.Token,
            Url = this.Url,
        };
    }
}
