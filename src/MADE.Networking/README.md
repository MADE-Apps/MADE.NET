# MADE.NET Networking

[![GitHub release](https://img.shields.io/github/release/MADE-Apps/MADE.NET.svg)](https://github.com/MADE-Apps/MADE.NET/releases)
[![Build status](https://github.com/MADE-Apps/MADE.NET/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/MADE-Apps/MADE.NET/actions/workflows/ci.yml)
[![Twitter Followers](https://img.shields.io/twitter/follow/jamesmcroft?label=follow%20%40jamesmcroft&style=flat)](https://twitter.com/jamesmcroft)
[![Nuget](https://img.shields.io/nuget/v/MADE.Networking.svg)](https://www.nuget.org/packages/MADE.Networking)
[![MADE.NET docs](https://img.shields.io/badge/docs-MADE.NET-blue.svg)](https://made-apps.github.io/MADE.NET/)

The MADE.NET Networking library comes fully loaded with wrappers for easily executing network requests from applications, handling the responses, as well as providing extensions for common URI scenarios. A perfect companion to any application handling networking.

## Features ⭐

- **Network Requests** - A collection of out-of-the-box network request wrappers including:
  - **JsonGetNetworkRequest** for executing a GET call with a JSON response
  - **JsonPostNetworkRequest** for executing a POST call with a JSON payload and response
  - **JsonPutNetworkRequest** for executing a PUT call with a JSON payload and response
  - **JsonPatchNetworkRequest** for executing a PATCH call with a JSON payload and response
  - **JsonDeleteNetworkRequest** for executing a DELETE call with a JSON response
  - **StreamGetNetworkRequest** for executing a GET call with a `Stream` response
- **NetworkRequestManager** - A service that can handle the processing of `NetworkRequest` instances in the background of a running application with callbacks on responses. Useful for when you want a fire-and-forget style networking approach. [Discover how to use the NetworkRequestManager in your apps](https://made-apps.github.io/MADE.NET/features/networking.html#queuing-your-network-requests-using-networkrequestmanager).
- **HttpResponseMessage{T}** - Extending the base `HttpResponseMessage` type to allow deserializing content to a specific type.
- **HttpResponseMessageExtensions** - Providing helper methods for allowing `HttpResponseMessage` responses to be deserialized easily. [Find out more about HttpResponseMessage extensions](https://made-apps.github.io/MADE.NET/api/MADE.Networking.Extensions.HttpResponseMessageExtensions.html).
- **UriExtensions** - A collection of extensions for manipulating URI values such as getting a specific value from a query. [Explore more helpers for URI values](https://made-apps.github.io/MADE.NET/api/MADE.Networking.Extensions.UriExtensions.html).

## Getting started

### Get the library

You can install the Networking library into your dotnet application by running the following command:

```bash
dotnet add package MADE.Networking
```

Or by adding the `MADE.Networking` package in your NuGet package manager of choice.

## Contributing 🤝🏻

Contributions, issues and feature requests are welcome!

Feel free to check the [issues page](https://github.com/MADE-Apps/MADE.NET/issues). You can also take a look at the [contributing guide](https://github.com/MADE-Apps/MADE.NET/blob/main/CONTRIBUTING.md).

We actively encourage you to jump in and help with any issues, and if you find one, don't forget to log it!

## Support this project 💗

As many developers know, projects like this are built and maintained in maintainers' spare time. If you find this project useful, [please **Star** or sponsor the repo on GitHub](https://github.com/MADE-Apps/MADE.NET).

## License

This project is made available under the terms and conditions of the [MIT license](LICENSE).
