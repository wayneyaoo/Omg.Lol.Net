namespace Omg.Lol.Net.Clients.Implementation;

using System.Threading.Tasks;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Address;

public class AddressClient : IAddressClient
{
    public string Token { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    // TODO: url encode the address?
    private const string RetreiveAddressAvailabilityEndpoint = "/address/{0}/availability";

    private const string RetrieveAddressExpirationEndpoint = "/address/{0}/expiration";

    private const string RetrieveAddressInformationEndpoint = "/address/{0}/info";

    private readonly IApiServerCommunicationHandler apiServerCommunicationHandler;

    public AddressClient(IApiServerCommunicationHandler apiServerCommunicationHandler)
    {
        this.apiServerCommunicationHandler = apiServerCommunicationHandler;
    }

    public async Task<CommonResponse<AddressAvailability>> RetrieveAddressAvailabilityAsync(string address)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<AddressAvailability>>(
            this.Url + string.Format(RetreiveAddressAvailabilityEndpoint, address))
            .ConfigureAwait(false);

    public async Task<CommonResponse<AddressExpiration>> RetrieveAddressExpirationAsync(string address)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<AddressExpiration>>(
            this.Url + string.Format(RetrieveAddressExpirationEndpoint, address))
            .ConfigureAwait(false);

    public async Task<CommonResponse<AddressInformation>> RetrieveAddressInformationAsync(string address)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<AddressInformation>>(
            this.Url + string.Format(RetrieveAddressInformationEndpoint, address))
            .ConfigureAwait(false);

    public async Task<CommonResponse<AddressInformation>> RetrievePrivateAddressInformationAsync(string address)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<AddressInformation>>(
            this.Url + string.Format(RetrieveAddressInformationEndpoint, address), this.Token)
            .ConfigureAwait(false);
}
