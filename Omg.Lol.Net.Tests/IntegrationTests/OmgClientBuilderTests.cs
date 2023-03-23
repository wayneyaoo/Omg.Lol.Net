namespace Omg.Lol.Net.Tests.IntegrationTests;

using System;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Omg.Lol.Net.Clients;
using Omg.Lol.Net.Infrastructure;

[TestFixture]
public class OmgClientBuilderTests
{
    private static string API_KEY = null!;

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

    [Test]
    public void OmgClientBuilder_Should_Create_A_Function_OmgLolClient_With_Key()
    {
        IOmgLolClient client = OmgLolClientBuilder.Create(API_KEY);

        Assert.That(client.ServiceClient, Is.Not.Null);
        Assert.That(client.AddressClient, Is.Not.Null);
        Assert.That(client.DnsClient, Is.Not.Null);
        Assert.That(client.PastebinClient, Is.Not.Null);
        Assert.That(client.StatuslogClient, Is.Not.Null);
        Assert.That(client.PurlsClient, Is.Not.Null);
        Assert.That(client.AccountClient, Is.Not.Null);
        Assert.That(client.DirectoryClient, Is.Not.Null);
        Assert.That(client.NowClient, Is.Not.Null);
        Assert.That(client.WebClient, Is.Not.Null);
    }

    [Test]
    public async Task OmgClientBuilder_Should_Create_A_Function_OmgLolClient_With_Key_And_HttpClietFactory()
    {
        // Arrange
        var factory = Substitute.For<IHttpClientFactory>();
        factory.GetHttpClient().Returns(new TestMaterial.ProxyClient());

        // Act
        IOmgLolClient client = OmgLolClientBuilder.Create(API_KEY, factory);

        // Assert
        Assert.That(client.ServiceClient, Is.Not.Null);
        Assert.That(client.AddressClient, Is.Not.Null);
        Assert.That(client.DnsClient, Is.Not.Null);
        Assert.That(client.PastebinClient, Is.Not.Null);
        Assert.That(client.StatuslogClient, Is.Not.Null);
        Assert.That(client.PurlsClient, Is.Not.Null);
        Assert.That(client.AccountClient, Is.Not.Null);
        Assert.That(client.DirectoryClient, Is.Not.Null);

        // lazy initialization
        factory.DidNotReceive().GetHttpClient();

        await client.ServiceClient.GetServiceStatisticsAsync();

        factory.Received().GetHttpClient();
    }

    [Test]
    public async Task OmgClientBuilder_Should_Create_A_Function_OmgLolClient_With_Callback()
    {
        var token = Guid.NewGuid().ToString();

        // Act
        IOmgLolClient client = await OmgLolClientBuilder.Create(() => Task.FromResult(token));

        // Assert
        Assert.That(client.ServiceClient, Is.Not.Null);
        Assert.That(client.AddressClient, Is.Not.Null);
        Assert.That(client.DnsClient, Is.Not.Null);
        Assert.That(client.PastebinClient, Is.Not.Null);
        Assert.That(client.StatuslogClient, Is.Not.Null);
        Assert.That(client.PurlsClient, Is.Not.Null);
        Assert.That(client.AccountClient, Is.Not.Null);
        Assert.That(client.DirectoryClient, Is.Not.Null);

        Assert.That(client.Token, Is.EqualTo(token));
        Assert.That(client.Url, Is.Not.Null);
    }

    [Test]
    public async Task OmgClientBuilder_Should_Create_A_Function_OmgLolClient_With_Callback_And_HttpClientFactory()
    {
        var token = Guid.NewGuid().ToString();
        var factory = Substitute.For<IHttpClientFactory>();
        factory.GetHttpClient().Returns(new TestMaterial.ProxyClient());

        // Act
        IOmgLolClient client = await OmgLolClientBuilder.Create(() => Task.FromResult(token), factory);
        await client.ServiceClient.GetServiceStatisticsAsync();

        // Assert
        Assert.That(client.ServiceClient, Is.Not.Null);
        Assert.That(client.AddressClient, Is.Not.Null);
        Assert.That(client.DnsClient, Is.Not.Null);
        Assert.That(client.PastebinClient, Is.Not.Null);
        Assert.That(client.StatuslogClient, Is.Not.Null);
        Assert.That(client.PurlsClient, Is.Not.Null);
        Assert.That(client.AccountClient, Is.Not.Null);
        Assert.That(client.DirectoryClient, Is.Not.Null);

        Assert.That(client.Token, Is.EqualTo(token));
        Assert.That(client.Url, Is.Not.Null);

        factory.Received().GetHttpClient();
    }

    [Test]
    public async Task OmgClientBuilder_Should_Create_A_Function_OmgLolClient_With_ApiKeyProvider_And_HttpClientFactory()
    {
        var provider = new TestApiKeyProvider();
        var factory = Substitute.For<IHttpClientFactory>();
        factory.GetHttpClient().Returns(new TestMaterial.ProxyClient());

        // Act
        IOmgLolClient client = await OmgLolClientBuilder.Create(provider, factory);
        await client.ServiceClient.GetServiceStatisticsAsync();

        // Assert
        Assert.That(client.ServiceClient, Is.Not.Null);
        Assert.That(client.AddressClient, Is.Not.Null);
        Assert.That(client.DnsClient, Is.Not.Null);
        Assert.That(client.PastebinClient, Is.Not.Null);
        Assert.That(client.StatuslogClient, Is.Not.Null);
        Assert.That(client.PurlsClient, Is.Not.Null);
        Assert.That(client.AccountClient, Is.Not.Null);
        Assert.That(client.DirectoryClient, Is.Not.Null);

        Assert.That(client.Token, Is.EqualTo(provider.Token));
        Assert.That(client.Url, Is.Not.Null);

        factory.Received().GetHttpClient();
    }

    [Test]
    public async Task OmgClientBuilder_Should_Create_A_Function_OmgLolClient_With_ApiKeyProvider()
    {
        var provider = new TestApiKeyProvider();

        // Act
        IOmgLolClient client = await OmgLolClientBuilder.Create(provider);

        // Assert
        Assert.That(client.ServiceClient, Is.Not.Null);
        Assert.That(client.AddressClient, Is.Not.Null);
        Assert.That(client.DnsClient, Is.Not.Null);
        Assert.That(client.PastebinClient, Is.Not.Null);
        Assert.That(client.StatuslogClient, Is.Not.Null);
        Assert.That(client.PurlsClient, Is.Not.Null);
        Assert.That(client.AccountClient, Is.Not.Null);
        Assert.That(client.DirectoryClient, Is.Not.Null);

        Assert.That(client.Token, Is.EqualTo(provider.Token));
        Assert.That(client.Url, Is.Not.Null);
    }
}

internal class TestApiKeyProvider : IApiKeyProvider
{
#pragma warning disable SA1401
    public readonly string Token = Guid.NewGuid().ToString();
#pragma warning restore SA1401

    public Task<string> GetApiKeyAsync(CancellationToken cancellationToken = default) => Task.FromResult(this.Token);
}
