namespace Omg.Lol.Net.Clients.Implementation;

using System.Threading.Tasks;
using Newtonsoft.Json;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Items;

public class PurlsClient : IPurlsClient
{
    public string Token { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    private const string RetrieveSpecificPurlEndpoint = "/address/{0}/purl/{1}";

    private const string RetrieveAllPurlEndpoint = "/address/{0}/purls";

    private const string DeleteSpecificPurlEndpoint = RetrieveSpecificPurlEndpoint;

    private const string CreateNewPurlEndpoint = "/address/{0}/purl";

    private readonly IApiServerCommunicationHandler apiServerCommunicationHandler;

    public PurlsClient(IApiServerCommunicationHandler apiServerCommunicationHandler)
    {
        this.apiServerCommunicationHandler = apiServerCommunicationHandler;
    }

    public async Task<CommonResponse<SinglePurl>> RetrievePurlAsync(string address, string purlName)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<SinglePurl>>(
                this.Url + string.Format(RetrieveSpecificPurlEndpoint, address, purlName),
                this.Token)
            .ConfigureAwait(false);

    public async Task<CommonResponse<MessageItem>> DeletePurlAsync(string address, string purlName)
        => await this.apiServerCommunicationHandler.DeleteAsync<CommonResponse<MessageItem>>(
                this.Url + string.Format(DeleteSpecificPurlEndpoint, address, purlName),
                this.Token)
            .ConfigureAwait(false);

    public async Task<CommonResponse<MultiplePurls>> RetrievePurlsAsync(string address)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<MultiplePurls>>(
                this.Url + string.Format(RetrieveAllPurlEndpoint, address),
                this.Token)
            .ConfigureAwait(false);

    public async Task<CommonResponse<MessageItem>> CreatePurlAsync(string address, PurlPost purl)
        => await this.apiServerCommunicationHandler.PostAsync<CommonResponse<MessageItem>>(
                this.Url + string.Format(CreateNewPurlEndpoint, address),
                JsonConvert.SerializeObject(purl),
                this.Token)
            .ConfigureAwait(false);
}
