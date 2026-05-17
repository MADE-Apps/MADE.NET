---
uid: web-exception-middleware
title: Exception middleware
---

# Exception middleware

The default ASP.NET Core error handling returns HTML error pages or minimal problem details. For APIs, you typically want structured JSON error responses with meaningful error codes and messages. The `HttpContextExceptionsMiddleware` provides this by catching exceptions and routing them to type-specific handlers.

## Setting up the middleware

Register your exception handlers in the service collection and add the middleware to the pipeline:

```csharp
// Register exception handlers
builder.Services.AddHttpContextExceptionHandler<Exception, DefaultExceptionHandler>();
builder.Services.AddHttpContextExceptionHandler<NotFoundException, NotFoundExceptionHandler>();
builder.Services.AddHttpContextExceptionHandler<ValidationException, ValidationExceptionHandler>();

var app = builder.Build();

// Add the middleware (should be early in the pipeline)
app.UseHttpContextExceptionHandling();
```

The middleware catches any unhandled exception from downstream middleware and controllers, finds the most specific registered handler, and delegates to it.

A catch-all `Exception` handler is included as a fallback that returns an HTTP 500 response.

## Creating exception handlers

Each handler implements `IHttpContextExceptionHandler<TException>` and writes the response:

```csharp
using MADE.Web.Exceptions;
using MADE.Web.Extensions;

public class NotFoundExceptionHandler : IHttpContextExceptionHandler<NotFoundException>
{
    public async Task HandleAsync(HttpContext context, NotFoundException exception)
    {
        var response = new ExceptionResponse<NotFoundException>(
            "NOT_FOUND",
            exception.Message,
            exception);

        await context.Response.WriteJsonAsync(HttpStatusCode.NotFound, response);
    }
}
```

The `ExceptionResponse<T>` type provides a structured error body with an error code, message, and exception details. The `WriteJsonAsync` extension serializes it as JSON and sets the status code.

## How handler resolution works

When an exception is thrown, the middleware looks for a handler registered for the exception's exact type. If none is found, it walks up the inheritance chain until it finds a match. The `Exception` catch-all handler ensures every exception gets handled.

This means you only need to register handlers for the exception types where you want specific behavior. Everything else falls through to the default handler.

## Best practices

- **Register the middleware early** in the pipeline (before routing and authentication) so it catches exceptions from all downstream components.
- **Always register a catch-all `Exception` handler** as a safety net.
- **Don't expose internal exception details in production.** Use `ExceptionResponse` to return a controlled error code and message. Include stack traces only in development environments.
- **Use exception types to communicate intent.** Define custom exceptions like `NotFoundException`, `ConflictException`, and `ForbiddenException` and map them to appropriate HTTP status codes through handlers.
