namespace Omg.Lol.Net.Tests.IntegrationTests;

using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;
using NUnit.Framework;
using Omg.Lol.Net.Infrastructure;

[TestFixture]
public class DefualtHttpClientTests
{
    [Test]
    public async Task DefaultHttpClient_Should_Work()
    {
        var httpClient = new DefaultHttpClient();
        var response = await httpClient.GetAsync("https://example.com");
        Assert.NotNull(response);
    }

    [Test]
    public async Task HttpClient_Should_Be_Able_To_Override()
    {
        var demo = new DemoClient();
        var response = await demo.GetAsync("https://example.com");
        Assert.NotNull(response);
    }

    private class DemoClient : DefaultHttpClient
    {
        protected override HttpClient GetHttpClient() => new ();
    }
}
