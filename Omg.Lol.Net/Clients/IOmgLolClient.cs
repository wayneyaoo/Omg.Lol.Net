namespace Omg.Lol.Net.Clients;

using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Infrastructure;

public interface IOmgLolClient : IApiInfoCarrier
{
    public IAccountClient AccountClient { get; }

    public IAddressClient AddressClient { get; }

    public IServiceClient ServiceClient { get; }

    public IDNSClient DnsClient { get; }

    public IPurlsClient PurlsClient { get; }

    public IPastebinClient PastebinClient { get; }

    public IStatuslogClient StatuslogClient { get; }
}
