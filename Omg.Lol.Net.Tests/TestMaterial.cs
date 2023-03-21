namespace Omg.Lol.Net.Tests;

using System;
using System.Net;
using System.Net.Http;
using Omg.Lol.Net.Infrastructure;

public static class TestMaterial
{
    private static bool UseProxy = false;

    public static readonly Lazy<IHttpClient> HttpClient = new (()
        => UseProxy
            ? new DefaultHttpClient(new HttpClient(new HttpClientHandler()
            {
                Proxy = new WebProxy("http://127.0.0.1"),
            }))
            : new DefaultHttpClient(new HttpClient()));
}
