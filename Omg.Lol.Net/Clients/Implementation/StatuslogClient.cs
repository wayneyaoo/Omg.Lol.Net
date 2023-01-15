namespace Omg.Lol.Net.Clients.Implementation;

using System.Threading.Tasks;
using Newtonsoft.Json;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Items;

public class StatuslogClient : IStatuslogClient
{
    public string Token { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    private const string RetrieveIndividualStatusEndpoint = "/address/{0}/statuses/{1}";

    private const string RetrieveLatestStatusEndpoint = "/address/{0}/statuses/latest";

    private const string RetrieveEntireStatusesEndpoint = "/address/{0}/statuses";

    private const string DeleteIndividualStatusEndpoint = RetrieveIndividualStatusEndpoint;

    private const string CreateStatusEndpoint = RetrieveEntireStatusesEndpoint;

    private const string UpdateStatusEndpoint = RetrieveEntireStatusesEndpoint;

    private const string RetrieveEveryoneStatusEndpoint = "/statuslog";

    private const string RetrieveEveryoneLatestStatusEndpoint = "/statuslog/latest";

    private const string RetrieveStatusBioEndpoint = "/address/{0}/statuses/bio";

    private const string UpdateStatusBioEndpoint = RetrieveStatusBioEndpoint;

    private readonly IApiServerCommunicationHandler apiServerCommunicationHandler;

    public StatuslogClient(IApiServerCommunicationHandler apiServerCommunicationHandler)
    {
        this.apiServerCommunicationHandler = apiServerCommunicationHandler;
    }

    public async Task<CommonResponse<SingleStatus>> RetrieveInvidualStatusAsync(string address, string statusId)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<SingleStatus>>(
                this.Url + string.Format(RetrieveIndividualStatusEndpoint, address, statusId))
            .ConfigureAwait(false);

    public async Task<CommonResponse<MessageItem>> DeleteStatusAsync(string address, string statusId)
        => await this.apiServerCommunicationHandler.DeleteAsync<CommonResponse<MessageItem>>(
                this.Url + string.Format(DeleteIndividualStatusEndpoint, address, statusId),
                this.Token)
            .ConfigureAwait(false);

    public async Task<CommonResponse<SingleStatus>> RetrieveLatestStatusAsync(string address)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<SingleStatus>>(
                this.Url + string.Format(RetrieveLatestStatusEndpoint, address))
            .ConfigureAwait(false);

    public async Task<CommonResponse<MultipleStatuses>> RetrieveEntireStatusesAsync(string address)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<MultipleStatuses>>(
                this.Url + string.Format(RetrieveEntireStatusesEndpoint, address))
            .ConfigureAwait(false);

    public async Task<CommonResponse<MultipleStatuses>> RetrieveEntireStatusesAsync()
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<MultipleStatuses>>(
                this.Url + string.Format(RetrieveEveryoneStatusEndpoint))
            .ConfigureAwait(false);

    public async Task<CommonResponse<MultipleStatuses>> RetrieveEntireLatestStatusAsync()
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<MultipleStatuses>>(
                this.Url + string.Format(RetrieveEveryoneLatestStatusEndpoint))
            .ConfigureAwait(false);

    public async Task<CommonResponse<StatusModified>> CreateStatusAsync(string address, StatusPost status)
        => await this.apiServerCommunicationHandler.PostAsync<CommonResponse<StatusModified>>(
                this.Url + string.Format(CreateStatusEndpoint, address),
                JsonConvert.SerializeObject(status),
                this.Token)
            .ConfigureAwait(false);

    public async Task<CommonResponse<StatusModified>> UpdateStatusAsync(string address, StatusPatch status)
        => await this.apiServerCommunicationHandler.PostAsync<CommonResponse<StatusModified>>(
                this.Url + string.Format(UpdateStatusEndpoint, address),
                JsonConvert.SerializeObject(status),
                this.Token)
            .ConfigureAwait(false);

    public async Task<CommonResponse<StatusBio>> RetrieveStatusBioAsync(string address)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<StatusBio>>(
                this.Url + string.Format(RetrieveStatusBioEndpoint, address))
            .ConfigureAwait(false);

    public async Task<CommonResponse<MessageItem>> UpdateStatusBioAsync(string address, ContentItem content)
        => await this.apiServerCommunicationHandler.PostAsync<CommonResponse<MessageItem>>(
                this.Url + string.Format(UpdateStatusBioEndpoint, address),
                JsonConvert.SerializeObject(content),
                this.Token)
            .ConfigureAwait(false);
}
