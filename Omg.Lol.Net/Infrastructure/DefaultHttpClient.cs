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

    public Task<HttpResponseMessage> GetAsync(string url)
        => this.SendInternalAsync(url, HttpMethod.Get);

    public Task<HttpResponseMessage> GetAsync(string url, string bearerToken)
        => this.SendInternalAsync(url, HttpMethod.Get, bearerToken);

    public Task<HttpResponseMessage> PostAsync(string url, string bearerToken, HttpContent content)
        => this.SendInternalAsync(url, HttpMethod.Post, bearerToken, content);

    public Task<HttpResponseMessage> PostAsync(string url, string bearerToken, string content)
        => this.SendInternalAsync(url, HttpMethod.Post, bearerToken, new StringContent(content));

    public Task<HttpResponseMessage> DeleteAsync(string url, string bearerToken)
        => this.SendInternalAsync(url, HttpMethod.Delete, bearerToken);

    private Task<HttpResponseMessage> SendInternalAsync(
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

        return this.RequestAsync(message);
    }
}
