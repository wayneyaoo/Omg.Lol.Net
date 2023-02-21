namespace Omg.Lol.Net.Clients.Abstract;

using System.Threading;
using System.Threading.Tasks;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Directory;

public interface IDirectoryClient : IApiInfoCarrier
{
    public Task<CommonResponse<AddressDirectory>> GetAddressDirectoryAsync(CancellationToken cancellationToken = default);
}
