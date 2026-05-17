---
uid: networking-retry-handling
title: Retry handling
---

# Retry handling

Transient HTTP failures - timeouts, server errors, rate limiting - are inevitable in distributed systems. Rather than wrapping every HTTP call in retry logic, the `RetryDelegatingHandler` adds automatic retry with exponential backoff at the `HttpClient` level.

## Setting up retry handling

Register the handler with `IHttpClientFactory`:

```csharp
services.AddHttpClient("ResilientApi")
    .AddHttpMessageHandler(() => new RetryDelegatingHandler(
        maxRetries: 3,
        initialDelay: TimeSpan.FromSeconds(1)));
```

Or use it directly when constructing an `HttpClient`:

```csharp
var handler = new RetryDelegatingHandler(
    maxRetries: 3,
    initialDelay: TimeSpan.FromSeconds(1));
var client = new HttpClient(handler);
```

## How it works

The handler retries on the following conditions:

- **HTTP 500** (Internal Server Error)
- **HTTP 502** (Bad Gateway)
- **HTTP 503** (Service Unavailable)
- **HTTP 504** (Gateway Timeout)
- **HTTP 429** (Too Many Requests)
- **Request timeout exceptions**

Each retry doubles the delay (exponential backoff):
- Retry 1: 1 second
- Retry 2: 2 seconds
- Retry 3: 4 seconds

If all retries are exhausted, the last response or exception is returned to the caller.

## Using with INetworkRequestFactory

Combine the retry handler with a named client for the factory:

```csharp
services.AddHttpClient("MyApi", client =>
{
    client.BaseAddress = new Uri("https://api.example.com/");
})
.AddHttpMessageHandler(() => new RetryDelegatingHandler(maxRetries: 3));

services.AddNetworkRequestFactory();

// In your service
var request = requestFactory.WithClient("MyApi").Get("/data");
var result = await request.ExecuteAsync<MyData>();
// Retries automatically on transient failures
```

## Best practices

- **Set reasonable retry limits.** Three retries is a good default. More than five retries usually indicates a deeper issue.
- **Use exponential backoff** (which is the default) to avoid overwhelming a recovering service.
- **Only retry idempotent operations.** GET, PUT, and DELETE are generally safe to retry. Be cautious with POST requests that create resources, as retries could create duplicates if the server processed the request but the response was lost.
- **Consider using Polly** for more advanced resilience patterns (circuit breakers, bulkhead isolation, hedging). `RetryDelegatingHandler` is intentionally simple for straightforward retry scenarios.
