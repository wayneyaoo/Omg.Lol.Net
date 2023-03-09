namespace Omg.Lol.Net.Clients.Implementation;

using System.Threading;
using System.Threading.Tasks;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Directory;

internal class DirectoryClient : IDirectoryClient
{
    public string Token { get; internal set; } = string.Empty;

    public string Url { get; internal set; } = string.Empty;

    private const string RetrieveDirectoryAddressEndpoint = "/directory";

    private readonly IApiServerCommunicationHandler apiServerCommunicationHandler;

    public DirectoryClient(IApiServerCommunicationHandler apiServerCommunicationHandler) =>
        this.apiServerCommunicationHandler = apiServerCommunicationHandler;

    public async Task<CommonResponse<AddressDirectory>> GetAddressDirectoryAsync(
        CancellationToken cancellationToken = default)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<AddressDirectory>>(
                this.Url + RetrieveDirectoryAddressEndpoint,
                cancellationToken)
            .ConfigureAwait(false);
}
