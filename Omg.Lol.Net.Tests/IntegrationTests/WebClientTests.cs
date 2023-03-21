namespace Omg.Lol.Net.Tests.IntegrationTests;

using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Clients.Implementation;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models.Web;

[TestFixture]
public class WebClientTests
{
    private static string API_KEY = null!;

    private IWebClient webClient = null!;

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
        this.webClient = new WebClient(new ApiServerCommunicationHandler(mockFactory))
        {
            Url = Constants.API_SERVER_ADDRESS,
            Token = API_KEY,
        };
    }

    [Test]
    public async Task Retrieve_Web_Content_Should_Work()
    {
        var response = await this.webClient.RetrieveWebPageContentAsync("wy-test");

        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Request.Success, Is.True);

        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.Branding, Is.Not.Empty);
        Assert.That(response.Response.Css, Is.Not.Empty);
        Assert.That(response.Response.Head, Is.Not.Empty);
        Assert.That(response.Response.Metadata, Is.Not.Empty);
        Assert.That(response.Response.ProfilePicture, Is.Not.Empty);
        Assert.That(response.Response.Verified, Is.Null);
        Assert.That(response.Response.Type, Is.EqualTo("profile"));
        Assert.That(response.Response.Theme, Is.EqualTo("default"));
        Assert.That(response.Response.Content, Is.Not.Empty);
    }

    [Test]
    public async Task UpdateWebContent_Should_Work()
    {
        var response = await this.webClient.RetrieveWebPageContentAsync("wy-test");

        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Response.Content, Is.Not.Empty);

        var content = response.Response.Content;
        var newContent = Regex.Replace(content, "\\d{10}", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString());

        var updateResponse = await this.webClient.UpdateWebPageContentAsync(
            "wy-test",
            new WebPageUpdate()
            {
                Content = newContent,
                Publish = true,
            });

        Assert.That(updateResponse.Request.StatusCode, Is.EqualTo(200));
        Assert.That(updateResponse.Request.Success, Is.True);
        Assert.That(updateResponse.Response.Message, Is.Not.Empty);

        await Task.Delay(TimeSpan.FromSeconds(2));

        response = await this.webClient.RetrieveWebPageContentAsync("wy-test");

        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Response.Content, Is.Not.EqualTo(content));
    }
}
