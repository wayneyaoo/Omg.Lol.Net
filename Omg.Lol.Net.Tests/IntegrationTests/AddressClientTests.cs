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
using Omg.Lol.Net.Models.Address;

[TestFixture]
public class AddressClientTests
{
    private static string API_KEY;

    private IAddressClient addressClient;

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
        this.addressClient = new AddressClient(new ApiServerCommunicationHandler(mockFactory))
        {
            Url = Constants.API_SERVER_ADDRESS,
            Token = API_KEY,
        };
    }

    [TearDown]
    public void Teardown()
    {
        this.addressClient = null!;
    }

    [Test]
    public async Task RetrieveAddressAvailability_Address_Is_Not_Available()
    {
        // Act
        CommonResponse<AddressAvailability>
            response = await this.addressClient.RetrieveAddressAvailabilityAsync("adam");

        // Assert
        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Request.StatusCode, Is.EqualTo(200));

        Assert.That(response.Response.Address, Is.EqualTo("adam"));
        Assert.That(response.Response.Available, Is.False);
        Assert.That(response.Response.Availability, Is.EqualTo("unavailable"));
        Assert.That(response.Response.PunyCode, Is.Empty);
        Assert.That(response.Response.SeeAlso.Length, Is.EqualTo(0));
    }

    [Test]
    public async Task RetrieveAddressAvailability_Address_Is_Available()
    {
        // Arrange
        var randomAddress = Guid.NewGuid().ToString();

        // Act
        CommonResponse<AddressAvailability> response =
            await this.addressClient.RetrieveAddressAvailabilityAsync(randomAddress);

        // Assert
        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Request.StatusCode, Is.EqualTo(200));

        Assert.That(response.Response.Message, Is.Not.Null);
        Assert.That(response.Response.Address, Is.EqualTo(randomAddress));
        Assert.That(response.Response.Available, Is.True);
        Assert.That(response.Response.Availability, Is.EqualTo("available"));
        Assert.That(response.Response.PunyCode, Is.Empty);
        Assert.That(response.Response.SeeAlso.Length, Is.EqualTo(0));
    }

    [Test]
    public async Task RetrieveAddressAvailability_Address_Is_Available_But_Will_Get_Encoded()
    {
        // Act
        CommonResponse<AddressAvailability> response =
            await this.addressClient.RetrieveAddressAvailabilityAsync("✔️");

        // Assert
        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Request.StatusCode, Is.EqualTo(200));

        Assert.That(response.Response.Message, Is.Not.Null);
        Assert.That(response.Response.Address, Is.EqualTo("✔️"));
        Assert.That(response.Response.Available, Is.True);
        Assert.That(response.Response.Availability, Is.EqualTo("available"));
        Assert.That(response.Response.PunyCode, Is.Not.Empty);
        Assert.That(response.Response.SeeAlso.Length, Is.GreaterThanOrEqualTo(2));
    }

    [Test]
    public async Task RetrieveAddressExpiration_Account_Not_Found()
    {
        // Arrange
        var randomAddress = Guid.NewGuid().ToString();

        // Act
        ApiResponseException exception =
            Assert.ThrowsAsync<ApiResponseException>(async () =>
                await this.addressClient.RetrieveAddressExpirationAsync(randomAddress));

        // Assert
        Assert.That(exception.ServerResponse.Request.StatusCode, Is.EqualTo(404));
        Assert.That(exception.ServerResponse.Request.Success, Is.False);

        Assert.That(exception.ServerResponse.Response.Message, Is.Not.Empty);
    }

    [Test]
    public async Task RetrieveAddressExpiration_Account_Not_Close_To_Expire()
    {
        CommonResponse<AddressExpirationPublicView> response =
            await this.addressClient.RetrieveAddressExpirationAsync("adam");

        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Request.Success, Is.True);

        Assert.That(response.Response.Expired, Is.False);
        Assert.That(response.Response.Message, Is.Not.Empty);
    }

    [Test]
    public async Task RetrieveAccountInformation_Public_Should_Retrieve_PublicAddressInformation()
    {
        CommonResponse<PublicAddressInformation> response =
            await this.addressClient.RetrievePublicAddressInformationAsync("adam");

        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Request.Success, Is.True);

        Assert.That(response.Response.Address, Is.EqualTo("adam"));
        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.Registration.Message, Is.Not.Empty);
        Assert.That(response.Response.Registration.RelativeTime, Is.Not.Empty);
        Assert.That(response.Response.Registration.Rfc2822_Time, Is.Not.Empty);
        Assert.That(response.Response.Registration.Iso8601_Time, Is.LessThan(DateTimeOffset.UtcNow));
        Assert.That(
            response.Response.Registration.UnixEpochTime,
            Is.LessThan(DateTimeOffset.UtcNow.ToUnixTimeSeconds()));
        Assert.That(response.Response.ExpirationPublicView.Message, Is.Not.Empty);
        Assert.That(response.Response.ExpirationPublicView.Expired, Is.False);
        Assert.That(response.Response.Verification.Verified, Is.True);
        Assert.That(response.Response.Verification.Message, Is.Not.Empty);
        Assert.That(response.Response.Keys, Is.Not.Empty);
    }

    [Test]
    public async Task RetrieveAccountInformation_Private_Should_Retrieve_PrivateAddressInformation()
    {
        CommonResponse<PrivateAddressInformation> response =
            await this.addressClient.RetrievePrivateAddressInformationAsync("wy-test");

        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Request.Success, Is.True);

        Assert.That(response.Response.Owner, Is.Not.Null);
        Assert.That(response.Response.Address, Is.EqualTo("wy-test"));
        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.Registration.Message, Is.Not.Empty);
        Assert.That(response.Response.Registration.RelativeTime, Is.Not.Empty);
        Assert.That(response.Response.Registration.Rfc2822_Time, Is.Not.Empty);
        Assert.That(response.Response.Registration.Iso8601_Time, Is.LessThan(DateTimeOffset.UtcNow));
        Assert.That(
            response.Response.Registration.UnixEpochTime,
            Is.LessThan(DateTimeOffset.UtcNow.ToUnixTimeSeconds()));
        Assert.That(response.Response.ExpirationPublicView.Message, Is.Not.Empty);
        Assert.That(response.Response.ExpirationPublicView.WillExpire, Is.True);
        Assert.That(response.Response.ExpirationPublicView.Expired, Is.False);
        Assert.That(response.Response.ExpirationPublicView.RelativeTime, Is.Not.Empty);
        Assert.That(response.Response.ExpirationPublicView.UnixEpochTime, Is.GreaterThan(0));
        Assert.That(response.Response.ExpirationPublicView.Iso8601Time, Is.GreaterThan(DateTimeOffset.MinValue));
        Assert.That(response.Response.ExpirationPublicView.Rfc2822Time, Is.Not.Empty);
        Assert.That(response.Response.Verification.Verified, Is.False);
        Assert.That(response.Response.Verification.Message, Is.Not.Empty);
        Assert.That(response.Response.Keys, Is.Empty);
    }

    [Test]
    public async Task RetrieveAccountInformation_Private_Should_Not_Retrieve_AddressInformation_Of_Other_Accounts()
    {
        ApiResponseException exception = Assert.ThrowsAsync<ApiResponseException>(
            async () => await this.addressClient.RetrievePrivateAddressInformationAsync("adam"));

        Assert.That(exception.ServerResponse.Request.StatusCode, Is.EqualTo(401));
        Assert.That(exception.ServerResponse.Request.Success, Is.False);

        Assert.That(exception.ServerResponse.Response.Message, Is.Not.Empty);
    }
}
