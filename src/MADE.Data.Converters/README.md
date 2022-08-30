# MADE.NET Data Converters

[![GitHub release](https://img.shields.io/github/release/MADE-Apps/MADE.NET.svg)](https://github.com/MADE-Apps/MADE.NET/releases)
[![Build status](https://github.com/MADE-Apps/MADE.NET/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/MADE-Apps/MADE.NET/actions/workflows/ci.yml)
[![Twitter Followers](https://img.shields.io/twitter/follow/jamesmcroft?label=follow%20%40jamesmcroft&style=flat)](https://twitter.com/jamesmcroft)
[![Nuget](https://img.shields.io/nuget/v/MADE.Data.Converters.svg)](https://www.nuget.org/packages/MADE.Data.Converters)
[![MADE.NET docs](https://img.shields.io/badge/docs-MADE.NET-blue.svg)](https://made-apps.github.io/MADE.NET/)

The MADE.NET Data Converters library provides out-of-the-box value converters for taking values of one type and changing them to another. The advantage of the IValueConverter is that they can be chained together. The library also contains extension helpers for converting values directly.

## Features ⭐

- **BooleanToStringValueConverter** - For converting between a boolean and a specified string value representing true or false.
- **DateTimeToStringValueConverter** - A converter for changing DateTime values to and from string values representing them.
- **StringToBase64StringValueConverter** - Easily convert to and from base64 strings with a value converter.
- **BooleanExtensions** - Providing helper methods for directly formatting boolean values as specified strings. [Find out more about extensions for boolean values](https://made-apps.github.io/MADE.NET/api/MADE.Data.Converters.Extensions.BooleanExtensions.html).
- **CollectionExtensions** - A collection of extensions for converting collections to delimited strings, for example, comma-separated. [Explore more helpers for collections](https://made-apps.github.io/MADE.NET/api/MADE.Data.Converters.Extensions.CollectionExtensions.html).
- **DateTimeExtensions** - Helpful extensions that manipulate DateTime values including rounding to the nearest hour, or retrieving an age based on a date of birth. [Discover all the DateTime converter extensions](https://made-apps.github.io/MADE.NET/api/MADE.Data.Converters.Extensions.DateTimeExtensions.html).
- **StringExtensions** - Methods that help you to convert string values into other formats such as converting to and from base64, or safely converting a string to an int, double, float, or boolean. [Jump into our docs to find all our string extensions](https://made-apps.github.io/MADE.NET/api/MADE.Data.Converters.Extensions.StringExtensions.html).

## Getting started

### Get the library

You can install the Data Converters library into your dotnet application by running the following command:

```bash
dotnet add package MADE.Data.Converters
```

Or by adding the `MADE.Data.Converters` package in your NuGet package manager of choice.

## Contributing 🤝🏻

Contributions, issues and feature requests are welcome!

Feel free to check the [issues page](https://github.com/MADE-Apps/MADE.NET/issues). You can also take a look at the [contributing guide](https://github.com/MADE-Apps/MADE.NET/blob/main/CONTRIBUTING.md).

We actively encourage you to jump in and help with any issues, and if you find one, don't forget to log it!

## Support this project 💗

As many developers know, projects like this are built and maintained in maintainers' spare time. If you find this project useful, [please **Star** or sponsor the repo on GitHub](https://github.com/MADE-Apps/MADE.NET).

## License

This project is made available under the terms and conditions of the [MIT license](LICENSE).
