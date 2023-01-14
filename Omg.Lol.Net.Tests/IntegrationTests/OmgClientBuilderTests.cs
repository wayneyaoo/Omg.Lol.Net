namespace Omg.Lol.Net.Tests.IntegrationTests;

using System;
using NUnit.Framework;
using Omg.Lol.Net.Clients;

[TestFixture]
public class OmgClientBuilderTests
{
    private static string API_KEY;

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
    public void OmgClientBuilder_Should_Create_A_Function_OmgLolClient()
    {
        IOmgLolClient client = OmgLolClientBuilder.CreateDefault(API_KEY);

        Assert.That(client.ServiceClient, Is.Not.Null);
        Assert.That(client.AddressClient, Is.Not.Null);
    }
}
