namespace Omg.Lol.Net.Clients.Implementation;

using System.Threading.Tasks;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;

public class ServiceClient : IServiceClient, ITokenBearer
{
    public string Token { get; set; }

    private readonly IApiServerCommunicationHandler apiServerCommunicationHandler;

    public ServiceClient(IApiServerCommunicationHandler apiServerCommunicationHandler)
    {
        this.apiServerCommunicationHandler = apiServerCommunicationHandler;
    }

    public async Task<CommonResponse<ServiceInfo>> GetServiceStatistics()
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<ServiceInfo>>();
}
