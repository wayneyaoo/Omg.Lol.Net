namespace Omg.Lol.Net.Tests;

using System;
using Omg.Lol.Net.Infrastructure;

public static class TestMaterial
{
    public static readonly Lazy<IHttpClient> HttpClient = new (() => new DefaultHttpClient());
}
