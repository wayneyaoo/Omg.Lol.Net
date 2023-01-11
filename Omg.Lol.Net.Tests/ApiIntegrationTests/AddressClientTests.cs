namespace Omg.Lol.Net.Tests.ApiIntegrationTests;

using System;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Clients.Implementation;
using Omg.Lol.Net.Infrastructure;

[TestFixture]
public class AddressClientTests
{
    private static string API_KEY;

    private IAddressClient addressClient;

    [OneTimeSetUp]
    public void ApiKeyRetrieve()
    {
        var key = Environment.GetEnvironmentVariable(Constants.API_KEY_ENV_VARIABLE);
        if (key is null)
        {
            Assert.Inconclusive(
                $"Test API Key is not available. Make sure you have api key set in environment variable {Constants.API_KEY_ENV_VARIABLE}. Skip all tests.");
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
    public async Task Address_Should_Not_Be_Available()
    {
        // Act
        var response = await this.addressClient.RetrieveAddressAvailability("adam");

        // Assert
        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Response.Address, Is.EqualTo("adam"));
        Assert.That(response.Response.Available, Is.False);
        Assert.That(response.Response.Availability, Is.EqualTo("unavailable"));
    }

    [Test]
    public async Task Address_Should_Be_Available()
    {
        // Arrange
        var randomAddress = Guid.NewGuid().ToString();

        // Act
        var response = await this.addressClient.RetrieveAddressAvailability(randomAddress);

        // Assert
        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Response.Address, Is.EqualTo(randomAddress));
        Assert.That(response.Response.Available, Is.True);
        Assert.That(response.Response.Availability, Is.EqualTo("available"));
    }
}
