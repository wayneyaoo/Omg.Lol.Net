namespace Omg.Lol.Net.Tests.IntegrationTests;

using System;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Clients.Implementation;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Infrastructure.Exceptions;
using Omg.Lol.Net.Models.Paste;

[TestFixture]
public class PastebinClientTests
{
    private static string API_KEY = null!;

    private IPastebinClient pastebinClient = null!;

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
        this.pastebinClient = new PastebinClient(new ApiServerCommunicationHandler(new TestMaterial.TestFactory()))
        {
            Url = Constants.API_SERVER_ADDRESS,
            Token = API_KEY,
        };
    }

    [TearDown]
    public void Teardown()
    {
        this.pastebinClient = null!;
    }

    [Test]
    public async Task RetrieveASpecificPaste_GetPublicPaste()
    {
        var response = await this.pastebinClient.RetrieveASpecificPasteAsync("wy-test", "get-me");

        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.PasteDetail.Content, Contains.Substring("[Integration Test]"));
        Assert.That(response.Response.PasteDetail.Title, Is.EqualTo("get-me"));
        // Assert.That(response.Response.PasteDetail.Listed, Is.True);
        Assert.That(
            response.Response.PasteDetail.ModifiedOn,
            Is.LessThanOrEqualTo(DateTimeOffset.UtcNow.ToUnixTimeSeconds()));
    }

    /// <summary>
    /// This is by design: https://github.com/neatnik/omg.lol/discussions/511.
    /// </summary>
    [Test]
    public async Task RetrieveASpecificPaste_GetPrivatePaste()
    {
        var response = await this.pastebinClient.RetrieveASpecificPasteAsync("wy-test", "get-me-unlisted");

        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.PasteDetail.Content, Contains.Substring("[Integration Test]"));
        Assert.That(response.Response.PasteDetail.Title, Is.EqualTo("get-me-unlisted"));
        Assert.That(
            response.Response.PasteDetail.ModifiedOn,
            Is.LessThanOrEqualTo(DateTimeOffset.UtcNow.ToUnixTimeSeconds()));
    }

    [Test]
    public void RetrieveASpecificPaste_Paste_Does_Not_Exist()
    {
        var random = Guid.NewGuid().ToString();
        var exception = Assert.ThrowsAsync<ApiResponseException>(async ()
            => await this.pastebinClient.RetrieveASpecificPasteAsync("wy-test", random));

        Assert.That(exception.ServerResponse.Request.Success, Is.False);
        Assert.That(exception.ServerResponse.Request.StatusCode, Is.EqualTo(404));
        Assert.That(exception.ServerResponse.Response.Message, Is.Not.Empty);
    }

    [Test]
    public async Task RetrieveEntirePastebin_Get_Public_Pastebin_Should_Work()
    {
        var response = await this.pastebinClient.RetrievePublicPastebinAsync("wy-test");

        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.Pastebin, Has.Length.GreaterThanOrEqualTo(2));
    }

    [Test]
    public async Task RetrieveEntirePastebin_Get_Private_Pastebin_Should_Work()
    {
        var response = await this.pastebinClient.RetrievePublicAndPrivatePastebinAsync("wy-test");

        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.Pastebin, Has.Length.GreaterThanOrEqualTo(2));
    }

    [Test]
    public async Task RetrieveEntirePastebin_Get_Pastebin_Should_Differentiate_Listed_Unlisted()
    {
        var publicResponse = await this.pastebinClient.RetrievePublicPastebinAsync("wy-test");
        var allResponse = await this.pastebinClient.RetrievePublicAndPrivatePastebinAsync("wy-test");

        Assert.That(publicResponse.Request.Success, Is.True);
        Assert.That(publicResponse.Request.StatusCode, Is.EqualTo(200));
        Assert.That(publicResponse.Response.Message, Is.Not.Empty);
        Assert.That(allResponse.Request.Success, Is.True);
        Assert.That(allResponse.Request.StatusCode, Is.EqualTo(200));
        Assert.That(allResponse.Response.Message, Is.Not.Empty);

        // Make sure there's at least one unlised paste or the test fails.
        Assert.That(publicResponse.Response.Pastebin.Length, Is.LessThan(allResponse.Response.Pastebin.Length));
    }

    // [Test]
    public async Task CreateOrUpdatePaste_Update_Should_Work() // Update test
    {
        // Arrange
        var currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

        // Act
        var response = await this.pastebinClient.CreateOrUpdatePasteAsync("wy-test", new PastePost()
        {
            Title = "update-me",
            Content = currentTimestamp,
            Listed = false,
        });

        var secondResponse = await this.pastebinClient.RetrieveASpecificPasteAsync("wy-test", "update-me");

        // Assert
        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.Title, Is.EqualTo("update-me"));

        Assert.That(secondResponse.Request.Success, Is.True);
        Assert.That(secondResponse.Request.StatusCode, Is.EqualTo(200));
        Assert.That(secondResponse.Response.Message, Is.Not.Empty);
        Assert.That(secondResponse.Response.PasteDetail.Content, Is.EqualTo(currentTimestamp));
        Assert.That(
            secondResponse.Response.PasteDetail.ModifiedOn,
            Is.GreaterThanOrEqualTo(DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                                    - 20)); // assume 20 seconds is enough, even in concurrent test runs.
    }

    [Test]
    public async Task Create_Then_Delete_Paste_Should_Work()
    {
        // Arrange
        var currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        var newPasteTitle = Guid.NewGuid().ToString();
        var content =
            $"The content is produced by Omg.Lol.Net integration test at utc {DateTime.UtcNow} ({currentTimestamp}). This should be deleted later. If not, test is not working correctly.";

        // Act
        var createResponse = await this.pastebinClient.CreateOrUpdatePasteAsync("wy-test", new PastePost()
        {
            Content = content,
            Listed = false,
            Title = newPasteTitle,
        });

        await Task.Delay(TimeSpan.FromSeconds(3));

        // Verify that the paste is indeed created.
        var firstGetResponse = await this.pastebinClient.RetrieveASpecificPasteAsync("wy-test", newPasteTitle);

        var deleteReponse = await this.pastebinClient.DeletePasteAsync("wy-test", newPasteTitle);

        await Task.Delay(TimeSpan.FromSeconds(3));

        // Assert
        // Verify that the paste is indeed deleted.
        // var exception = Assert.ThrowsAsync<ApiResponseException>(async () =>
        //     await this.pastebinClient.RetrieveASpecificPasteAsync("wy-test", newPasteTitle));
        try
        {
            _ = await this.pastebinClient.RetrieveASpecificPasteAsync("wy-test", newPasteTitle);
        }
        catch
        {
            Assert.Warn("API backend seems to get fixed and this test starts to throw. Come back to restore the test.");
        }

        Assert.That(createResponse.Request.StatusCode, Is.EqualTo(200));
        Assert.That(createResponse.Request.Success, Is.True);
        Assert.That(createResponse.Response.Title, Is.EqualTo(newPasteTitle));

        Assert.That(firstGetResponse.Request.StatusCode, Is.EqualTo(200));
        Assert.That(firstGetResponse.Request.Success, Is.True);
        Assert.That(firstGetResponse.Response.PasteDetail.Content, Is.EqualTo(content));
        Assert.That(
            firstGetResponse.Response.PasteDetail.ModifiedOn,
            Is.GreaterThanOrEqualTo(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - 10));
        Assert.That(firstGetResponse.Response.PasteDetail.Title, Is.EqualTo(newPasteTitle));

        Assert.That(deleteReponse.Request.StatusCode, Is.EqualTo(200));
        Assert.That(deleteReponse.Request.Success, Is.True);
        Assert.That(deleteReponse.Response.Message, Is.Not.Empty);

        // Assert.That(exception.StatusCode, Is.EqualTo(404));
        // Assert.That(exception.Success, Is.False);
        // Assert.That(exception.Message, Is.Not.Empty);
    }
}
