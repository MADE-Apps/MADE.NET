---
uid: web-overview
title: ASP.NET Core Helpers
---

# ASP.NET Core Helpers

Every ASP.NET Core API project needs the same supporting infrastructure: structured exception handling that returns meaningful error responses, pagination for list endpoints, claims-based user identity extraction, and API versioning. These aren't complex to implement, but they're tedious to set up from scratch on each project.

`MADE.Web` provides these as ready-to-use middleware, types, and extensions.

```bash
dotnet add package MADE.Web
```

## What's included

| Guide | What it covers |
| --- | --- |
| [Exception middleware](exception-middleware.md) | `HttpContextExceptionsMiddleware` for catching and formatting exception responses as JSON. |
| [Pagination](pagination.md) | `PaginatedRequest` and `PaginatedResponse` for standardized pagination across endpoints. |
| [Authentication](authentication.md) | `AuthenticatedUser` and `IAuthenticatedUserAccessor` for extracting claims-based identity. |
| [API versioning](api-versioning.md) | URL-based and header-based API versioning with `ApiVersioningExtensions`. |

## When to use this package

- You want consistent, structured error responses across your API without writing try/catch in every controller.
- You need a standard pagination pattern that works across all list endpoints.
- You want to extract authenticated user information from claims without manually parsing `ClaimsPrincipal` every time.
- You need to add API versioning to your application.

## Quick example

```csharp
// Program.cs

// Register exception handlers
builder.Services.AddHttpContextExceptionHandler<Exception, DefaultExceptionHandler>();
builder.Services.AddHttpContextExceptionHandler<NotFoundException, NotFoundExceptionHandler>();

// Register authentication accessor
builder.Services.AddAuthenticatedUserAccessor();

// Add API versioning
builder.Services.AddApiVersionSupport(defaultMajor: 1, defaultMinor: 0);

var app = builder.Build();

// Use exception handling middleware
app.UseHttpContextExceptionHandling();
```
