namespace Omg.Lol.Net.Clients.Implementation;

using Omg.Lol.Net.Clients.Abstract;

public class DnsClient : IDnsClient
{
    public string Token { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;
}
