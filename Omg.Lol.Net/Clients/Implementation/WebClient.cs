namespace Omg.Lol.Net.Clients.Implementation;

using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Items;
using Omg.Lol.Net.Models.Web;

internal class WebClient : IWebClient
{
    public string Token { get; internal set; } = string.Empty;

    public string Url { get; internal set; } = string.Empty;

    private const string RetrieveWebPageContentEndpoint = "/address/{0}/web";

    private const string UpdateWebPageContentEndpoint = RetrieveWebPageContentEndpoint;

    private readonly IApiServerCommunicationHandler apiServerCommunicationHandler;

    public WebClient(IApiServerCommunicationHandler apiServerCommunicationHandler)
    {
        this.apiServerCommunicationHandler = apiServerCommunicationHandler;
    }

    public async Task<CommonResponse<WebPageContent>> RetrieveWebPageContentAsync(
        string address,
        CancellationToken cancellationToken = default)
        => await this.apiServerCommunicationHandler
            .GetAsync<CommonResponse<WebPageContent>>(
                string.Format(this.Url + RetrieveWebPageContentEndpoint, address),
                this.Token,
                cancellationToken)
            .ConfigureAwait(false);

    public async Task<CommonResponse<MessageItem>> UpdateWebPageContentAsync(
        string address,
        WebPageUpdate webPageUpdate,
        CancellationToken cancellationToken = default)
        => await this.apiServerCommunicationHandler
            .PostAsync<CommonResponse<MessageItem>>(
                string.Format(this.Url + RetrieveWebPageContentEndpoint, address),
                JsonConvert.SerializeObject(webPageUpdate),
                this.Token,
                cancellationToken)
            .ConfigureAwait(false);
}
