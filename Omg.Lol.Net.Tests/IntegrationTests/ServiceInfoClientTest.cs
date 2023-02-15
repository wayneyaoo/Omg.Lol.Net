namespace Omg.Lol.Net.Tests.IntegrationTests;

using System.Threading.Tasks;
using NUnit.Framework;
using Omg.Lol.Net.Clients.Implementation;
using Omg.Lol.Net.Infrastructure;

[TestFixture]
public class ServiceInfoClientTest
{
    [Test]
    public async Task Should_Get_Server_Statistics()
    {
        var serviceClient = new ServiceClient(new ApiServerCommunicationHandler(new HttpClientFactory()))
        {
            Url = Constants.API_SERVER_ADDRESS,
        };
        var response = await serviceClient.GetServiceStatisticsAsync();

        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Response.Addresses, Is.GreaterThan(0));
        Assert.That(response.Response.Members, Is.GreaterThan(0));
        Assert.That(response.Response.Profiles, Is.GreaterThan(0));
        Assert.That(response.Response.Message, Is.Not.Empty);
    }
}
