namespace Omg.Lol.Net.Tests.IntegrationTests;

using System;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Clients.Implementation;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;

[TestFixture]
public class DnsClientTests
{
    private static string API_KEY;

    private IDnsClient dnsClient;

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
        this.dnsClient = new DnsClient(new ApiServerCommunicationHandler(mockFactory))
        {
            Url = Constants.API_SERVER_ADDRESS,
            Token = API_KEY,
        };
    }

    [TearDown]
    public void Teardown()
    {
        this.dnsClient = null!;
    }

    [Test]
    public async Task RetrieveDnsRecords_Should_Work()
    {
        var response = await this.dnsClient.RetrieveDnsRecordsAsync("wy-test");

        Assert.That(response.Request.StatusCode, Is.EqualTo(200));
        Assert.That(response.Request.Success, Is.True);

        Assert.That(response.Response.Message, Is.Not.Empty);
        Assert.That(response.Response.Dns.Length, Is.GreaterThanOrEqualTo(1));
        DnsRecordDetail record = response.Response.Dns.Single(r => r.Data.Contains("donot delete"));
        Assert.That(record.Id, Is.Not.Empty);
        Assert.That(record.Type, Is.EqualTo("TXT"), "Make sure the type is TXT");
        Assert.That(record.Name, Is.Not.Empty);
        Assert.That(record.Ttl, Is.EqualTo(3600), "Make sure the TXT record TTL is 3600");
        Assert.That(record.CreatedAt, Is.LessThan(DateTimeOffset.UtcNow));
        Assert.That(record.UpdatedAt, Is.LessThan(DateTimeOffset.UtcNow));
        Assert.That(record.Priority, Is.Null);
    }

    [Test]
    public async Task Create_Update_Then_Delete_Dns_Should_Work()
    {
        var random1 = Guid.NewGuid().ToString();
        var random2 = Guid.NewGuid().ToString();
        var ttl = 3600;

        var createResponse = await this.dnsClient.CreateDnsRecordAsync("wy-test", new DnsRecordPost()
        {
            Data = $"[Integration Test] {random1}",
            Name = $"{random1}",
            Ttl = ttl,
            Type = DnsRecordType.TXT,
        });

        Assert.That(createResponse.Response.ResponseReceived.Data.Id, Is.Not.Empty, "Fail to create DNS record");

        var id = createResponse.Response.ResponseReceived.Data.Id;

        await Task.Delay(TimeSpan.FromSeconds(2));

        // Verify Create works.
        var firstGetResponse = await this.dnsClient.RetrieveDnsRecordsAsync("wy-test");

        var updateResponse = await this.dnsClient.UpdateDnsRecordAsync("wy-test", new DnsRecordUpdate()
        {
            Id = id,
            Data = $"[Integration Test] {random2}",
            Name = $"{random2}",
            Ttl = ttl,
            Type = DnsRecordType.TXT,
        });

        await Task.Delay(TimeSpan.FromSeconds(3));

        var secondGetResponse = await this.dnsClient.RetrieveDnsRecordsAsync("wy-test");

        var deleteResponse = await this.dnsClient.DeleteDnsRecordAsync("wy-test", id);

        Assert.That(createResponse.Request.StatusCode, Is.EqualTo(200));
        Assert.That(createResponse.Request.Success, Is.True);
        Assert.That(createResponse.Response.Message, Is.Not.Empty);
        Assert.That(createResponse.Response.DataSent.Type, Is.EqualTo(DnsRecordType.TXT));
        // Assert.That(createResponse.Response.DataSent.Data, Contains.Substring(random1));
        Assert.That(createResponse.Response.DataSent.Name, Contains.Substring(random1));
        Assert.That(createResponse.Response.DataSent.Ttl, Is.EqualTo(ttl));
        Assert.That(
            createResponse.Response.ResponseReceived.Data.CreatedAt,
            Is.GreaterThanOrEqualTo(DateTimeOffset.UtcNow.Subtract(TimeSpan.FromSeconds(10))));
        Assert.That(
            createResponse.Response.ResponseReceived.Data.UpdatedAt,
            Is.GreaterThanOrEqualTo(DateTimeOffset.UtcNow.Subtract(TimeSpan.FromSeconds(10))));
        Assert.That(createResponse.Response.ResponseReceived.Data.Id, Is.EqualTo(id));
        Assert.That(createResponse.Response.ResponseReceived.Data.Name, Contains.Substring(random1));
        Assert.That(createResponse.Response.ResponseReceived.Data.Type, Is.EqualTo("TXT"));
        // Assert.That(createResponse.Response.ResponseReceived.Data.Data, Contains.Substring(random1));
        Assert.That(createResponse.Response.ResponseReceived.Data.Ttl, Is.EqualTo(ttl));
        Assert.That(createResponse.Response.ResponseReceived.Data.Priority, Is.Null);

        Assert.That(firstGetResponse.Request.StatusCode, Is.EqualTo(200));
        Assert.That(firstGetResponse.Request.Success, Is.True);
        DnsRecordDetail record = firstGetResponse.Response.Dns.Single(r => r.Data.Contains(random1));
        Assert.That(record.Id, Is.EqualTo(id));
        Assert.That(record.Type, Is.EqualTo("TXT"));
        Assert.That(record.Name, Contains.Substring(random1));
        Assert.That(record.Ttl, Is.EqualTo(ttl));
        Assert.That(
            record.CreatedAt,
            Is.GreaterThanOrEqualTo(DateTimeOffset.UtcNow.Subtract(TimeSpan.FromSeconds(10))));
        Assert.That(
            record.UpdatedAt,
            Is.GreaterThanOrEqualTo(DateTimeOffset.UtcNow.Subtract(TimeSpan.FromSeconds(10))));
        Assert.That(record.Priority, Is.Null);

        Assert.That(updateResponse.Request.StatusCode, Is.EqualTo(200));
        Assert.That(updateResponse.Request.Success, Is.True);

        Assert.That(secondGetResponse.Request.StatusCode, Is.EqualTo(200));
        Assert.That(secondGetResponse.Request.Success, Is.True);
        record = secondGetResponse.Response.Dns.Single(r => r.Data.Contains(random2));
        Assert.That(record.Id, Is.EqualTo(id));
        Assert.That(record.Type, Is.EqualTo("TXT"));
        Assert.That(record.Name, Contains.Substring(random2));
        Assert.That(record.Ttl, Is.EqualTo(ttl));
        Assert.That(
            record.CreatedAt,
            Is.GreaterThanOrEqualTo(DateTimeOffset.UtcNow.Subtract(TimeSpan.FromSeconds(10))));
        Assert.That(
            record.UpdatedAt,
            Is.GreaterThanOrEqualTo(DateTimeOffset.UtcNow.Subtract(TimeSpan.FromSeconds(10))));
        Assert.That(record.Priority, Is.Null);

        Assert.That(deleteResponse.Request.StatusCode, Is.EqualTo(200));
        Assert.That(deleteResponse.Request.Success, Is.True);
        Assert.That(deleteResponse.Response.Message, Is.Not.Empty);
    }
}
