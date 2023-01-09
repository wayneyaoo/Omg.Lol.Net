namespace Omg.Lol.Net.Clients;

public interface IOmgLolClient
{
    public IAccountClient AccountClient { get; }

    public IAddressClient AddressClient { get; }

    public IDNSClient DnsClient { get; }

    public IPurlsClient PurlsClient { get; }

    public IPastebinClient PastebinClient { get; }

    public IStatusLogClient StatusLogClient { get; }
}
