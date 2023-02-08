namespace Omg.Lol.Net.Infrastructure;

using System.Threading;
using System.Threading.Tasks;

public interface IApiServerCommunicationHandler
{
    public Task<T> GetAsync<T>(
        string url,
        string bearerToken,
        CancellationToken cancellationToken = default);

    public Task<T> GetAsync<T>(
        string url,
        CancellationToken cancellationToken = default);

    public Task<T> PostAsync<T>(
        string url,
        string content,
        string bearerToken,
        CancellationToken cancellationToken = default);

    public Task<T> PatchAsync<T>(
        string url,
        string content,
        string bearerToken,
        CancellationToken cancellationToken = default);

    public Task<T> PutAsync<T>(
        string url,
        string content,
        string bearerToken,
        CancellationToken cancellationToken = default);

    public Task<T> DeleteAsync<T>(
        string url,
        string bearerToken,
        CancellationToken cancellationToken = default);
}
