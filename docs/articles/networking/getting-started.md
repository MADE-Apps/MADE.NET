---
uid: networking-getting-started
title: Getting started with networking
---

# Getting started with networking

The `INetworkRequestFactory` is the recommended entry point for making HTTP requests with `MADE.Networking`. It wraps `IHttpClientFactory` and provides a clean API for creating typed requests without managing `HttpClient` instances yourself.

## Registration

Register the factory in your service collection:

```csharp
services.AddNetworkRequestFactory();
```

You can also register named clients with pre-configured settings:

```csharp
services.AddNetworkRequestFactory("ProductApi", client =>
{
    client.BaseAddress = new Uri("https://api.example.com/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
```

## Creating and executing requests

Inject `INetworkRequestFactory` into your services and use the HTTP method helpers to create requests:

```csharp
public class ProfileService
{
    private readonly INetworkRequestFactory requestFactory;

    public ProfileService(INetworkRequestFactory requestFactory)
    {
        this.requestFactory = requestFactory;
    }

    public async Task<Profile> GetProfileAsync(CancellationToken cancellationToken = default)
    {
        var request = requestFactory.Get("https://api.example.com/profile");
        return await request.ExecuteAsync<Profile>(cancellationToken);
    }

    public async Task UpdateProfileAsync(Profile profile, CancellationToken cancellationToken = default)
    {
        var request = requestFactory.Put(
            "https://api.example.com/profile",
            JsonSerializer.Serialize(profile));
        await request.ExecuteAsync<bool>(cancellationToken);
    }
}
```

The factory provides methods for all standard HTTP verbs: `Get`, `Post`, `Put`, `Patch`, `Delete`, and `PostMultipart`.

## Using named clients

If you registered a named client configuration, select it with `WithClient`:

```csharp
// Uses the "ProductApi" client configuration (base address, headers, etc.)
var request = requestFactory.WithClient("ProductApi").Get("/products");
var products = await request.ExecuteAsync<List<Product>>();
```

This is useful when you communicate with multiple APIs that have different base addresses, authentication headers, or timeout settings.

## Direct request construction

If you're not using dependency injection, you can create request instances directly with an `HttpClient`:

```csharp
var client = httpClientFactory.CreateClient();
var request = new JsonGetNetworkRequest(client, "https://api.example.com/data");
var result = await request.ExecuteAsync<MyData>();
```

See [Request types](request-types.md) for all available request types and their constructors.
