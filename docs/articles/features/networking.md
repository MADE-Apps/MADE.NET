---
uid: package-networking
title: Using the Networking package
---

# Using the Networking package

The Networking package contains a collection of helpers for applications that use `HttpClient` for making network requests to APIs.

> **Important:** You should not create a new `HttpClient` for each request. Use the built-in `INetworkRequestFactory` or [`IHttpClientFactory`](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests) via dependency injection to manage `HttpClient` lifetimes.

## Getting started with INetworkRequestFactory

The `INetworkRequestFactory` is the recommended way to create network requests. It wraps `IHttpClientFactory` and provides a clean API for creating requests without manual `HttpClient` management.

### Registration

```csharp
// In your Startup or Program.cs
services.AddNetworkRequestFactory();
```

You can also register a named client with pre-configured settings:

```csharp
services.AddNetworkRequestFactory("MyApi", client =>
{
    client.BaseAddress = new Uri("https://api.example.com/");
});
```

### Usage

Inject `INetworkRequestFactory` into your services and create requests directly:

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
        var request = this.requestFactory.Get("https://api.example.com/profile");
        return await request.ExecuteAsync<Profile>(cancellationToken);
    }

    public async Task UpdateProfileAsync(Profile profile, CancellationToken cancellationToken = default)
    {
        var request = this.requestFactory.Put(
            "https://api.example.com/profile",
            JsonSerializer.Serialize(profile));
        await request.ExecuteAsync<bool>(cancellationToken);
    }
}
```

### Using named clients

If you have registered named `HttpClient` configurations, use `WithClient` to select one:

```csharp
var request = this.requestFactory.WithClient("MyApi").Get("/profile");
```

## Making simple network requests using NetworkRequest instances

The Network package comes with a variety of `NetworkRequest` types that can be used to perform network requests without a lot of additional overhead.

The current available in-box `NetworkRequest` types are:

- JsonGetNetworkRequest, for making a HTTP GET request with a JSON response, deserializing to a specified type.
- JsonPostNetworkRequest, for making a HTTP POST request with a JSON payload, and a JSON response.
- JsonPutNetworkRequest, for making a HTTP PUT request with a JSON payload, and a JSON response.
- JsonPatchNetworkRequest, for making a HTTP PATCH request with a JSON payload, and a JSON response.
- JsonDeleteNetworkRequest, for making a HTTP DELETE request with a JSON response.
- StreamGetNetworkRequest, for making a HTTP GET request with a data stream response.

## Using NetworkRequest types directly

If you prefer to create `NetworkRequest` instances directly (e.g., outside of a DI container), each type accepts an `HttpClient` instance, a URL, and optional headers.

```csharp
var client = this.httpClientFactory.CreateClient();
var request = new JsonGetNetworkRequest(client, "https://api.example.com/profile");
var profile = await request.ExecuteAsync<Profile>(cancellationToken);
```

## Queuing your network requests using NetworkRequestManager

Built on the MADE `NetworkRequest` type, the `MADE.Networking.Http.NetworkRequestManager` is capable of managing a queue of multiple concurrent network requests.

This allows you to publish network requests and not need to worry about when you might receive a response. The implementation handles that for you with the use of success and error callback actions.

This can be achieved by registering your `NetworkRequest` instances with an instance of the `NetworkRequestManager`.

**Note**, to make sure network requests are processed, the `NetworkRequestManager.Start()` method must be called.

```csharp
private INetworkRequestManager NetworkManager { get; }

private INetworkRequestFactory RequestFactory { get; }

public void GetMyProfileAsync()
{
    var request = this.RequestFactory.Get("https://api.example.com/profile");

    NetworkManager.AddOrUpdate<JsonGetNetworkRequest, Profile>(
        request, 
        this.UpdateProfileDetails);
}

public void UpdateProfileDetails(Profile profile)
{
    // Update UI elements
}
```

`NetworkRequest` objects have a `Guid` identifier also, so if you need to update a pending request with different data or a change in URL, you can do simply by recalling `NetworkManager.AddOrUpdate` passing in a network request with the same ID.

The `AddOrUpdate` method has overloads for providing a success callback, as well as an error callback. This allows you to make decisions in your code to handle a successful or failed network request.

## Uploading files with MultipartFormDataPostNetworkRequest

The `MultipartFormDataPostNetworkRequest` allows you to upload files and form data using multipart/form-data encoding. It provides a fluent API for building the request content.

```csharp
public async Task<UploadResult> UploadFileAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default)
{
    var request = this.requestFactory.PostMultipart("https://api.example.com/upload")
        .AddStreamContent("file", fileStream, fileName, "image/png")
        .AddStringContent("description", "Profile photo");

    return await request.ExecuteAsync<UploadResult>(cancellationToken);
}
```

You can add multiple types of content:

- `AddStringContent` - Adds a string form field.
- `AddStreamContent` - Adds a file stream with a file name and content type.
- `AddByteArrayContent` - Adds byte array content with a file name and content type.

## Adding retry support with RetryDelegatingHandler

The `RetryDelegatingHandler` is a `DelegatingHandler` that automatically retries failed HTTP requests with exponential backoff. It handles transient failures including timeouts, server errors (500, 502, 503, 504), and rate limiting (429).

Register it with `IHttpClientFactory` for use across your application:

```csharp
services.AddHttpClient("ResilientApi")
    .AddHttpMessageHandler(() => new RetryDelegatingHandler(maxRetries: 3, initialDelay: TimeSpan.FromSeconds(1)));
```

Or use it directly when constructing an `HttpClient`:

```csharp
var handler = new RetryDelegatingHandler(maxRetries: 3, initialDelay: TimeSpan.FromSeconds(1));
var client = new HttpClient(handler);

var request = new JsonGetNetworkRequest(client, "https://api.example.com/data");
var result = await request.ExecuteAsync<MyData>();
```

The handler uses exponential backoff, doubling the delay between each retry attempt. You can customize the maximum number of retries and the initial delay via the constructor parameters.
