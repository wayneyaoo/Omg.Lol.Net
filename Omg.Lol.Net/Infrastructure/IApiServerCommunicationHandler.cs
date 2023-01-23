namespace Omg.Lol.Net.Infrastructure;

using System.Threading.Tasks;

public interface IApiServerCommunicationHandler
{
    public Task<T> GetAsync<T>(string url, string bearerToken);

    public Task<T> GetAsync<T>(string url);

    public Task<T> PostAsync<T>(string url, string content, string bearerToken);

    public Task<T> PatchAsync<T>(string url, string content, string bearerToken);

    public Task<T> PutAsync<T>(string url, string content, string bearerToken);

    public Task<T> DeleteAsync<T>(string url, string bearerToken);
}
