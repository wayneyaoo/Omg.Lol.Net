namespace Omg.Lol.Net.Tests.UnitTest;

using System.Net.Http;
using NUnit.Framework;
using Omg.Lol.Net.Infrastructure;

[TestFixture]
public class HttpClientFactoryTest
{
    [SetUp]
    public void Setup()
    {
        DefaultHttpClient.HttpClient = null!;
    }

    [TearDown]
    public void CleanUp()
    {
        DefaultHttpClient.HttpClient = null!;
    }

    [Test]
    [Order(1)]
    public void HttpClientFactory_Should_Use_Default_HttpClient()
    {
        var factory = new HttpClientFactory();
        var httpClient = (DefaultHttpClient)factory.GetHttpClient();

        Assert.That(factory, Is.Not.Null);
        Assert.That(httpClient, Is.Not.Null);
        Assert.That(DefaultHttpClient.HttpClient.IsValueCreated, Is.False);
        Assert.That(DefaultHttpClient.HttpClient.Value, Is.Not.Null);
        Assert.That(DefaultHttpClient.HttpClient.IsValueCreated, Is.True);
    }

    [Test]
    [Order(2)]
    public void HttpClientFactory_Should_Use_Given_Instance_If_Given()
    {
        var ins = new HttpClient();

        var factory = new HttpClientFactory();
        var httpClient = factory.GetHttpClient(ins);

        Assert.That(factory, Is.Not.Null);
        Assert.That(httpClient, Is.Not.Null);
        Assert.That(DefaultHttpClient.HttpClient.IsValueCreated, Is.False);
        Assert.That(DefaultHttpClient.HttpClient.Value, Is.SameAs(ins));
        Assert.That(DefaultHttpClient.HttpClient.IsValueCreated, Is.True);
    }
}
