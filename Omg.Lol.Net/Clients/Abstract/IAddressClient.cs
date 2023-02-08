namespace Omg.Lol.Net.Clients.Abstract;

using System.Threading;
using System.Threading.Tasks;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Address;

public interface IAddressClient : IApiInfoCarrier
{
    public Task<CommonResponse<AddressAvailability>> RetrieveAddressAvailabilityAsync(
        string address,
        CancellationToken cancellationToken = default);

    public Task<CommonResponse<AddressExpirationPublicView>> RetrieveAddressExpirationAsync(
        string address,
        CancellationToken cancellationToken = default);

    public Task<CommonResponse<PublicAddressInformation>> RetrievePublicAddressInformationAsync(
        string address,
        CancellationToken cancellationToken = default);

    public Task<CommonResponse<PrivateAddressInformation>> RetrievePrivateAddressInformationAsync(
        string address,
        CancellationToken cancellationToken = default);
}
