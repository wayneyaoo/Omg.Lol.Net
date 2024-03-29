﻿namespace Omg.Lol.Net.Clients.Implementation;

using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Items;
using Omg.Lol.Net.Models.Purl;

internal class PurlsClient : IPurlsClient
{
    public string Token { get; internal set; } = string.Empty;

    public string Url { get; internal set; } = string.Empty;

    // TODO: revisit when this https://github.com/neatnik/omg.lol/issues/538 is resolved.
    private const string RetrieveSpecificPurlEndpoint = "/address/{0}/purl/{1}";

    private const string RetrieveAllPurlEndpoint = "/address/{0}/purls";

    private const string DeleteSpecificPurlEndpoint = RetrieveSpecificPurlEndpoint;

    // TODO: revisit when this https://github.com/neatnik/omg.lol/issues/538 is resolved.
    private const string CreateNewPurlEndpoint = "/address/{0}/purl";

    private readonly IApiServerCommunicationHandler apiServerCommunicationHandler;

    public PurlsClient(IApiServerCommunicationHandler apiServerCommunicationHandler)
    {
        this.apiServerCommunicationHandler = apiServerCommunicationHandler;
    }

    public async Task<CommonResponse<SinglePurl>> RetrievePurlAsync(
        string address,
        string purlName,
        CancellationToken cancellationToken = default)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<SinglePurl>>(
                this.Url + string.Format(RetrieveSpecificPurlEndpoint, address, purlName),
                this.Token,
                cancellationToken)
            .ConfigureAwait(false);

    public async Task<CommonResponse<MessageItem>> DeletePurlAsync(
        string address,
        string purlName,
        CancellationToken cancellationToken = default)
        => await this.apiServerCommunicationHandler.DeleteAsync<CommonResponse<MessageItem>>(
                this.Url + string.Format(DeleteSpecificPurlEndpoint, address, purlName),
                this.Token,
                cancellationToken)
            .ConfigureAwait(false);

    public async Task<CommonResponse<MultiplePurls>> RetrievePurlsAsync(
        string address,
        CancellationToken cancellationToken = default)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<MultiplePurls>>(
                this.Url + string.Format(RetrieveAllPurlEndpoint, address),
                this.Token,
                cancellationToken)
            .ConfigureAwait(false);

    public async Task<CommonResponse<MessageItem>> CreatePurlAsync(
        string address,
        PurlPost purl,
        CancellationToken cancellationToken = default)
        => await this.apiServerCommunicationHandler.PutAsync<CommonResponse<MessageItem>>(
                this.Url + string.Format(CreateNewPurlEndpoint, address),
                JsonConvert.SerializeObject(purl),
                this.Token,
                cancellationToken)
            .ConfigureAwait(false);
}
