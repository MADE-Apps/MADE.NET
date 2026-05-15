---
uid: package-networking
title: Using the Networking package
---

# Using the Networking package

The Networking package contains a collection of helpers for applications that use `HttpClient` for making network requests to APIs.

## Making simple network requests using NetworkRequest instances

The Network package comes with a variety of `NetworkRequest` types that can be used to perform network requests without a lot of additional overhead.

The current available in-box `NetworkRequest` types are:

- JsonGetNetworkRequest, for making a HTTP GET request with a JSON response, deserializing to a specified type.
- JsonPostNetworkRequest, for making a HTTP POST request with a JSON payload, and a JSON response.
- JsonPutNetworkRequest, for making a HTTP PUT request with a JSON payload, and a JSON response.
- JsonPatchNetworkRequest, for making a HTTP PATCH request with a JSON payload, and a JSON response.
- JsonDeleteNetworkRequest, for making a HTTP DELETE request with a JSON response.
- StreamGetNetworkRequest, for making a HTTP GET request with a data stream response.

Each one needs a `HttpClient` instance, a URL to call, and any additional headers that may be required for the request.

The example below shows how you can use a `JsonGetNetworkRequest` to make a request to an API endpoint and retrieve data with a specified type.

```csharp
public async Task<Profile> GetMyProfileAsync(CancellationToken cancellationToken = default)
{
    JsonGetNetworkRequest request = new JsonGetNetworkRequest(
        new HttpClient(), 
        "https://jamescroft.co.uk/api/profile", 
        this.GetRequestHeaders());

    return await request.ExecuteAsync<Profile>(cancellationToken);
}
```

The `JsonPostNetworkRequest`, `JsonPutNetworkRequest`, and `JsonPatchNetworkRequest` types all have an additional parameter which require a JSON string.

```csharp
public async Task UpdateMyProfileAsync(Profile profile, CancellationToken cancellationToken = default)
{
    JsonPutNetworkRequest request = new JsonPutNetworkRequest(
        new HttpClient(), 
        "https://jamescroft.co.uk/api/profile", 
        JsonConvert.SerializeObject(profile),
        this.GetRequestHeaders());

    await request.ExecuteAsync<bool>(cancellationToken);
}
```

## Queuing your network requests using NetworkRequestManager

Built on the MADE `NetworkRequest` type, the `MADE.Networking.Http.NetworkRequestManager` is capable of managing a queue of multiple concurrent network requests.

This allows you to publish network requests and not need to worry about when you might receive a response. The implementation handles that for you with the use of success and error callback actions.

This can be achieved by registering your `NetworkRequest` instances with an instance of the `NetworkRequestManager`.

**Note**, to make sure network requests are processed, the `NetworkRequestManager.Start()` method must be called.

```csharp
private INetworkRequestManager NetworkManager { get; }

public void GetMyProfileAsync()
{
    JsonGetNetworkRequest request = new JsonGetNetworkRequest(
        new HttpClient(), 
        "https://jamescroft.co.uk/api/profile", 
        this.GetRequestHeaders());

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
    var request = new MultipartFormDataPostNetworkRequest(new HttpClient(), "https://api.example.com/upload")
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

```csharp
var handler = new RetryDelegatingHandler(maxRetries: 3, initialDelay: TimeSpan.FromSeconds(1));
var client = new HttpClient(handler);

// All requests made with this client will automatically retry on transient failures
var request = new JsonGetNetworkRequest(client, "https://api.example.com/data");
var result = await request.ExecuteAsync<MyData>();
```

The handler uses exponential backoff, doubling the delay between each retry attempt. You can customize the maximum number of retries and the initial delay via the constructor parameters.
