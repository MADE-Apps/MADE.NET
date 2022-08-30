# MADE.NET Data Serialization

[![GitHub release](https://img.shields.io/github/release/MADE-Apps/MADE.NET.svg)](https://github.com/MADE-Apps/MADE.NET/releases)
[![Build status](https://github.com/MADE-Apps/MADE.NET/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/MADE-Apps/MADE.NET/actions/workflows/ci.yml)
[![Twitter Followers](https://img.shields.io/twitter/follow/jamesmcroft?label=follow%20%40jamesmcroft&style=flat)](https://twitter.com/jamesmcroft)
[![Nuget](https://img.shields.io/nuget/v/MADE.Data.Serialization.svg)](https://www.nuget.org/packages/MADE.Data.Serialization)
[![MADE.NET docs](https://img.shields.io/badge/docs-MADE.NET-blue.svg)](https://made-apps.github.io/MADE.NET/)

The MADE.NET Data Serialization library provides out-of-the-box serialization helpers and extensions for ensuring data quality when serializing to and from different formats such as JSON and XML.

## Features ⭐

- **JsonTypeMigrationSerializationBinder** - A JSON.NET serialization binder that allows an object type serialized to a JSON string can be migrated to a different object type. Useful when JSON has been serialized with the `TypeNameHandling` set to **All**. [Find out more about how to use the binder in your own projects](https://made-apps.github.io/MADE.NET/articles/features/data-serialization.html#handling-type-changes-in-json-objects-serialized-with-jsonnet-with-typenamehandling-set-to-all).

## Getting started

### Get the library

You can install the Data Serialization library into your dotnet application by running the following command:

```bash
dotnet add package MADE.Data.Serialization
```

Or by adding the `MADE.Data.Serialization` package in your NuGet package manager of choice.

## Contributing 🤝🏻

Contributions, issues and feature requests are welcome!

Feel free to check the [issues page](https://github.com/MADE-Apps/MADE.NET/issues). You can also take a look at the [contributing guide](https://github.com/MADE-Apps/MADE.NET/blob/main/CONTRIBUTING.md).

We actively encourage you to jump in and help with any issues, and if you find one, don't forget to log it!

## Support this project 💗

As many developers know, projects like this are built and maintained in maintainers' spare time. If you find this project useful, [please **Star** or sponsor the repo on GitHub](https://github.com/MADE-Apps/MADE.NET).

## License

This project is made available under the terms and conditions of the [MIT license](LICENSE).
