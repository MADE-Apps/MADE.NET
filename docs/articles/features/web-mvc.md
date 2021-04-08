---
uid: package-web-mvc
title: Using the Web MVC package | MADE.NET
---

# Using the Web MVC package

The Web MVC library is a complementary extension package to ASP.NET Core MVC applications, providing additional helpers for building applications following the MVC pattern.

## Returning an internal server error ObjectResult

Out-of-the-box, the ASP.NET Core MVC packages don't contain a way of returning an internal server error (500) `ObjectResult` if an error occurs in your application.

The `InternalServerErrorObjectResult` can be used to achieve this. It contains two constructors, one for providing the error, and another for providing a `ModelStateDictionary` model state containing validation errors. 
