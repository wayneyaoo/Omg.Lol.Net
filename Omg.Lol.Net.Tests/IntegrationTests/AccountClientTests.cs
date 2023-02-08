namespace Omg.Lol.Net.Tests.IntegrationTests;

using System;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Clients.Implementation;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Infrastructure.Exceptions;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Account;

[TestFixture]
public class AccountClientTests
{
    private const string Email = "omg@wayneyao.me";

    private static string API_KEY;

    private IAccountClient accountClient;

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
        this.accountClient = new AccountClient(new ApiServerCommunicationHandler(mockFactory))
        {
            Url = Constants.API_SERVER_ADDRESS,
            Token = API_KEY,
        };
    }

    [TearDown]
    public void Teardown()
    {
        this.accountClient = null!;
    }

    [Test]
    public async Task Retrieve_AccountInformation_Should_Work()
    {
        var response = await this.accountClient.RetrieveAccountInformationAsync(Email);

        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Response.Email, Is.EqualTo(Email));
        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.Created.Iso8601Time, Is.Not.EqualTo(DateTimeOffset.MinValue));
        Assert.That(response.Response.Created.RelativeTime, Is.Not.Empty);
        Assert.That(response.Response.Created.Rfc2822Time, Is.Not.Empty);
        Assert.That(response.Response.Created.UnixEpochTime, Is.Not.EqualTo(long.MinValue));
        Assert.That(response.Response.Name, Is.Not.Empty);
        Assert.That(response.Response.Settings.Owner, Is.EqualTo(Email));
        Assert.That(
            response.Response.Settings.Communication,
            Is.Not.Empty);
        Assert.That(
            response.Response.Settings.DateFormat,
            Is.Not.Empty); // API should not return null. See if this gets fixed.
        Assert.That(
            response.Response.Settings.WebEditor,
            Is.Null); // API should not return null. See if this gets fixed.
    }

    [Test]
    public async Task Retrieve_AccountInformation_Cannot_Retrieve_Others_Info()
    {
        var exception = Assert.ThrowsAsync<ApiResponseException>(async () =>
            await this.accountClient.RetrieveAccountInformationAsync("wy"));

        Assert.That(exception.Success, Is.False);
        Assert.That(exception.StatusCode, Is.EqualTo(401));
        Assert.That(exception.Message, Is.Not.Empty);
    }

    [Test]
    public async Task Retrieve_AccountAddress_Should_Work()
    {
        CommonResponse<AccountAddress[]> response = await this.accountClient.RetrieveAccountAddressesAsync(Email);

        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Response.Length, Is.EqualTo(1));

        var address = response.Response[0];
        Assert.That(address.Address, Is.EqualTo("wy-test"));
        Assert.That(address.Expiration.Iso8601Time, Is.Not.EqualTo(DateTimeOffset.MinValue));
        Assert.That(address.Expiration.UnixEpochTime, Is.Not.EqualTo(long.MinValue));
        Assert.That(address.Expiration.Rfc2822Time, Is.Not.Empty);
        Assert.That(address.Expiration.RelativeTime, Is.Not.Empty);
        Assert.That(address.Expiration.Expired, Is.False);
        Assert.That(address.Expiration.WillExpire, Is.True);
        Assert.That(address.Registration.Message, Is.Not.Empty);
        Assert.That(address.Registration.RelativeTime, Is.Not.Empty);
        Assert.That(address.Registration.Iso8601Time, Is.Not.EqualTo(DateTimeOffset.MinValue));
        Assert.That(address.Registration.Rfc2822Time, Is.Not.Empty);
        Assert.That(address.Registration.UnixEpochTime, Is.Not.EqualTo(long.MinValue));
        Assert.That(address.Message, Is.Not.Empty);
    }

    [Test]
    public async Task Retrieve_AccontName_Should_Work()
    {
        CommonResponse<AccountName> response = await this.accountClient.RetrieveAccountNameAsync(Email);

        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.Name, Contains.Substring("Test"));
    }

    [Test]
    public async Task Retrieve_Account_Settings_Should_Work()
    {
        CommonResponse<AccountSettings> response = await this.accountClient.RetrieveAccountSettingsAsync(Email);

        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.Settings.Communication, Is.Not.Empty);
        Assert.That(
            response.Response.Settings.WebEditor,
            Is.Null); // This supposes to have default values. See if this will get fixed.
        Assert.That(response.Response.Settings.DateFormat, Is.Not.Empty);
        Assert.That(response.Response.Settings.Owner, Is.Not.Empty);
    }

    [Test]
    public async Task Retrieve_Account_Sessions_Should_Work()
    {
        CommonResponse<AccountSession[]> response = await this.accountClient.RetrieveAccountSessionsAsync(Email);

        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Request.StatusCode, Is.EqualTo(200));

        if (response.Response.Length == 0)
        {
            Assert.Inconclusive("Currently the account has no active session.");
        }

        var session = response.Response[0];
        Assert.That(session.SessionId, Is.Not.Empty);
        Assert.That(session.CreatedIp, Is.Not.Empty);
        Assert.That(session.UserAgent, Is.Not.Empty);
        Assert.That(session.CreatedOn, Is.GreaterThan(0));
        Assert.That(session.ExpiresOn, Is.GreaterThan(0));
    }
}
