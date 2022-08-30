# MADE.NET for EF Core

[![GitHub release](https://img.shields.io/github/release/MADE-Apps/MADE.NET.svg)](https://github.com/MADE-Apps/MADE.NET/releases)
[![Build status](https://github.com/MADE-Apps/MADE.NET/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/MADE-Apps/MADE.NET/actions/workflows/ci.yml)
[![Twitter Followers](https://img.shields.io/twitter/follow/jamesmcroft?label=follow%20%40jamesmcroft&style=flat)](https://twitter.com/jamesmcroft)
[![Nuget](https://img.shields.io/nuget/v/MADE.Data.EFCore.svg)](https://www.nuget.org/packages/MADE.Data.EFCore)
[![MADE.NET docs](https://img.shields.io/badge/docs-MADE.NET-blue.svg)](https://made-apps.github.io/MADE.NET/)

The MADE.NET for EF Core library builds on the base Entity Framework library to provide base classes and helpers for maintaining data in databases.

## Features ⭐

- **EntityBase** - A base definition for an EF context object that includes a default primary key, as well as created and updated dates. It also has backing interfaces so you can pick and choose the bits you need to create your own. [Find out more about the EntityBase and the interfaces in our documentation](https://made-apps.github.io/MADE.NET/api/MADE.Data.EFCore.html).
- **UtcDateTimeConverter** - A converter for EF contexts to ensure that date values of models in the context are stored with a UTC kind.
- **DbContextExtensions** - A collection of helper extensions to `DbContext` objects that allow you to easily update and save changes to entities, set `EntityBase` dates automatically when saving, or safely try an action on a context. [Discover the extensions for DbContext instances](https://made-apps.github.io/MADE.NET/api/MADE.Data.EFCore.Extensions.DbContextExtensions.html).
- **EntityBaseExtensions** - Methods that help to configure the `EntityBase` models when creating an entity type configuration for EF, such as configuring the primary key and date values. [Explore more helpers for EntityBase](https://made-apps.github.io/MADE.NET/api/MADE.Data.EFCore.Extensions.EntityBaseExtensions.html).
- **QueryableExtensions** - Helpful extensions for EF `IQueryable` functions that provide support for pagination and smarter ordering capabilities. [Explore all the queryable extensions](https://made-apps.github.io/MADE.NET/api/MADE.Data.EFCore.Extensions.QueryableExtensions.html).

## Getting started

### Get the library

You can install the EF Core library into your dotnet application by running the following command:

```bash
dotnet add package MADE.Data.EFCore
```

Or by adding the `MADE.Data.EFCore` package in your NuGet package manager of choice.

## Contributing 🤝🏻

Contributions, issues and feature requests are welcome!

Feel free to check the [issues page](https://github.com/MADE-Apps/MADE.NET/issues). You can also take a look at the [contributing guide](https://github.com/MADE-Apps/MADE.NET/blob/main/CONTRIBUTING.md).

We actively encourage you to jump in and help with any issues, and if you find one, don't forget to log it!

## Support this project 💗

As many developers know, projects like this are built and maintained in maintainers' spare time. If you find this project useful, [please **Star** or sponsor the repo on GitHub](https://github.com/MADE-Apps/MADE.NET).

## License

This project is made available under the terms and conditions of the [MIT license](LICENSE).
