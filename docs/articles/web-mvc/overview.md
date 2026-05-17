---
uid: web-mvc-overview
title: ASP.NET Core MVC Extensions
---

# ASP.NET Core MVC Extensions

ASP.NET Core MVC provides `OkObjectResult`, `NotFoundObjectResult`, `BadRequestObjectResult`, and a few other status-specific result types. But there are gaps - there's no built-in way to return a 500 Internal Server Error or 403 Forbidden with a response body. And returning JSON with a custom status code requires more ceremony than it should.

`MADE.Web.Mvc` fills these gaps with additional action result types and controller extensions.

```bash
dotnet add package MADE.Web.Mvc
```

## What's included

| Type | What it does |
| --- | --- |
| `InternalServerErrorObjectResult` | Returns HTTP 500 with an error object or model state. |
| `ForbiddenObjectResult` | Returns HTTP 403 with an error object or model state. |
| `JsonResult` | Returns JSON with a configurable HTTP status code. |
| `ControllerBaseExtensions` | Helper methods: `Json()`, `InternalServerError()`, `Forbidden()`. |

## Quick example

```csharp
using MADE.Web.Mvc.Extensions;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        try
        {
            var order = await orderService.CreateAsync(request);
            return this.Json(order, HttpStatusCode.Created);
        }
        catch (InsufficientPermissionsException)
        {
            return this.Forbidden(new { message = "You don't have permission to create orders." });
        }
        catch (Exception ex)
        {
            return this.InternalServerError(new { message = "An unexpected error occurred.", detail = ex.Message });
        }
    }
}
```

## Action result types

### InternalServerErrorObjectResult

Returns an HTTP 500 response with a body. Two constructors are available:

```csharp
// With an error object
return new InternalServerErrorObjectResult(new { message = "Something went wrong" });

// With model state validation errors
return new InternalServerErrorObjectResult(ModelState);
```

### ForbiddenObjectResult

Returns an HTTP 403 response with a body:

```csharp
return new ForbiddenObjectResult(new { message = "Insufficient permissions" });
```

### JsonResult

Serializes a value as JSON using `System.Text.Json` with a configurable HTTP status code:

```csharp
return new MADE.Web.Mvc.Responses.JsonResult(myObject, HttpStatusCode.Created);

// With custom serialization options
return new MADE.Web.Mvc.Responses.JsonResult(myObject, HttpStatusCode.OK, new JsonSerializerOptions
{
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
});
```

## Controller extensions

The `ControllerBaseExtensions` class adds helper methods to any `ControllerBase`:

```csharp
// Return JSON with a status code
return this.Json(value, HttpStatusCode.Created);
return this.Json(value, HttpStatusCode.OK, jsonSerializerOptions);

// Return 500 with an error object
return this.InternalServerError(errorObject);
return this.InternalServerError(ModelState);

// Return 403 with an error object
return this.Forbidden(errorObject);
return this.Forbidden(ModelState);
```

These are convenience methods that create the corresponding result types. Use them instead of constructing the result objects directly for cleaner controller code.
