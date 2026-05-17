---
uid: web-api-versioning
title: API versioning
---

# API versioning

As your API evolves, breaking changes to existing endpoints require versioning to avoid disrupting clients. `MADE.Web` provides extensions for adding URL-based or header-based API versioning to your ASP.NET Core application.

## URL-based versioning

The most common approach. Clients specify the version in the URL path (e.g., `/api/v1/products`):

```csharp
builder.Services.AddApiVersionSupport(defaultMajor: 1, defaultMinor: 0);
```

## Header-based versioning

Clients specify the version in a request header. This keeps URLs clean but requires clients to set the header:

```csharp
builder.Services.AddApiVersionHeaderSupport(
    apiHeaderName: "x-api-version",
    defaultMajor: 1,
    defaultMinor: 0);
```

## Using in controllers

Decorate your controllers with version attributes:

```csharp
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        // v1 implementation
    }
}

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsV2Controller : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        // v2 implementation with breaking changes
    }
}
```

## Choosing a strategy

**URL-based versioning** is simpler for clients - the version is visible in the URL and easy to test in a browser. It's the most widely used approach.

**Header-based versioning** keeps URLs stable and is useful when you want clients to opt in to new versions explicitly. It's common in enterprise APIs where URL stability is important.

Both strategies support a default version, so clients that don't specify a version get the default behavior.
