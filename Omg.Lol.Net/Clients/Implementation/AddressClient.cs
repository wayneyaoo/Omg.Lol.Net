namespace Omg.Lol.Net.Clients.Implementation;

using System.Threading.Tasks;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;

public class AddressClient : IAddressClient
{
    public string Token { get; set; }

    public string Url { get; set; }

    // TODO: url encode the address?
    private static readonly string RetreiveAddressAvailabilityEndpoint = "/address/{0}/availability";

    private static readonly string RetrieveAddressExpirationEndpoint = "/address/{0}/expiration";

    private static readonly string RetrieveAddressInformationEndpoint = "/address/{0}/info";

    private readonly IApiServerCommunicationHandler apiServerCommunicationHandler;

    public AddressClient(IApiServerCommunicationHandler apiServerCommunicationHandler)
    {
        this.apiServerCommunicationHandler = apiServerCommunicationHandler;
    }

    public async Task<CommonResponse<AddressAvailability>> RetrieveAddressAvailability(string address)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<AddressAvailability>>(
            this.Url + string.Format(RetreiveAddressAvailabilityEndpoint, address));

    public async Task<CommonResponse<AddressExpiration>> RetrieveAddressExpiration(string address)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<AddressExpiration>>(
            this.Url + string.Format(RetrieveAddressExpirationEndpoint, address));

    public async Task<CommonResponse<AddressInformation>> RetrieveAddressInformation(string address)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<AddressInformation>>(
            this.Url + string.Format(RetrieveAddressInformationEndpoint, address));

    public async Task<CommonResponse<AddressInformation>> RetrieveAddressInformation(
        string address,
        string bearerToken)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<AddressInformation>>(
            this.Url + string.Format(RetrieveAddressInformationEndpoint, address), bearerToken);
}
