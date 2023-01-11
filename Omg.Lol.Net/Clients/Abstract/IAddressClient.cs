namespace Omg.Lol.Net.Clients.Abstract;

using System.Threading.Tasks;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;

public interface IAddressClient : IApiInfoCarrier
{
    public Task<CommonResponse<AddressAvailability>> RetrieveAddressAvailability(string address);
}
