namespace Omg.Lol.Net.Infrastructure;

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Omg.Lol.Net.Infrastructure.Exceptions;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Items;

public class ApiServerCommunicationHandler : IApiServerCommunicationHandler
{
    private readonly Lazy<IHttpClient> httpClient;

    public ApiServerCommunicationHandler(IHttpClientFactory httpClientFactory)
    {
        this.httpClient = new Lazy<IHttpClient>(httpClientFactory.GetHttpClient);
    }

    public async Task<T> GetAsync<T>(string url, string bearerToken)
        => await this.SendInternalAsync<T>(this.httpClient.Value.GetAsync, url, bearerToken)
            .ConfigureAwait(false);

    public async Task<T> GetAsync<T>(string url)
        => await this.SendInternalAsync<T>(this.httpClient.Value.GetAsync, url)
            .ConfigureAwait(false);

    public async Task<T> PostAsync<T>(string url, string content, string bearerToken)
        => await this.SendInternalAsync<T>(this.httpClient.Value.PostAsync, url, content, bearerToken)
            .ConfigureAwait(false);

    public async Task<T> PatchAsync<T>(string url, string content, string bearerToken)
        => await this.SendInternalAsync<T>(this.httpClient.Value.PostAsync, url, content, bearerToken)
            .ConfigureAwait(false);

    public async Task<T> DeleteAsync<T>(string url, string bearerToken)
        => await this.SendInternalAsync<T>(this.httpClient.Value.DeleteAsync, url, bearerToken)
            .ConfigureAwait(false);

    private async Task<T> SendInternalAsync<T>(
        Func<string, Task<HttpResponseMessage>> method,
        string url)
        => await this.ResponseTransformation<T>(await method(url)
            .ConfigureAwait(false)).ConfigureAwait(false);

    private async Task<T> SendInternalAsync<T>(
        Func<string, string, Task<HttpResponseMessage>> method,
        string url,
        string bearer)
        => await this.ResponseTransformation<T>(await method(url, bearer)
                .ConfigureAwait(false))
            .ConfigureAwait(false);

    private async Task<T> SendInternalAsync<T>(
        Func<string, string, string, Task<HttpResponseMessage>> method,
        string url,
        string content,
        string bearer)
        => await this.ResponseTransformation<T>(await method(url, content, bearer)
                .ConfigureAwait(false))
            .ConfigureAwait(false);

    private async Task<T> ResponseTransformation<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApiResponseException(
                JsonConvert.DeserializeObject<CommonResponse<MessageItem>>(
                    await response.Content.ReadAsStringAsync().ConfigureAwait(false)));
        }

        return JsonConvert.DeserializeObject<T>(content) !;
    }
}
