namespace Omg.Lol.Net.Tests.IntegrationTests;

using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Clients.Implementation;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models.Now;

[TestFixture]
public class NowClientTests
{
    private static string API_KEY = null!;

    private INowClient nowClient = null!;

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
        this.nowClient = new NowClient(new ApiServerCommunicationHandler(new TestMaterial.TestFactory()))
        {
            Url = Constants.API_SERVER_ADDRESS,
            Token = API_KEY,
        };
    }

    [TearDown]
    public void Teardown()
    {
        this.nowClient = null!;
    }

    [Test]
    public async Task ListNowGarden_Should_Work()
    {
        var response = await this.nowClient.RetrieveNowGardenPagesAsync();

        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Request.Success, Is.True);

        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.Gardens, Is.Not.Empty);
        Assert.That(response.Response.Gardens.Length, Is.GreaterThan(10)); // Hope this is always true

        var gardenItem = response.Response.Gardens[response.Response.Gardens.Length - 1];

        Assert.That(gardenItem.Url, Is.Not.Empty);
        Assert.That(gardenItem.Address, Is.Not.Empty);
        Assert.That(gardenItem.Updated.UnixEpochTime, Is.LessThan(DateTimeOffset.UtcNow.ToUnixTimeSeconds()));
        Assert.That(gardenItem.Updated.Iso8601Time, Is.LessThan(DateTimeOffset.UtcNow));
        Assert.That(gardenItem.Updated.RelativeTime, Is.Not.Empty);
        Assert.That(gardenItem.Updated.Rfc2822Time, Is.Not.Empty);
    }

    [Test]
    public async Task Retrieve_NowPage_Should_Work()
    {
        var response = await this.nowClient.RetrieveNowPageAsync("adam");

        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Request.Success, Is.True);

        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.NowContent, Is.Not.Null);
        Assert.That(response.Response.NowContent.Updated, Is.LessThan(DateTimeOffset.UtcNow.ToUnixTimeSeconds()));
        Assert.That(response.Response.NowContent.Content, Is.Not.Empty);
        // Assert.That(response.Response.NowContent.Listed, Is.Not.Empty);
    }

    [Test]
    public async Task Update_NowPage_Should_Work()
    {
        var response = await this.nowClient.RetrieveNowPageAsync("wy-test");
        if (!response.Request.Success)
        {
            Assert.Inconclusive("Cannot retrieve now page to update. No update operation.");
        }

        var content = response.Response.NowContent.Content;

        var updateResponse = await this.nowClient.UpdateNowPageAsync("wy-test", new NowContentPost()
        {
            Content = Regex.Replace(content, "\\d{10}", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
            Listed = "1",
        });

        Assert.That(updateResponse.Request.StatusCode, Is.EqualTo(200));
        Assert.That(updateResponse.Request.Success, Is.True);
        Assert.That(updateResponse.Response.Message, Is.Not.Empty);

        var response2 = await this.nowClient.RetrieveNowPageAsync("wy-test");
        Assert.That(response2.Response.NowContent.Content, Is.Not.EqualTo(response.Response.NowContent.Content));
    }
}
