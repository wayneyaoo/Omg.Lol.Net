namespace Omg.Lol.Net.Clients.Abstract;

using System.Threading.Tasks;
using Omg.Lol.Net.Models;

public interface IServiceClient
{
    public Task<CommonResponse<ServiceInfo>> GetServiceStatistics();
}
