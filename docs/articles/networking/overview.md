---
uid: networking-overview
title: Networking
---

# Networking

Making HTTP requests in .NET is straightforward with `HttpClient`, but the patterns around it - managing client lifetimes, serializing request/response bodies, handling retries, uploading files - add up to a lot of repetitive code. And if you get `HttpClient` lifetime management wrong, you'll hit socket exhaustion in production.

`MADE.Networking` provides typed HTTP request handlers that wrap `HttpClient` with built-in JSON serialization, integrates with `IHttpClientFactory` for proper lifetime management, and includes retry handling and file upload support.

```bash
dotnet add package MADE.Networking
```

## What's included

| Guide | What it covers |
| --- | --- |
| [Getting started](getting-started.md) | `INetworkRequestFactory` registration, creating requests, and using named clients. |
| [Request types](request-types.md) | `JsonGetNetworkRequest`, `JsonPostNetworkRequest`, `JsonPutNetworkRequest`, `JsonPatchNetworkRequest`, `JsonDeleteNetworkRequest`, and `StreamGetNetworkRequest`. |
| [File uploads](file-uploads.md) | `MultipartFormDataPostNetworkRequest` for uploading files with form data. |
| [Retry handling](retry-handling.md) | `RetryDelegatingHandler` for automatic retry with exponential backoff. |
| [Request manager](request-manager.md) | `NetworkRequestManager` for queuing and managing concurrent requests with callbacks. |

## When to use this package

- You want typed HTTP requests that automatically serialize/deserialize JSON without manual `JsonSerializer` calls at every call site.
- You need `IHttpClientFactory` integration without the boilerplate of creating and configuring clients.
- You want built-in retry with exponential backoff for transient HTTP failures.
- You need file upload support with multipart form data.

> **Important:** Never create `HttpClient` instances manually with `new HttpClient()` in production code. This leads to socket exhaustion. Use `INetworkRequestFactory` or `IHttpClientFactory` to manage client lifetimes correctly.

## Quick example

```csharp
// Registration
services.AddNetworkRequestFactory();

// Usage
public class ProductService(INetworkRequestFactory requestFactory)
{
    public async Task<Product> GetProductAsync(int id)
    {
        var request = requestFactory.Get($"https://api.example.com/products/{id}");
        return await request.ExecuteAsync<Product>();
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        var request = requestFactory.Post(
            "https://api.example.com/products",
            JsonSerializer.Serialize(product));
        return await request.ExecuteAsync<Product>();
    }
}
```
