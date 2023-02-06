# Omg.Lol.Net

[![Codecov](https://img.shields.io/codecov/c/github/wayneyaoo/omg.lol.net)](https://codecov.io/github/wayneyaoo/Omg.Lol.Net)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/omg.lol.net?color=red)](https://www.nuget.org/packages/Omg.Lol.Net/)

[![GitHub](https://img.shields.io/github/license/wayneyaoo/omg.lol.net)](./LICENSE)
![Supported framework](https://img.shields.io/badge/supported%20framework-netstandard2.0%20%7C%20netstandard2.1%20%7C%20net6%20%7C%20net7-red)

An unofficial .NET SDK for the lovely [omg.lol](https://omg.lol) [API service](https://api.omg.lol/)! If you haven't checked it out, please do so : ) $20 a year brings so much fun!

> Disclaimer: \
> I'm not affliated with the service in any way. I'm one of the customers and a random dev.

> Warning: \
> The omg.lol API service is still [working in progress](https://api.omg.lol), hence this downstream library can only be, at most, as stable as the upstream API. Also expect breaking change until the version reaches 1.0.0.


- `net6`, `net7`, `netstandard2.0` and `netstandard2.1` support
- Clean interface design with testability in mind
- Lazy evaluation
- Fully asynchronous
- Extensibility in core components.

## Installation

```shell
dotnet add package Omg.Lol.Net
```

Or see [Omg.Lol.Net on nuget.org](https://www.nuget.org/packages/Omg.Lol.Net/).

## Usage

The main interface client code should interact with is `IOmgLolClient`. An instance of it can (only) be created via `OmgLolClientBuilder`. If you have specific needs (custom `HttpClient`, custom way to fetch your API key from different sources), see [extensivility section](#Extensibility) for more details.

```csharp
var client = OmgLolClient.Create("my-api-key");
```

With the client, you can dive into different sub-clients to consume the API.
```csharp
// service statistics.
var serviceStatistics = await client.ServiceClient.GetServiceStatistics();

// post a PURL
var response = await client.PurlsClient
                    .CreatePurlAsync(
                        "my-address",
                        new PurlPost()
                        {
                            name = "example",
                            url = "https://example.com",
                        });

// ...
```

> As the library is still in early development, not all functionalities are implemented (the service is also evolvig with new features). Stay tuned!

## Extensibility

### Custom `HttpClient`

If you need to inject your instance of `HttpClient`, you can achieve this via

- Create an implementation of interface `IHttpClient` to wrap your instance.
- Create an implementation of interface `IHttpClientFactory` to return above instance.
- When creating an `IOmgLolClient` with the builder, use another overload:
    ```csharp
    var client = OmgLolClientBuilder.Create("my-api-key", new MyHttpClientFactory());
    ```

As creating an instance with the builder is likely to happen at the early stage of an application initialization, "injecting" your factory implementation is a bit cumbersome so the library doesn't bother doing so. You might still want to `new` it there.

> Because of lazy evaluation, the `IHttpClient` is only created when making a real network call.

### Custom API key fetching from other sources

Your API key is sensitive for sure, and could be stored in different sources (environment variable, file, remote secret storage, another computer etc.) with special ways to fetch. To make things easier for you (and me), there're two overloads in the client builder:

```csharp
// A simple overload to get key via a callback
var client = await OmgLolClientBuilder.Create(
            () => Task.FromResult("use-whatever-logic-to-fetch-the-key"));

// Or if the logic is complicated enough, create an implementation of interface IApiKeyProvider
public class TestApiKeyProvider : IApiKeyProvider
{
    public Task<string> GetApiKeyAsync() => Task.FromResult("my-api-key");
}

// and use this overload
var client = await OmgLolClientBuilder.Create(new TestApiKeyProvider());
```
