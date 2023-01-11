namespace Omg.Lol.Net.Tests.ApiIntegrationTests;

using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Omg.Lol.Net.Clients.Implementation;
using Omg.Lol.Net.Infrastructure;

[TestFixture]
public class ServiceStatisticsTest
{
    // private static string API_KEY;
    //
    // [OneTimeSetUp]
    // public void ApiKeyRetrieve()
    // {
    //     var key = Environment.GetEnvironmentVariable(Constants.API_KEY_ENV_VARIABLE);
    //     if (key is null)
    //     {
    //         Assert.Inconclusive(
    //             $"Test API Key is not available. Make sure you have api key set in environment variable {Constants.API_KEY_ENV_VARIABLE}. Skip all tests.");
    //     }
    //
    //     API_KEY = key;
    // }
    [Test]
    public async Task Should_Get_Server_Statistics()
    {
        var serviceClient = new ServiceClient(new ApiServerCommunicationHandler(new HttpClientFactory()))
        {
            Url = Constants.API_SERVER_ADDRESS,
        };
        var response = await serviceClient.GetServiceStatistics();

        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Response.Addresses, Is.GreaterThan(0));
        Assert.That(response.Response.Members, Is.GreaterThan(0));
        Assert.That(response.Response.Profiles, Is.GreaterThan(0));
        Assert.That(response.Response.Message, Is.Not.Empty);
    }
}
