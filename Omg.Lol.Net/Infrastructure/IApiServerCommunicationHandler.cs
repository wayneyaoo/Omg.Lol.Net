namespace Omg.Lol.Net.Infrastructure;

using System.Threading.Tasks;

public interface IApiServerCommunicationHandler
{
    public Task<T> GetAsync<T>(string url, string bearerToken);

    public Task<T> PostAsync<T>(string url, string bearerToken, string content);

    public Task<T> DeleteAsync<T>(string url, string bearerToken);
}
