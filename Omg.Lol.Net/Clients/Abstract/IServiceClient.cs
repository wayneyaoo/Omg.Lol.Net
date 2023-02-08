namespace Omg.Lol.Net.Clients.Abstract;

using System.Threading;
using System.Threading.Tasks;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.ServiceStatus;

public interface IServiceClient : IApiInfoCarrier
{
    public Task<CommonResponse<ServiceInfo>> GetServiceStatistics(CancellationToken cancellationToken = default);
}
