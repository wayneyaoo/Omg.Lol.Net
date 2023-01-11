namespace Omg.Lol.Net.Clients.Implementation;

using System.Threading.Tasks;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;

public class AddressClient : IAddressClient
{
    public string Token { get; set; }

    public string Url { get; set; }

    private readonly IApiServerCommunicationHandler apiServerCommunicationHandler;

    // TODO: url encode the address?
    private string RetreiveAddressAvailabilityEndpoint = "/address/{0}/availability";

    public AddressClient(IApiServerCommunicationHandler apiServerCommunicationHandler)
    {
        this.apiServerCommunicationHandler = apiServerCommunicationHandler;
    }

    public async Task<CommonResponse<AddressAvailability>> RetrieveAddressAvailability(string address)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<AddressAvailability>>(
            this.Url + string.Format(this.RetreiveAddressAvailabilityEndpoint, address));
}
