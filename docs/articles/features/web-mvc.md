---
uid: package-web-mvc
title: Using the Web MVC package
---

# Using the Web MVC package

The Web MVC library is a complementary extension package to ASP.NET Core MVC applications, providing additional helpers for building applications following the MVC pattern.

## Returning an internal server error ObjectResult

Out-of-the-box, the ASP.NET Core MVC packages don't contain a way of returning an internal server error (500) `ObjectResult` if an error occurs in your application.

The `InternalServerErrorObjectResult` can be used to achieve this. It contains two constructors, one for providing the error, and another for providing a `ModelStateDictionary` model state containing validation errors. 

## Returning JSON with a custom status code using JsonResult

The `MADE.Web.Mvc.Responses.JsonResult` is a custom `ActionResult` that serializes a value as JSON using `System.Text.Json` and returns it with a configurable HTTP status code.

```csharp
return new MADE.Web.Mvc.Responses.JsonResult(myObject, HttpStatusCode.Created);
```

You can also pass custom `JsonSerializerOptions` to control serialization behavior.

## Returning a forbidden ObjectResult

The `ForbiddenObjectResult` can be used to return a Forbidden (403) response from your API controllers. Like `InternalServerErrorObjectResult`, it supports both an error object and a `ModelStateDictionary`.

## Controller extensions

The `MADE.Web.Mvc.Extensions.ControllerBaseExtensions` class provides helper methods for returning common action results from controllers:

- `Json(object, HttpStatusCode, JsonSerializerOptions?)` - Returns a `JsonResult` with a custom status code.
- `InternalServerError(object)` - Returns an `InternalServerErrorObjectResult` with an error value.
- `InternalServerError(ModelStateDictionary)` - Returns an `InternalServerErrorObjectResult` with model state validation errors.
- `Forbidden(object)` - Returns a `ForbiddenObjectResult` with an error value.
- `Forbidden(ModelStateDictionary)` - Returns a `ForbiddenObjectResult` with model state validation errors.
