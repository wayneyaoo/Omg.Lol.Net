namespace Omg.Lol.Net.Infrastructure;

using System.Net.Http;
using System.Threading.Tasks;

public interface IHttpClient
{
    public Task<HttpResponseMessage> RequestAsync(HttpRequestMessage requestMessage);

    public Task<HttpResponseMessage> GetAsync(string url);

    public Task<HttpResponseMessage> GetAsync(string url, string bearerToken);

    public Task<HttpResponseMessage> PostAsync(string url, string bearerToken, HttpContent content);

    public Task<HttpResponseMessage> PostAsync(string url, string bearerToken, string content);

    public Task<HttpResponseMessage> DeleteAsync(string url, string bearerToken);
}
