namespace Omg.Lol.Net.Clients.Abstract;

using System.Threading.Tasks;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Address;

public interface IAddressClient : IApiInfoCarrier
{
    public Task<CommonResponse<AddressAvailability>> RetrieveAddressAvailabilityAsync(string address);

    public Task<CommonResponse<AddressExpirationPublicView>> RetrieveAddressExpirationAsync(string address);

    public Task<CommonResponse<PublicAddressInformation>> RetrievePublicAddressInformationAsync(string address);

    public Task<CommonResponse<PrivateAddressInformation>> RetrievePrivateAddressInformationAsync(string address);
}
