namespace Omg.Lol.Net.Clients.Implementation;

using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Items;
using Omg.Lol.Net.Models.Paste;

public sealed class PastebinClient : IPastebinClient
{
    public string Token { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    private const string RetrieveASpecificPasteEndpoint = "/address/{0}/pastebin/{1}";

    private const string RetrievePastebinEndpoint = "/address/{0}/pastebin";

    private const string CreateOrUpdatePasteEndpoint = RetrievePastebinEndpoint;

    private const string DeletePasteEndpoint = RetrieveASpecificPasteEndpoint;

    private readonly IApiServerCommunicationHandler apiServerCommunicationHandler;

    public PastebinClient(IApiServerCommunicationHandler apiServerCommunicationHandler)
    {
        this.apiServerCommunicationHandler = apiServerCommunicationHandler;
    }

    public async Task<CommonResponse<SinglePaste>> RetrieveASpecificPasteAsync(
        string address,
        string pasteTitle,
        CancellationToken cancellationToken = default)
        => await this.apiServerCommunicationHandler
            .GetAsync<CommonResponse<SinglePaste>>(
                this.Url + string.Format(RetrieveASpecificPasteEndpoint, address, pasteTitle), cancellationToken)
            .ConfigureAwait(false);

    public async Task<CommonResponse<MultiplePastes>> RetrievePublicAndPrivatePastebinAsync(
        string address,
        CancellationToken cancellationToken = default)
        => await this.apiServerCommunicationHandler
            .GetAsync<CommonResponse<MultiplePastes>>(
                this.Url + string.Format(RetrievePastebinEndpoint, address),
                this.Token,
                cancellationToken)
            .ConfigureAwait(false);

    public async Task<CommonResponse<MultiplePastes>> RetrievePublicPastebinAsync(
        string address,
        CancellationToken cancellationToken = default)
        => await this.apiServerCommunicationHandler
            .GetAsync<CommonResponse<MultiplePastes>>(
                this.Url + string.Format(RetrievePastebinEndpoint, address),
                cancellationToken)
            .ConfigureAwait(false);

    public async Task<CommonResponse<PasteModified>> CreateOrUpdatePasteAsync(
        string address,
        PastePost paste,
        CancellationToken cancellationToken = default)
        => await this.apiServerCommunicationHandler
            .PostAsync<CommonResponse<PasteModified>>(
                this.Url + string.Format(CreateOrUpdatePasteEndpoint, address),
                JsonConvert.SerializeObject(paste),
                this.Token,
                cancellationToken)
            .ConfigureAwait(false);

    public async Task<CommonResponse<MessageItem>> DeletePasteAsync(
        string address,
        string pasteTitle,
        CancellationToken cancellationToken = default)
        => await this.apiServerCommunicationHandler
            .DeleteAsync<CommonResponse<MessageItem>>(
                this.Url + string.Format(DeletePasteEndpoint, address, pasteTitle),
                this.Token,
                cancellationToken)
            .ConfigureAwait(false);
}
