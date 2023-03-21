namespace Omg.Lol.Net.Infrastructure;

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

public class DefaultHttpClient : IHttpClient
{
#pragma warning disable SA1401
    internal static Lazy<HttpClient> HttpClient = null!;
#pragma warning restore SA1401

    private const string BearerAuth = "Bearer";

    public DefaultHttpClient()
    {
        HttpClient ??= new (() =>
        {
            var ret = new HttpClient();
            ret.DefaultRequestHeaders.UserAgent.Clear();
            ret.DefaultRequestHeaders.Add("User-Agent", "Omg.Lol.Net SDK Client");
            return ret;
        });
    }

    public DefaultHttpClient(HttpClient client)
    {
        HttpClient ??= new Lazy<HttpClient>(() => client);
    }

    public async Task<HttpResponseMessage> RequestAsync(
        HttpRequestMessage requestMessage,
        CancellationToken cancellationToken = default)
        => await HttpClient.Value.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);

    public async Task<HttpResponseMessage> GetAsync(string url, CancellationToken cancellationToken = default)
        => await this.SendInternalAsync(url, HttpMethod.Get, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

    public async Task<HttpResponseMessage> GetAsync(
        string url,
        string bearerToken,
        CancellationToken cancellationToken = default)
        => await this.SendInternalAsync(url, HttpMethod.Get, bearerToken, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

    public async Task<HttpResponseMessage> PostAsync(
        string url,
        string content,
        string bearerToken,
        CancellationToken cancellationToken = default)
        => await this.SendInternalAsync(
                url,
                HttpMethod.Post,
                bearerToken,
                new StringContent(content),
                cancellationToken)
            .ConfigureAwait(false);

    public async Task<HttpResponseMessage> PatchAsync(
        string url,
        string content,
        string bearerToken,
        CancellationToken cancellationToken = default)
        => await this.SendInternalAsync(
                url,
                new HttpMethod("PATCH"),
                bearerToken,
                new StringContent(content),
                cancellationToken)
            .ConfigureAwait(false);

    public async Task<HttpResponseMessage> PutAsync(
        string url,
        string content,
        string bearerToken,
        CancellationToken cancellationToken = default)
        => await this.SendInternalAsync(
                url,
                HttpMethod.Put,
                bearerToken,
                new StringContent(content),
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

    public async Task<HttpResponseMessage> DeleteAsync(
        string url,
        string bearerToken,
        CancellationToken cancellationToken = default)
        => await this.SendInternalAsync(url, HttpMethod.Delete, bearerToken, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

    private async Task<HttpResponseMessage> SendInternalAsync(
        string url,
        HttpMethod method,
        string? bearer = null,
        HttpContent? content = null,
        CancellationToken cancellationToken = default)
    {
        var message = new HttpRequestMessage()
        {
            Method = method,
            RequestUri = new Uri(url),
        };

        if (bearer is not null)
        {
            message.Headers.Authorization = new AuthenticationHeaderValue(BearerAuth, bearer);
        }

        message.Content = content;

        return await this.RequestAsync(message, cancellationToken).ConfigureAwait(false);
    }
}
