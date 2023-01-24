namespace Omg.Lol.Net.Tests.IntegrationTests;

using System;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Clients.Implementation;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;

[TestFixture]
public class PurlsClientTests
{
    private static string API_KEY;

    private IPurlsClient purlsClient;

    [OneTimeSetUp]
    public void ApiKeyRetrieve()
    {
        var key = Environment.GetEnvironmentVariable(TestConstants.API_KEY_ENV_VARIABLE);
        if (key is null)
        {
            Assert.Inconclusive(
                $"Test API Key is not available. Make sure you have api key set in environment variable {TestConstants.API_KEY_ENV_VARIABLE}. Skip all tests.");
        }

        API_KEY = key;
    }

    [SetUp]
    public void Setup()
    {
        var mockFactory = Substitute.For<IHttpClientFactory>();
        mockFactory.GetHttpClient().Returns(TestMaterial.HttpClient.Value);
        this.purlsClient = new PurlsClient(new ApiServerCommunicationHandler(mockFactory))
        {
            Url = Constants.API_SERVER_ADDRESS,
            Token = API_KEY,
        };
    }

    [TearDown]
    public void Teardown()
    {
        this.purlsClient = null!;
    }

    [Test]
    public async Task RetrievePurl_Should_Work()
    {
        var response = await this.purlsClient.RetrievePurlAsync("wy-test", "dontdelete");

        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.Purl.Url, Is.EqualTo("https://google.com"));
        // Assert.That(response.Response.Purl.Counter, Is.GreaterThan(0));
        Assert.That(response.Response.Purl.Name, Is.EqualTo("dontdelete"));
    }

    [Test]
    public async Task RetrievePurls_Should_Work()
    {
        var response = await this.purlsClient.RetrievePurlsAsync("wy-test");

        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.Purls.Length, Is.GreaterThan(1));

        var purl = response.Response.Purls.Single(item => item.Name == "dontdelete");
        Assert.That(purl.Url, Is.EqualTo("https://google.com"));
        // Assert.That(purl.Counter, Is.GreaterThan(0));
        Assert.That(purl.Name, Is.EqualTo("dontdelete"));
    }

    [Test]
    public async Task Create_Delete_Should_Work()
    {
        var random = Guid.NewGuid().ToString();
        var url = "https://youtube.com";

        var createResponse = await this.purlsClient.CreatePurlAsync("wy-test", new PurlPost()
        {
            Name = random,
            Url = url,
        });

        await Task.Delay(TimeSpan.FromSeconds(2));

        var getResponse = await this.purlsClient.RetrievePurlAsync("wy-test", random);

        await Task.Delay(TimeSpan.FromSeconds(2));

        var deleteResponse = await this.purlsClient.DeletePurlAsync("wy-test", random);

        Assert.That(createResponse.Request.StatusCode, Is.EqualTo(200));
        Assert.That(createResponse.Request.Success, Is.True);
        Assert.That(createResponse.Response.Message, Is.Not.Empty);

        Assert.That(getResponse.Request.StatusCode, Is.EqualTo(200));
        Assert.That(getResponse.Request.Success, Is.True);
        Assert.That(getResponse.Response.Message, Is.Not.Empty);
        // Assert.That(getResponse.Response.Purl.Counter, Is.EqualTo(0));
        Assert.That(getResponse.Response.Purl.Name, Is.EqualTo(random));
        Assert.That(getResponse.Response.Purl.Url, Is.EqualTo(url));

        Assert.That(deleteResponse.Request.StatusCode, Is.EqualTo(200));
        Assert.That(deleteResponse.Request.Success, Is.True);
        Assert.That(deleteResponse.Response.Message, Is.Not.Empty);
    }
}
