namespace Omg.Lol.Net.Infrastructure;

using System.Threading.Tasks;

/// <summary>
/// An interface for caller to implement custom logic to fetch API key.
/// </summary>
/// <remarks>
/// SDK does not provide default implementation.
/// </remarks>
public interface IApiKeyProvider
{
    public Task<string> GetApiKeyAsync();
}
