﻿namespace Omg.Lol.Net.Tests.ApiIntegrationTests;

using System;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Clients.Implementation;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Infrastructure.Exceptions;
using Omg.Lol.Net.Models;

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
    public async Task RetrieveAddressAvailability_Address_Should_Not_Be_Available()
    {
        // Act
        CommonResponse<AddressAvailability> response = await this.addressClient.RetrieveAddressAvailability("adam");

        // Assert
        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Request.StatusCode, Is.EqualTo(200));

        Assert.That(response.Response.Address, Is.EqualTo("adam"));
        Assert.That(response.Response.Available, Is.False);
        Assert.That(response.Response.Availability, Is.EqualTo("unavailable"));
    }

    [Test]
    public async Task RetrieveAddressAvailability_Address_Should_Be_Available()
    {
        // Arrange
        var randomAddress = Guid.NewGuid().ToString();

        // Act
        CommonResponse<AddressAvailability> response =
            await this.addressClient.RetrieveAddressAvailability(randomAddress);

        // Assert
        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Request.StatusCode, Is.EqualTo(200));

        Assert.That(response.Response.Address, Is.EqualTo(randomAddress));
        Assert.That(response.Response.Available, Is.True);
        Assert.That(response.Response.Availability, Is.EqualTo("available"));
    }

    [Test]
    public async Task RetrieveAddressExpiration_Account_Not_Found()
    {
        // Arrange
        var randomAddress = Guid.NewGuid().ToString();

        // Act
        ApiResponseException exception =
            Assert.ThrowsAsync<ApiResponseException>(async () =>
                await this.addressClient.RetrieveAddressExpiration(randomAddress));

        // Assert
        Assert.That(exception.ServerResponse.Request.StatusCode, Is.EqualTo(404));
        Assert.That(exception.ServerResponse.Request.Success, Is.False);

        Assert.That(exception.ServerResponse.Response.Message, Is.Not.Empty);
    }

    [Test]
    public async Task RetrieveAddressExpiration_Account_Never_Expires()
    {
        CommonResponse<AddressExpiration> response = await this.addressClient.RetrieveAddressExpiration("adam");

        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Request.Success, Is.True);

        Assert.That(response.Response.Expired, Is.False);
        Assert.That(response.Response.WillExpire, Is.False);
    }

    [Test]
    public async Task RetrieveAddressExpiration_Account_Expirable_Not_Expired_Yet()
    {
        CommonResponse<AddressExpiration> response = await this.addressClient.RetrieveAddressExpiration("wy-test");

        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Request.Success, Is.True);

        Assert.That(response.Response.Expired, Is.False);
        Assert.That(response.Response.WillExpire, Is.True);
        Assert.That(response.Response.UnixEpochTime, Is.GreaterThan(0));
        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.Iso8601_Time, Is.GreaterThan(DateTimeOffset.Now));
        Assert.That(response.Response.Rfc2822_Time, Is.Not.Empty);
        Assert.That(response.Response.RelativeTime, Is.Not.Empty);
    }

    [Test]
    public async Task RetrieveAccountInformation_Public_Should_Retrieve_AddressInformation()
    {
        CommonResponse<AddressInformation> response = await this.addressClient.RetrieveAddressInformation("adam");

        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Request.Success, Is.True);

        Assert.That(response.Response.Owner, Is.Null);
        Assert.That(response.Response.Address, Is.EqualTo("adam"));
        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.Registration.Message, Is.Not.Empty);
        Assert.That(response.Response.Registration.RelativeTime, Is.Not.Empty);
        Assert.That(response.Response.Registration.Rfc2822_Time, Is.Not.Empty);
        Assert.That(response.Response.Registration.Iso8601_Time, Is.LessThan(DateTimeOffset.UtcNow));
        Assert.That(
            response.Response.Registration.UnixEpochTime,
            Is.LessThan(DateTimeOffset.UtcNow.ToUnixTimeSeconds()));
        Assert.That(response.Response.Expiration.Message, Is.Not.Empty);
        Assert.That(response.Response.Expiration.WillExpire, Is.False);
        Assert.That(response.Response.Expiration.Expired, Is.False);
        Assert.That(response.Response.Verification.Verified, Is.True);
    }

    [Test]
    public async Task RetrieveAccountInformation_Private_Should_Retrieve_AddressInformation()
    {
        CommonResponse<AddressInformation> response =
            await this.addressClient.RetrieveAddressInformation("wy-test", API_KEY);

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
        Assert.That(response.Response.Expiration.Message, Is.Not.Empty);
        Assert.That(response.Response.Expiration.WillExpire, Is.True);
        Assert.That(response.Response.Expiration.Expired, Is.False);
        Assert.That(response.Response.Verification.Verified, Is.False);
    }

    [Test]
    public async Task RetrieveAccountInformation_Private_Should_Not_Retrieve_AddressInformation_Of_Other_Accounts()
    {
        ApiResponseException exception = Assert.ThrowsAsync<ApiResponseException>(
            async () => await this.addressClient.RetrieveAddressInformation("adam", API_KEY));

        Assert.That(exception.ServerResponse.Request.StatusCode, Is.EqualTo(401));
        Assert.That(exception.ServerResponse.Request.Success, Is.False);

        Assert.That(exception.ServerResponse.Response.Message, Is.Not.Empty);
    }
}
