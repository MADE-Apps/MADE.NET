# MADE.NET Collections

[![GitHub release](https://img.shields.io/github/release/MADE-Apps/MADE.NET.svg)](https://github.com/MADE-Apps/MADE.NET/releases)
[![Build status](https://github.com/MADE-Apps/MADE.NET/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/MADE-Apps/MADE.NET/actions/workflows/ci.yml)
[![Twitter Followers](https://img.shields.io/twitter/follow/jamesmcroft?label=follow%20%40jamesmcroft&style=flat)](https://twitter.com/jamesmcroft)
[![Nuget](https://img.shields.io/nuget/v/MADE.Collections.svg)](https://www.nuget.org/packages/MADE.Collections)
[![MADE.NET docs](https://img.shields.io/badge/docs-MADE.NET-blue.svg)](https://made-apps.github.io/MADE.NET/)

The MADE.NET Collections library allows you to easily interact with collections, lists, arrays, and dictionaries with out-of-the-box extensions. It also comes with implementations for new collection types and comparison helpers.

## Features ⭐

- **GenericEqualityComparer** - For providing an equality comparer wrapper for any type.
- **ObservableItemCollection** - An ObservableCollection that manages both collection change events as well as contained item property change events.
- **CollectionExtensions** - Extending collection objects with methods for easily updating an existing object in the collection, or updating a collection to equal each other. [Discover the extensions available for collections in our documentation](https://made-apps.github.io/MADE.NET/api/MADE.Collections.CollectionExtensions.html).
- **DictionaryExtensions** - Providing helper methods for adding or updating items in a dictionary based on a given key, or getting a value or default for a given key. [Find out more about extensions for dictionaries](https://made-apps.github.io/MADE.NET/api/MADE.Collections.DictionaryExtensions.html).
- **QueryableExtensions** - A collection of extensions for queryable objects that allow chunking queries into batched sizes. [Explore more helpers for queryable objects](https://made-apps.github.io/MADE.NET/api/MADE.Collections.QueryableExtensions.html).

## Getting started

### Get the library

You can install the Collections library into your dotnet application by running the following command:

```bash
dotnet add package MADE.Collections
```

Or by adding the `MADE.Collections` package in your NuGet package manager of choice.

## Contributing 🤝🏻

Contributions, issues and feature requests are welcome!

Feel free to check the [issues page](https://github.com/MADE-Apps/MADE.NET/issues). You can also take a look at the [contributing guide](https://github.com/MADE-Apps/MADE.NET/blob/main/CONTRIBUTING.md).

We actively encourage you to jump in and help with any issues, and if you find one, don't forget to log it!

## Support this project 💗

As many developers know, projects like this are built and maintained in maintainers' spare time. If you find this project useful, [please **Star** or sponsor the repo on GitHub](https://github.com/MADE-Apps/MADE.NET).

## License

This project is made available under the terms and conditions of the [MIT license](LICENSE).
