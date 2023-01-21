namespace Omg.Lol.Net.Tests.IntegrationTests;

using System;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Clients.Implementation;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Infrastructure.Exceptions;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Items;

[TestFixture]
public class StatuslogClientTests
{
    private static string API_KEY;

    private IStatuslogClient statuslogClient;

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
        this.statuslogClient = new StatuslogClient(new ApiServerCommunicationHandler(mockFactory))
        {
            Url = Constants.API_SERVER_ADDRESS,
            Token = API_KEY,
        };
    }

    [TearDown]
    public void Teardown()
    {
        this.statuslogClient = null!;
    }

    [Test]
    public async Task RetrieveIndividualStatus_Should_Work_For_Given_Address()
    {
        var response = await this.statuslogClient.RetrieveInvidualStatusAsync("wy-test", "63c3865865377");

        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.Status.Address, Is.EqualTo("wy-test"));
        Assert.That(response.Response.Status.Content, Contains.Substring("[Integration Test]"));
        Assert.That(response.Response.Status.Created, Is.EqualTo(1673758296));
        Assert.That(response.Response.Status.RelativeTime, Is.Not.Empty);
        Assert.That(response.Response.Status.Emoji, Is.Not.Empty);
    }

    [Test]
    public void RetrieveIndividualStatus_Should_Not_Work_For_Non_Exist_Address()
    {
        var random = Guid.NewGuid().ToString();
        var exception = Assert.ThrowsAsync<ApiResponseException>(async () =>
            await this.statuslogClient.RetrieveInvidualStatusAsync(random, "63c3865865377"));

        Assert.That(exception.StatusCode, Is.EqualTo(404));
        Assert.That(exception.Message, Is.Not.Empty);
        Assert.That(exception.Success, Is.False);
        Assert.That(exception.ServerResponse, Is.Not.Null);
    }

    [Test]
    public async Task RetrieveEntireStatuses_Should_Wokr_For_Given_Address()
    {
        var response = await this.statuslogClient.RetrieveEntireStatusesAsync("wy-test");

        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(
            response.Response.Statuses.Length,
            Is.GreaterThanOrEqualTo(2),
            "Make sure wy-test address has at least two statuses");

        foreach (var status in response.Response.Statuses)
        {
            Assert.That(status.Address, Is.EqualTo("wy-test"));
            Assert.That(status.Created, Is.GreaterThan(0));
            Assert.That(status.RelativeTime, Is.Not.Empty);
            Assert.That(status.Emoji, Is.Not.Empty);
            Assert.That(status.Id, Is.Not.Empty);
            Assert.That(status.Content, Is.Not.Empty);
        }
    }

    [Test]
    public async Task RetrieveLatestStatus_Should_Wokr_For_Given_Address()
    {
        var response = await this.statuslogClient.RetrieveLatestStatusAsync("wy-test");

        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.Status.Address, Is.EqualTo("wy-test"));
        Assert.That(response.Response.Status.RelativeTime, Is.Not.Empty);
        Assert.That(response.Response.Status.Content, Is.Not.Empty);
        Assert.That(response.Response.Status.Created, Is.GreaterThan(0));
        Assert.That(response.Response.Status.RelativeTime, Is.Not.Empty);
        Assert.That(response.Response.Status.Emoji, Is.Not.Empty);
    }

    [Test]
    public async Task RetrieveEntireStatus_Should_Wokr_For_Everyone()
    {
        var response = await this.statuslogClient.RetrieveEntireStatusesAsync();

        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.Statuses.Length, Is.GreaterThanOrEqualTo(100));

        foreach (var status in response.Response.Statuses.Take(30))
        {
            Assert.That(status.Address, Is.Not.Empty);
            Assert.That(status.Created, Is.GreaterThan(0));
            Assert.That(status.RelativeTime, Is.Not.Empty);
            Assert.That(status.Emoji, Is.Not.Empty);
            Assert.That(status.Id, Is.Not.Empty);
            Assert.That(status.Content, Is.Not.Empty);
        }
    }

    [Test]
    public async Task RetrieveEntireLatestStatus_Should_Wokr_For_Everyone()
    {
        var response = await this.statuslogClient.RetrieveEntireLatestStatusAsync();

        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.Statuses.Length, Is.GreaterThanOrEqualTo(100));

        foreach (var status in response.Response.Statuses.Take(30))
        {
            Assert.That(status.Address, Is.Not.Empty);
            Assert.That(status.Created, Is.GreaterThan(0));
            Assert.That(status.RelativeTime, Is.Not.Empty);
            Assert.That(status.Emoji, Is.Not.Empty);
            Assert.That(status.Id, Is.Not.Empty);
            Assert.That(status.Content, Is.Not.Empty);
        }
    }

    [Test]
    public async Task RetrieveStatusBio_Should_Work()
    {
        CommonResponse<StatusBio> response = await this.statuslogClient.RetrieveStatusBioAsync("wy-test");

        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.Bio, Is.Not.Empty);
        Assert.That(response.Response.Css, Is.Empty);
    }

    [Test]
    public async Task Create_Update_Then_Delete_Status_Should_Work()
    {
        // Arrange
        var randomToken = Guid.NewGuid().ToString();
        var secondRandomToken = Guid.NewGuid().ToString();
        var content =
            $"Greetings from a bot 😀. Here's a random token: {randomToken}. If you still see this status after a few refreshes (within 20s), please contact @wy";
        var emoji = "😉";

        // Act
        CommonResponse<StatusModified> createResponse = await this.statuslogClient.CreateStatusAsync(
            "wy-test",
            new StatusPost()
            {
                Content = content,
                Emoji = emoji,
            });

        await Task.Delay(TimeSpan.FromSeconds(2));

        var statusId = createResponse.Response.Id;

        // Verify create works.
        CommonResponse<SingleStatus> secondGetResponse =
            await this.statuslogClient.RetrieveInvidualStatusAsync("wy-test", statusId);

        CommonResponse<StatusModified> updateResponse = await this.statuslogClient.UpdateStatusAsync(
            "wy-test",
            new StatusPatch()
            {
                Content = content.Replace(randomToken, secondRandomToken),
                Emoji = emoji,
                Id = statusId,
            });

        await Task.Delay(TimeSpan.FromSeconds(2));

        // Verify update works.
        var thirdResponse = await this.statuslogClient.RetrieveInvidualStatusAsync("wy-test", statusId);

        CommonResponse<MessageItem> deleteResponse = await this.statuslogClient.DeleteStatusAsync("wy-test", statusId);

        await Task.Delay(2);

        var exception = Assert.ThrowsAsync<ApiResponseException>(async () =>
            await this.statuslogClient.RetrieveInvidualStatusAsync("wy-test", statusId));

        // Assert
        Assert.That(createResponse.Request.StatusCode, Is.EqualTo(200));
        Assert.That(createResponse.Request.Success, Is.True);
        Assert.That(createResponse.Response.Id, Is.Not.Empty);
        Assert.That(createResponse.Response.Message, Is.Not.Empty);
        Assert.That(createResponse.Response.Url, Is.Not.Empty);

        // What the new status should look like
        Assert.That(secondGetResponse.Request.StatusCode, Is.EqualTo(200));
        Assert.That(secondGetResponse.Request.Success, Is.True);
        Assert.That(secondGetResponse.Response.Message, Is.Not.Empty);
        Assert.That(secondGetResponse.Response.Status.Address, Is.EqualTo("wy-test"));
        Assert.That(secondGetResponse.Response.Status.RelativeTime, Is.Not.Empty);
        Assert.That(secondGetResponse.Response.Status.Content, Contains.Substring(randomToken));
        Assert.That(
            secondGetResponse.Response.Status.Created,
            Is.GreaterThanOrEqualTo(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - 10));
        Assert.That(secondGetResponse.Response.Status.Emoji, Is.EqualTo(emoji));
        Assert.That(secondGetResponse.Response.Status.Id, Is.Not.Empty);

        // what the API service says about the update action
        Assert.That(updateResponse.Request.StatusCode, Is.EqualTo(200));
        Assert.That(updateResponse.Request.Success, Is.True);
        Assert.That(updateResponse.Response.Message, Is.Not.Empty);
        Assert.That(updateResponse.Response.Id, Is.EqualTo(statusId));
        Assert.That(updateResponse.Response.Url, Is.Not.Empty);

        // what the updated status should look like
        Assert.That(thirdResponse.Request.StatusCode, Is.EqualTo(200));
        Assert.That(thirdResponse.Request.Success, Is.True);
        Assert.That(thirdResponse.Response.Message, Is.Not.Empty);
        Assert.That(thirdResponse.Response.Status.Address, Is.EqualTo("wy-test"));
        Assert.That(thirdResponse.Response.Status.RelativeTime, Is.Not.Empty);
        Assert.That(thirdResponse.Response.Status.Content, Contains.Substring(secondRandomToken));
        Assert.That(
            thirdResponse.Response.Status.Created,
            Is.GreaterThanOrEqualTo(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - 10));
        Assert.That(thirdResponse.Response.Status.Emoji, Is.EqualTo(emoji));
        Assert.That(thirdResponse.Response.Status.Id, Is.Not.Empty);

        // what the API service says about the delation operation
        Assert.That(deleteResponse.Request.StatusCode, Is.EqualTo(200));
        Assert.That(deleteResponse.Request.Success, Is.True);
        Assert.That(deleteResponse.Response.Message, Is.Not.Empty);

        // the status should be gone
        Assert.That(exception.StatusCode, Is.EqualTo(404));
        Assert.That(exception.Success, Is.False);
        Assert.That(exception.Message, Is.Not.Empty);
    }

    [Test]
    public async Task UpdateStatusBio_Should_Work()
    {
        // Arrange
        var response = await this.statuslogClient.RetrieveStatusBioAsync("wy-test");
        var currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        if (!response.Request.Success)
        {
            Assert.Inconclusive($"Cannot retrieve status bio before updating: {response.Response.Message}");
        }

        if (!long.TryParse(response.Response.Bio.Substring(response.Response.Bio.Length - 10), out var lastNumber))
        {
            Assert.Inconclusive("wy-test's status bio last 10 chars is not a number. Make sure it's a number");
        }

        // Act
        var updateResponse = await this.statuslogClient.UpdateStatusBioAsync("wy-test", new ContentItem()
        {
            Content = response.Response.Bio.Replace(lastNumber.ToString(), currentTimestamp.ToString()),
        });

        var secondResponse = await this.statuslogClient.RetrieveStatusBioAsync("wy-test");

        Assert.That(updateResponse.Request.StatusCode, Is.EqualTo(200));
        Assert.That(updateResponse.Request.Success, Is.True);
        Assert.That(updateResponse.Response.Message, Is.Not.Empty);

        Assert.That(secondResponse.Request.StatusCode, Is.EqualTo(200));
        Assert.That(secondResponse.Request.Success, Is.True);
        Assert.That(secondResponse.Response.Message, Is.Not.Empty);
        Assert.That(secondResponse.Response.Bio, Is.Not.Empty); // because of concurrent tests, the unix seconds might not be accurate (concurrent update). We only check for not empty here.
    }
}
