namespace Omg.Lol.Net.Infrastructure;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

public interface IHttpClient
{
    public Task<HttpResponseMessage> RequestAsync(
        HttpRequestMessage requestMessage,
        CancellationToken cancellationToken = default);

    public Task<HttpResponseMessage> GetAsync(string url, CancellationToken cancellationToken = default);

    public Task<HttpResponseMessage> GetAsync(
        string url,
        string bearerToken,
        CancellationToken cancellationToken = default);

    public Task<HttpResponseMessage> PostAsync(
        string url,
        string content,
        string bearerToken,
        CancellationToken cancellationToken = default);

    public Task<HttpResponseMessage> PatchAsync(
        string url,
        string content,
        string bearerToken,
        CancellationToken cancellationToken = default);

    public Task<HttpResponseMessage> PutAsync(
        string url,
        string content,
        string bearerToken,
        CancellationToken cancellationToken = default);

    public Task<HttpResponseMessage> DeleteAsync(
        string url,
        string bearerToken,
        CancellationToken cancellationToken = default);
}
