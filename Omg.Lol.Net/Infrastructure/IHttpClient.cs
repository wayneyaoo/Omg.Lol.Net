namespace Omg.Lol.Net.Infrastructure;

using System.Net.Http;
using System.Threading.Tasks;

public interface IHttpClient
{
    public Task<HttpResponseMessage> RequestAsync(HttpRequestMessage requestMessage);

    public Task<HttpResponseMessage> GetAsync(string url);

    public Task<HttpResponseMessage> GetAsync(string url, string bearerToken);

    public Task<HttpResponseMessage> PostAsync(string url, HttpContent content, string bearerToken);

    public Task<HttpResponseMessage> PostAsync(string url, string content, string bearerToken);

    public Task<HttpResponseMessage> DeleteAsync(string url, string bearerToken);
}
