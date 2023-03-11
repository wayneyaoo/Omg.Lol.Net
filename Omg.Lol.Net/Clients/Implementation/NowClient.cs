namespace Omg.Lol.Net.Clients.Implementation;

using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Items;
using Omg.Lol.Net.Models.Now;

internal class NowClient : INowClient
{
    public string Token { get; internal set; } = string.Empty;

    public string Url { get; internal set; } = string.Empty;

    private const string RetrieveNowPageEndpoint = "/address/{0}/now";

    private const string UpdateNowPageEndpoint = RetrieveNowPageEndpoint;

    private const string RetrieveNowGardenEndpoint = "/now/garden";

    private readonly IApiServerCommunicationHandler apiServerCommunicationHandler;

    public NowClient(IApiServerCommunicationHandler apiServerCommunicationHandler)
    {
        this.apiServerCommunicationHandler = apiServerCommunicationHandler;
    }

    public async Task<CommonResponse<Now>> RetrieveNowPageAsync(
        string address,
        CancellationToken cancellationToken = default)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<Now>>(
                this.Url + string.Format(RetrieveNowPageEndpoint, address),
                cancellationToken)
            .ConfigureAwait(false);

    public async Task<CommonResponse<NowGarden>> RetrieveNowGardenPagesAsync(
        CancellationToken cancellationToken = default)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<NowGarden>>(
                this.Url + string.Format(RetrieveNowGardenEndpoint),
                cancellationToken)
            .ConfigureAwait(false);

    public async Task<CommonResponse<MessageItem>> UpdateNowPageAsync(
        string address,
        NowContentPost updatedNowContent,
        CancellationToken cancellationToken = default)
        => await this.apiServerCommunicationHandler.PostAsync<CommonResponse<MessageItem>>(
                this.Url + string.Format(UpdateNowPageEndpoint, address),
                JsonConvert.SerializeObject(updatedNowContent),
                this.Token,
                cancellationToken)
            .ConfigureAwait(false);
}
