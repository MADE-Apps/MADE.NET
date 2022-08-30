# MADE.NET Runtime

[![GitHub release](https://img.shields.io/github/release/MADE-Apps/MADE.NET.svg)](https://github.com/MADE-Apps/MADE.NET/releases)
[![Build status](https://github.com/MADE-Apps/MADE.NET/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/MADE-Apps/MADE.NET/actions/workflows/ci.yml)
[![Twitter Followers](https://img.shields.io/twitter/follow/jamesmcroft?label=follow%20%40jamesmcroft&style=flat)](https://twitter.com/jamesmcroft)
[![Nuget](https://img.shields.io/nuget/v/MADE.Runtime.svg)](https://www.nuget.org/packages/MADE.Runtime)
[![MADE.NET docs](https://img.shields.io/badge/docs-MADE.NET-blue.svg)](https://made-apps.github.io/MADE.NET/)

The MADE.NET Runtime library is a base library that builds on .NET to provide additional types and extensions that you might expect to find in the BCL, but might not exist, such as a `WeakReferenceCallback`.

## Features ⭐

- **Chain** - A wrapper for an object type that allows you to chain together multiple instances to perform synchronous and asynchronous actions on them handled using `WeakReference`s.
- **WeakReferenceCallback** - An approach to performing callbacks at a later point in time using `WeakReference`s to the originator. Used in the MADE.NET Networking library for handling callbacks to fire-and-forget network requests using the `NetworkRequestManager`.
- **WeakReferenceEventHandler** - Similar to the callback, providing an approach to event handlers using `WeakReference`s.
- **ReflectionExtensions** - Providing helper methods that take advantage of Reflection APIs to get property names or specific property values from objects. [Find out more about reflection extensions](https://made-apps.github.io/MADE.NET/api/MADE.Runtime.Extensions.ReflectionExtensions.html).

## Getting started

### Get the library

You can install the Runtime library into your dotnet application by running the following command:

```bash
dotnet add package MADE.Runtime
```

Or by adding the `MADE.Runtime` package in your NuGet package manager of choice.

## Contributing 🤝🏻

Contributions, issues and feature requests are welcome!

Feel free to check the [issues page](https://github.com/MADE-Apps/MADE.NET/issues). You can also take a look at the [contributing guide](https://github.com/MADE-Apps/MADE.NET/blob/main/CONTRIBUTING.md).

We actively encourage you to jump in and help with any issues, and if you find one, don't forget to log it!

## Support this project 💗

As many developers know, projects like this are built and maintained in maintainers' spare time. If you find this project useful, [please **Star** or sponsor the repo on GitHub](https://github.com/MADE-Apps/MADE.NET).

## License

This project is made available under the terms and conditions of the [MIT license](LICENSE).
