# MADE.NET for ASP.NET Core

[![GitHub release](https://img.shields.io/github/release/MADE-Apps/MADE.NET.svg)](https://github.com/MADE-Apps/MADE.NET/releases)
[![Build status](https://github.com/MADE-Apps/MADE.NET/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/MADE-Apps/MADE.NET/actions/workflows/ci.yml)
[![Twitter Followers](https://img.shields.io/twitter/follow/jamesmcroft?label=follow%20%40jamesmcroft&style=flat)](https://twitter.com/jamesmcroft)
[![Nuget](https://img.shields.io/nuget/v/MADE.Web.svg)](https://www.nuget.org/packages/MADE.Web)
[![MADE.NET docs](https://img.shields.io/badge/docs-MADE.NET-blue.svg)](https://made-apps.github.io/MADE.NET/)

The MADE.NET for ASP.NET Core library builds on features of ASP.NET Core to provide better support for your web API or MVC applications, including standardized pagination support, global authenticated user accessor, standardized exception handling with JSON responses, and API versioning.

## Features ⭐

- **HttpContextExceptionsMiddleware** - For handling application-wide exceptions thrown within an HTTP context providing JSON responses for known, registered exception types.
- **AuthenticatedUserAccessor** - For capturing the authenticated user of an HTTP context and providing a wrapper around the `ClaimsPrincipal`.
- **PaginatedRequest** and **PaginatedResponse** - For standardizing the approach to creating a request to retrieve paginated data from a source, and the expected response object that automatically calculates pages based on the request.
- **ApiVersioningExtensions** - Providing helper methods for configuring your web API with versioning support either via query or header. [Find out more about extensions for API versioning](https://made-apps.github.io/MADE.NET/api/MADE.Web.Extensions.ApiVersioningExtensions.html).
- **HttpResponseExtensions** - A collection of extensions for `HttpResponse` objects that wrapper JSON.NET to provide a standard approach to writing responses back as a JSON result based on a specified object. [Explore more helpers for HttpResponse objects](https://made-apps.github.io/MADE.NET/api/MADE.Web.Extensions.HttpResponseExtensions.html).

## Getting started

### Get the library

You can install the ASP.NET Core library into your dotnet application by running the following command:

```bash
dotnet add package MADE.Web
```

Or by adding the `MADE.Web` package in your NuGet package manager of choice.

## Contributing 🤝🏻

Contributions, issues and feature requests are welcome!

Feel free to check the [issues page](https://github.com/MADE-Apps/MADE.NET/issues). You can also take a look at the [contributing guide](https://github.com/MADE-Apps/MADE.NET/blob/main/CONTRIBUTING.md).

We actively encourage you to jump in and help with any issues, and if you find one, don't forget to log it!

## Support this project 💗

As many developers know, projects like this are built and maintained in maintainers' spare time. If you find this project useful, [please **Star** or sponsor the repo on GitHub](https://github.com/MADE-Apps/MADE.NET).

## License

This project is made available under the terms and conditions of the [MIT license](LICENSE).
