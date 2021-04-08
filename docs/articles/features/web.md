---
uid: package-web
title: Using the Web package | MADE.NET
---

# Using the Web package

The Web library contains a collection of helpers and extensions that sit on top of ASP.NET Core, to provide useful components to complement your web applications.

## Improved HttpContext exception handling with HttpContextExceptionsMiddleware

The Web package contains a `MADE.Web.Exceptions.HttpContextExceptionsMiddleware` which can be used with your ASP.NET Core application to manage how exceptions are handled and errors are returned using JSON responses.

Using the `UseHttpContextExceptionHandling` extension method on your `IApplicationBuilder` instance in the `Startup` of your web application, you can start taking advantage of improved error handling with MADE.

Under the hood, exceptions are handled using `IHttpContextExceptionHandler` types which are registered with your application's `IServiceCollection`.

The implementation has a basic catch-all `Exception` handler which returns an internal server error (500) when an unhandled exception is thrown. This can be registered alongside your own exception handlers in your `Startup` class.

### Registering the HttpContextExceptionsMiddleware and handlers

Below is an example of configuring the exception handling middleware and exception handlers in your application's `Startup` class.

```csharp
// Startup.cs

public void Configure( IApplicationBuilder app )
{
    // Registers the middleware for exception handling
    app.
    UseHttpContextExceptionHandling();
}

public void ConfigureServices( IServiceCollection services )
{
    // Registers the default exception handler.
    services.AddHttpContextExceptionHandler<Exception, DefaultExceptionHandler>

    services.AddHttpContextExceptionHandler<MyCustomException, MyCustomExceptionHandler>
}
```

And this is an example of a custom exception handler.

```csharp
namespace MyApp.Exceptions
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using MADE.Web.Exceptions;
    using MADE.Web.Extensions;
    using Microsoft.AspNetCore.Http;

    public class MyCustomExceptionHandler : IHttpContextExceptionHandler<MyCustomException>
    {
        public async Task HandleAsync(HttpContext context, MyCustomException exception)
        {
            var response = new ExceptionResponse<MyCustomException>("MyCustomExceptionError", "An error occurred causing my custom exception to be thrown.", exception);

            await context.Response.WriteJsonAsync(HttpStatusCode.InternalServerError, response);
        }
    }
}
```

## Improving pagination support with PaginatedRequest and PaginatedResponse

Pagination is a common concept for web requests to help improve performance of UIs and reduce network traffic by querying a subset of data.

The `PaginatedRequest` and `PaginatedResponse` can be used together to help you achieve these performance improvements in your own web applications.

The `PaginatedRequest` takes a page and page size parameter when constructed, and it automatically provides you with the `Skip` and `Take` properties that you can provide to your data queries.

When you have your data from your request, you can construct a response using the `PaginatedResponse` which takes the data, the original page and page size parameters, and the number of available items. It will also provide the `TotalPages` that are available to allow a UI to generate a pagination user experience.
