namespace Omg.Lol.Net.Infrastructure;

public interface IApiInfoCarrier
{
    public string Token { get; set; }

    public string Url { get; set; }
}
