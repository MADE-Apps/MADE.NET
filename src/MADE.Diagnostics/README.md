# MADE.NET Diagnostics

[![GitHub release](https://img.shields.io/github/release/MADE-Apps/MADE.NET.svg)](https://github.com/MADE-Apps/MADE.NET/releases)
[![Build status](https://github.com/MADE-Apps/MADE.NET/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/MADE-Apps/MADE.NET/actions/workflows/ci.yml)
[![Twitter Followers](https://img.shields.io/twitter/follow/jamesmcroft?label=follow%20%40jamesmcroft&style=flat)](https://twitter.com/jamesmcroft)
[![Nuget](https://img.shields.io/nuget/v/MADE.Diagnostics.svg)](https://www.nuget.org/packages/MADE.Diagnostics)
[![MADE.NET docs](https://img.shields.io/badge/docs-MADE.NET-blue.svg)](https://made-apps.github.io/MADE.NET/)

The MADE.NET Diagnostics library provides helpers to make debugging and logging results in your applications easier.

## Features ⭐

- **AppDiagnostics** - An global exception handler for applications, with additional native app support for handling exceptions thrown in UWP, Android, and iOS applications.
- **FileEventLogger** - A simple event logger that can store the log to disk, with additional native app support for handling file systems in UWP, Android, and iOS applications.
- **StopwatchHelper** - A wrapper for creating and managing `Stopwatch` instances for calling methods with message outputs that include the detail of the Stopwatch and the elapsed time. Perfect for handling the timing of your code execution without the boilerplate setup for a `Stopwatch`. Run as many for as long as you need them with simple `Start` and `Stop` method calls.

## Getting started

### Get the library

You can install the Diagnostics library into your dotnet application by running the following command:

```bash
dotnet add package MADE.Diagnostics
```

Or by adding the `MADE.Diagnostics` package in your NuGet package manager of choice.

## Contributing 🤝🏻

Contributions, issues and feature requests are welcome!

Feel free to check the [issues page](https://github.com/MADE-Apps/MADE.NET/issues). You can also take a look at the [contributing guide](https://github.com/MADE-Apps/MADE.NET/blob/main/CONTRIBUTING.md).

We actively encourage you to jump in and help with any issues, and if you find one, don't forget to log it!

## Support this project 💗

As many developers know, projects like this are built and maintained in maintainers' spare time. If you find this project useful, [please **Star** or sponsor the repo on GitHub](https://github.com/MADE-Apps/MADE.NET).

## License

This project is made available under the terms and conditions of the [MIT license](LICENSE).
