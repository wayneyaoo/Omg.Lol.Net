namespace Omg.Lol.Net.Infrastructure;

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Omg.Lol.Net.Infrastructure.Exceptions;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Items;

internal class ApiServerCommunicationHandler : IApiServerCommunicationHandler
{
    private readonly Lazy<IHttpClient> httpClient;

    public ApiServerCommunicationHandler(IHttpClientFactory httpClientFactory)
    {
        this.httpClient = new Lazy<IHttpClient>(httpClientFactory.GetHttpClient);
    }

    public async Task<T> GetAsync<T>(string url, string bearerToken, CancellationToken cancellationToken = default)
        => await this.SendInternalAsync<T>(this.httpClient.Value.GetAsync, url, bearerToken, cancellationToken)
            .ConfigureAwait(false);

    public async Task<T> GetAsync<T>(string url, CancellationToken cancellationToken = default)
        => await this.SendInternalAsync<T>(this.httpClient.Value.GetAsync, url, cancellationToken)
            .ConfigureAwait(false);

    public async Task<T> PostAsync<T>(
        string url,
        string content,
        string bearerToken,
        CancellationToken cancellationToken = default)
        => await this
            .SendInternalAsync<T>(this.httpClient.Value.PostAsync, url, content, bearerToken, cancellationToken)
            .ConfigureAwait(false);

    public async Task<T> PatchAsync<T>(
        string url,
        string content,
        string bearerToken,
        CancellationToken cancellationToken = default)
        => await this
            .SendInternalAsync<T>(this.httpClient.Value.PatchAsync, url, content, bearerToken, cancellationToken)
            .ConfigureAwait(false);

    public async Task<T> PutAsync<T>(
        string url,
        string content,
        string bearerToken,
        CancellationToken cancellationToken = default)
        => await this.SendInternalAsync<T>(this.httpClient.Value.PutAsync, url, content, bearerToken, cancellationToken)
            .ConfigureAwait(false);

    public async Task<T> DeleteAsync<T>(string url, string bearerToken, CancellationToken cancellationToken = default)
        => await this.SendInternalAsync<T>(this.httpClient.Value.DeleteAsync, url, bearerToken, cancellationToken)
            .ConfigureAwait(false);

    private async Task<T> SendInternalAsync<T>(
        Func<string, CancellationToken, Task<HttpResponseMessage>> method,
        string url,
        CancellationToken cancellationToken)
        => await this.ResponseTransformation<T>(await method(url, cancellationToken)
            .ConfigureAwait(false)).ConfigureAwait(false);

    private async Task<T> SendInternalAsync<T>(
        Func<string, string, CancellationToken, Task<HttpResponseMessage>> method,
        string url,
        string bearer,
        CancellationToken cancellationToken)
        => await this.ResponseTransformation<T>(await method(url, bearer, cancellationToken)
                .ConfigureAwait(false))
            .ConfigureAwait(false);

    private async Task<T> SendInternalAsync<T>(
        Func<string, string, string, CancellationToken, Task<HttpResponseMessage>> method,
        string url,
        string content,
        string bearer,
        CancellationToken cancellationToken)
        => await this.ResponseTransformation<T>(await method(url, content, bearer, cancellationToken)
                .ConfigureAwait(false))
            .ConfigureAwait(false);

    private async Task<T> ResponseTransformation<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var serverResponse = JsonConvert.DeserializeObject<CommonResponse<MessageItem>>(content);
            if (serverResponse is null)
            {
                throw new ApiResponseException(response.StatusCode);
            }

            throw new ApiResponseException(serverResponse);
        }

        return JsonConvert.DeserializeObject<T>(content) !;
    }
}
