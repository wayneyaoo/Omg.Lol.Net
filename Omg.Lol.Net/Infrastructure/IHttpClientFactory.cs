namespace Omg.Lol.Net.Infrastructure;

public interface IHttpClientFactory
{
    public IHttpClient GetHttpClient();
}
