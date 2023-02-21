namespace Omg.Lol.Net.Tests.IntegrationTests;

using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Omg.Lol.Net.Clients.Implementation;
using Omg.Lol.Net.Infrastructure;

public class DirectoryClientTests
{
    [Test]
    public async Task GetAddressDirectory_Should_Work()
    {
        var mockFactory = Substitute.For<IHttpClientFactory>();
        mockFactory.GetHttpClient().Returns(TestMaterial.HttpClient.Value);
        var directoryClient = new DirectoryClient(new ApiServerCommunicationHandler(mockFactory))
        {
            Url = Constants.API_SERVER_ADDRESS,
            Token = string.Empty,
        };

        var response = await directoryClient.GetAddressDirectoryAsync();

        Assert.That(response.Request.Success, Is.True);
        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Response.Url, Is.Not.Empty);
        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.Directory.Length, Is.GreaterThan(100));
    }
}
