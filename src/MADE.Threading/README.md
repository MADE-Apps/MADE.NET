# MADE.NET Threading

[![GitHub release](https://img.shields.io/github/release/MADE-Apps/MADE.NET.svg)](https://github.com/MADE-Apps/MADE.NET/releases)
[![Build status](https://github.com/MADE-Apps/MADE.NET/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/MADE-Apps/MADE.NET/actions/workflows/ci.yml)
[![Twitter Followers](https://img.shields.io/twitter/follow/jamesmcroft?label=follow%20%40jamesmcroft&style=flat)](https://twitter.com/jamesmcroft)
[![Nuget](https://img.shields.io/nuget/v/MADE.Threading.svg)](https://www.nuget.org/packages/MADE.Threading)
[![MADE.NET docs](https://img.shields.io/badge/docs-MADE.NET-blue.svg)](https://made-apps.github.io/MADE.NET/)

The MADE.NET Threading library provides helpers and extensions that help with application scenarios that care about threads. Amongst the features, you'll find an improved take on the .NET `Timer` implementation to make it easier for you to use in your apps!

## Features ⭐

- **Timer** - Providing an easy-to-use alternative to the `System.Threading.Timer` with `Start` and `Stop` methods.
- **TaskExtensions** - A collection of methods that build on the `Task` type to provide support for ensuring thrown exceptions are observed, and simply call `WhenAll` and `WhenAny` on `Task` collections. [Find out more about extensions for Tasks](https://made-apps.github.io/MADE.NET/api/MADE.Threading.TaskExtensions.html).

## Getting started

### Get the library

You can install the Threading library into your dotnet application by running the following command:

```bash
dotnet add package MADE.Threading
```

Or by adding the `MADE.Threading` package in your NuGet package manager of choice.

## Contributing 🤝🏻

Contributions, issues and feature requests are welcome!

Feel free to check the [issues page](https://github.com/MADE-Apps/MADE.NET/issues). You can also take a look at the [contributing guide](https://github.com/MADE-Apps/MADE.NET/blob/main/CONTRIBUTING.md).

We actively encourage you to jump in and help with any issues, and if you find one, don't forget to log it!

## Support this project 💗

As many developers know, projects like this are built and maintained in maintainers' spare time. If you find this project useful, [please **Star** or sponsor the repo on GitHub](https://github.com/MADE-Apps/MADE.NET).

## License

This project is made available under the terms and conditions of the [MIT license](LICENSE).
