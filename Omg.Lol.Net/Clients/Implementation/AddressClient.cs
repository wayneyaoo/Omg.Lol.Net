namespace Omg.Lol.Net.Clients.Implementation;

using System.Threading;
using System.Threading.Tasks;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Address;

public sealed class AddressClient : IAddressClient
{
    public string Token { get; internal set; } = string.Empty;

    public string Url { get; internal set; } = string.Empty;

    // TODO: url encode the address?
    private const string RetreiveAddressAvailabilityEndpoint = "/address/{0}/availability";

    private const string RetrieveAddressExpirationEndpoint = "/address/{0}/expiration";

    private const string RetrieveAddressInformationEndpoint = "/address/{0}/info";

    private readonly IApiServerCommunicationHandler apiServerCommunicationHandler;

    public AddressClient(IApiServerCommunicationHandler apiServerCommunicationHandler)
    {
        this.apiServerCommunicationHandler = apiServerCommunicationHandler;
    }

    public async Task<CommonResponse<AddressAvailability>> RetrieveAddressAvailabilityAsync(
        string address,
        CancellationToken cancellationToken = default)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<AddressAvailability>>(
                this.Url + string.Format(RetreiveAddressAvailabilityEndpoint, address), cancellationToken)
            .ConfigureAwait(false);

    public async Task<CommonResponse<AddressExpirationPublicView>> RetrieveAddressExpirationAsync(
        string address,
        CancellationToken cancellationToken = default)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<AddressExpirationPublicView>>(
                this.Url + string.Format(RetrieveAddressExpirationEndpoint, address), cancellationToken)
            .ConfigureAwait(false);

    public async Task<CommonResponse<PublicAddressInformation>> RetrievePublicAddressInformationAsync(
        string address,
        CancellationToken cancellationToken = default)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<PublicAddressInformation>>(
                this.Url + string.Format(RetrieveAddressInformationEndpoint, address), cancellationToken)
            .ConfigureAwait(false);

    public async Task<CommonResponse<PrivateAddressInformation>> RetrievePrivateAddressInformationAsync(
        string address,
        CancellationToken cancellationToken = default)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<PrivateAddressInformation>>(
                this.Url + string.Format(RetrieveAddressInformationEndpoint, address), this.Token, cancellationToken)
            .ConfigureAwait(false);
}
