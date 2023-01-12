namespace Omg.Lol.Net.Infrastructure;

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class DefaultHttpClient : IHttpClient
{
    private const string BearerAuth = "Bearer";

    private static readonly Lazy<HttpClient> HttpClient = new (() => new HttpClient());

    public async Task<HttpResponseMessage> RequestAsync(HttpRequestMessage requestMessage)
        => await HttpClient.Value.SendAsync(requestMessage).ConfigureAwait(false);

    public async Task<HttpResponseMessage> GetAsync(string url)
        => await this.SendInternalAsync(url, HttpMethod.Get).ConfigureAwait(false);

    public async Task<HttpResponseMessage> GetAsync(string url, string bearerToken)
        => await this.SendInternalAsync(url, HttpMethod.Get, bearerToken).ConfigureAwait(false);

    public async Task<HttpResponseMessage> PostAsync(string url, string bearerToken, HttpContent content)
        => await this.SendInternalAsync(url, HttpMethod.Post, bearerToken, content).ConfigureAwait(false);

    public async Task<HttpResponseMessage> PostAsync(string url, string bearerToken, string content)
        => await this.SendInternalAsync(url, HttpMethod.Post, bearerToken, new StringContent(content))
            .ConfigureAwait(false);

    public async Task<HttpResponseMessage> DeleteAsync(string url, string bearerToken)
        => await this.SendInternalAsync(url, HttpMethod.Delete, bearerToken).ConfigureAwait(false);

    private async Task<HttpResponseMessage> SendInternalAsync(
        string url,
        HttpMethod method,
        string? bearer = null,
        HttpContent? content = null)
    {
        var message = new HttpRequestMessage()
        {
            Method = method,
            RequestUri = new Uri(url),
        };

        if (content is not null)
        {
            message.Content = content;
        }

        if (bearer is not null)
        {
            message.Headers.Authorization = new AuthenticationHeaderValue(BearerAuth, bearer);
        }

        return await this.RequestAsync(message);
    }
}
