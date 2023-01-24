namespace Omg.Lol.Net.Clients;

using System;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Clients.Implementation;
using Omg.Lol.Net.Infrastructure;

internal class OmgLolClient : IOmgLolClient
{
    public string Token { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public IAccountClient AccountClient => null;

    public IAddressClient AddressClient => this.addressClient.Value;

    public IServiceClient ServiceClient => this.serviceClient.Value;

    public IDnsClient DnsClient => this.dnsClient.Value;

    public IPurlsClient PurlsClient => this.purlsClient.Value;

    public IPastebinClient PastebinClient => this.pastebinClient.Value;

    public IStatuslogClient StatuslogClient => this.statuslogClient.Value;

    private Lazy<AddressClient> addressClient;

    private Lazy<ServiceClient> serviceClient;

    private Lazy<DnsClient> dnsClient;

    private Lazy<PurlsClient> purlsClient;

    private Lazy<PastebinClient> pastebinClient;

    private Lazy<StatuslogClient> statuslogClient;

    public OmgLolClient(IApiServerCommunicationHandler apiServerCommunicationHandler)
    {
        this.addressClient = new Lazy<AddressClient>(() => new AddressClient(apiServerCommunicationHandler)
        {
            Token = this.Token,
            Url = this.Url,
        });

        this.serviceClient = new Lazy<ServiceClient>(() => new ServiceClient(apiServerCommunicationHandler)
        {
            Token = this.Token,
            Url = this.Url,
        });

        this.dnsClient = new Lazy<DnsClient>(() => new DnsClient(apiServerCommunicationHandler)
        {
            Token = this.Token,
            Url = this.Url,
        });

        this.pastebinClient = new Lazy<PastebinClient>(() => new PastebinClient(apiServerCommunicationHandler)
        {
            Token = this.Token,
            Url = this.Url,
        });

        this.statuslogClient = new Lazy<StatuslogClient>(() => new StatuslogClient(apiServerCommunicationHandler)
        {
            Token = this.Token,
            Url = this.Url,
        });

        this.purlsClient = new Lazy<PurlsClient>(() => new PurlsClient(apiServerCommunicationHandler)
        {
            Token = this.Token,
            Url = this.Url,
        });
    }
}
