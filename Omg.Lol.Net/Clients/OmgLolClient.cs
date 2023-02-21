namespace Omg.Lol.Net.Clients;

using System;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Clients.Implementation;
using Omg.Lol.Net.Infrastructure;

internal class OmgLolClient : IOmgLolClient
{
    public string Token { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public IAccountClient AccountClient => this.accountClient.Value;

    public IAddressClient AddressClient => this.addressClient.Value;

    public IServiceClient ServiceClient => this.serviceClient.Value;

    public IDnsClient DnsClient => this.dnsClient.Value;

    public IPurlsClient PurlsClient => this.purlsClient.Value;

    public IPastebinClient PastebinClient => this.pastebinClient.Value;

    public IStatuslogClient StatuslogClient => this.statuslogClient.Value;

    public IDirectoryClient DirectoryClient => this.directoryClient.Value;

    private readonly Lazy<AddressClient> addressClient;

    private readonly Lazy<ServiceClient> serviceClient;

    private readonly Lazy<DnsClient> dnsClient;

    private readonly Lazy<PurlsClient> purlsClient;

    private readonly Lazy<PastebinClient> pastebinClient;

    private readonly Lazy<StatuslogClient> statuslogClient;

    private readonly Lazy<AccountClient> accountClient;

    private readonly Lazy<DirectoryClient> directoryClient;

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

        this.accountClient = new Lazy<AccountClient>(() => new AccountClient(apiServerCommunicationHandler)
        {
            Token = this.Token,
            Url = this.Url,
        });

        this.directoryClient = new Lazy<DirectoryClient>(() => new DirectoryClient(apiServerCommunicationHandler)
        {
            Token = this.Token,
            Url = this.Url,
        });
    }
}
