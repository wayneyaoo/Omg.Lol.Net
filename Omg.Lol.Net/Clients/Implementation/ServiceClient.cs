namespace Omg.Lol.Net.Clients.Implementation;

using System.Threading;
using System.Threading.Tasks;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.ServiceStatus;

public sealed class ServiceClient : IServiceClient
{
    public string Token { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    private const string RetrieveServiceInformation = "/service/info";

    private readonly IApiServerCommunicationHandler apiServerCommunicationHandler;

    public ServiceClient(IApiServerCommunicationHandler apiServerCommunicationHandler)
    {
        this.apiServerCommunicationHandler = apiServerCommunicationHandler;
    }

    public async Task<CommonResponse<ServiceInfo>> GetServiceStatisticsAsync(CancellationToken cancellationToken = default)
        => await this.apiServerCommunicationHandler
            .GetAsync<CommonResponse<ServiceInfo>>(this.Url + RetrieveServiceInformation, cancellationToken)
            .ConfigureAwait(false);
}
