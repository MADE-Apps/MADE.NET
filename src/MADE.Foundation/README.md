# MADE.NET Foundation

[![GitHub release](https://img.shields.io/github/release/MADE-Apps/MADE.NET.svg)](https://github.com/MADE-Apps/MADE.NET/releases)
[![Build status](https://github.com/MADE-Apps/MADE.NET/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/MADE-Apps/MADE.NET/actions/workflows/ci.yml)
[![Twitter Followers](https://img.shields.io/twitter/follow/jamesmcroft?label=follow%20%40jamesmcroft&style=flat)](https://twitter.com/jamesmcroft)
[![Nuget](https://img.shields.io/nuget/v/MADE.Foundation.svg)](https://www.nuget.org/packages/MADE.Foundation)
[![MADE.NET docs](https://img.shields.io/badge/docs-MADE.NET-blue.svg)](https://made-apps.github.io/MADE.NET/)

The MADE.NET Foundation library is a base library for MADE.NET that allows platform-specific logic to be defined with a helper for ensuring your code can continue to execute with API availability checks. You can even use this in your own projects!

## Features ⭐

- **PlatformApiHelper** - A helper that can check if a `Type`, method, or property of a type is supported by validating the existence of the `PlatformNotSupportedAttribute`.
- **PlatformNotSupportedAttribute** - An attribute that can be applied to a `Type`, method, or property of a type that should not be supported by a given platform.
- **PlatformNotSupportedException** - An exception that can be thrown if a `PlatformNotSupportedAttribute` is found.

## Getting started

### Get the library

You can install the Foundation library into your dotnet application by running the following command:

```bash
dotnet add package MADE.Foundation
```

Or by adding the `MADE.Foundation` package in your NuGet package manager of choice.

## Contributing 🤝🏻

Contributions, issues and feature requests are welcome!

Feel free to check the [issues page](https://github.com/MADE-Apps/MADE.NET/issues). You can also take a look at the [contributing guide](https://github.com/MADE-Apps/MADE.NET/blob/main/CONTRIBUTING.md).

We actively encourage you to jump in and help with any issues, and if you find one, don't forget to log it!

## Support this project 💗

As many developers know, projects like this are built and maintained in maintainers' spare time. If you find this project useful, [please **Star** or sponsor the repo on GitHub](https://github.com/MADE-Apps/MADE.NET).

## License

This project is made available under the terms and conditions of the [MIT license](LICENSE).
