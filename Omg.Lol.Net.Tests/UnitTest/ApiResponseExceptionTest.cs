namespace Omg.Lol.Net.Tests.UnitTest;

using System.Net;
using NUnit.Framework;
using Omg.Lol.Net.Infrastructure.Exceptions;

[TestFixture]
public class ApiResponseExceptionTest
{
    [Test]
    public void ApiResponseException_Should_Handle_Server_500()
    {
        var ex = new ApiResponseException(HttpStatusCode.InternalServerError);

        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.StatusCode, Is.EqualTo(500));
        Assert.That(ex.Message, Is.Not.Empty);
        Assert.That(ex.Success, Is.False);
        Assert.That(ex.ServerResponse, Is.Not.Null);
    }
}
