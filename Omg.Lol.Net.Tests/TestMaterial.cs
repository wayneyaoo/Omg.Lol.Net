namespace Omg.Lol.Net.Tests;

using System.Net;
using System.Net.Http;
using Omg.Lol.Net.Infrastructure;

public static class TestMaterial
{
    internal class TestFactory : IHttpClientFactory
    {
        public IHttpClient GetHttpClient() => new ProxyClient();
    }

    internal class ProxyClient : DefaultHttpClient
    {
        private static bool UseProxy = false;

        protected override HttpClient GetHttpClient()
        {
            return UseProxy
                ? new HttpClient(new HttpClientHandler()
                {
                    Proxy = new WebProxy("http://127.0.0.1:7070"),
                })
                : new HttpClient();
        }
    }
}
