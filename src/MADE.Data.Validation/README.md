# MADE.NET Data Validation

[![GitHub release](https://img.shields.io/github/release/MADE-Apps/MADE.NET.svg)](https://github.com/MADE-Apps/MADE.NET/releases)
[![Build status](https://github.com/MADE-Apps/MADE.NET/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/MADE-Apps/MADE.NET/actions/workflows/ci.yml)
[![Twitter Followers](https://img.shields.io/twitter/follow/jamesmcroft?label=follow%20%40jamesmcroft&style=flat)](https://twitter.com/jamesmcroft)
[![Nuget](https://img.shields.io/nuget/v/MADE.Data.Validation.svg)](https://www.nuget.org/packages/MADE.Data.Validation)
[![MADE.NET docs](https://img.shields.io/badge/docs-MADE.NET-blue.svg)](https://made-apps.github.io/MADE.NET/)

The MADE.NET Data Validation library comes fully loaded with all the value validators you'd expect of any validation library. Easily get up and running with data validation on value ranges, alphanumeric, email address, min/max lengths, required, regular expressions and more as well as defining your own!

## Features ⭐

- **Validators** - A collection of out-of-the-box value validators including:
  - **AlphaNumericValidator** for values containing only alphanumeric characters
  - **AlphaValidator** for values containing only alpha characters
  - **Base64Validator** for values being a valid base64 string
  - **BetweenValidator** for values being within a minimum and maximum range
  - **EmailValidator** for values being a valid email address
  - **GuidValidator** for values being a valid GUID
  - **IpAddressValidator** for values being a valid IP address
  - **LatitudeValidator** for values being a valid latitude
  - **LongitudeValidator** for values being a valid longitude
  - **MacAddressValidator** for values being a valid MAC address
  - **MaxLengthValidator** for values below a maximum length
  - **MaxValueValidator** for values below a maximum value
  - **MinLengthValidator** for values above a minimum length
  - **MinValueValidator** for values above a minimum value
  - **PredicateValidator** for values meeting the condition of a value predicate
  - **RegexValidator** for values matching a regular expression
  - **RequiredValidator** for values that need to be set
  - **WellFormedUrlValidator** for values being a well-formed URL
- **ComparableExtensions** - Extending objects inheriting `IComparable` with methods for easily validating whether values are greater or less than others. [Discover the extensions available for comparable objects in our documentation](https://made-apps.github.io/MADE.NET/api/MADE.Data.Validation.Extensions.ComparableExtensions.html).
- **DateTimeExtensions** - Providing helper methods for validating that `DateTime` values are within ranges, or whether given dates are weekdays or weekends. [Find out more about validation extensions for DateTime](https://made-apps.github.io/MADE.NET/api/MADE.Data.Validation.Extensions.DateTimeExtensions.html).
- **MathExtensions** - A collection of extensions for validating numeric values are close to others, greater or less than, or within a range. [Explore more helpers for numeric values](https://made-apps.github.io/MADE.NET/api/MADE.Data.Validation.Extensions.MathExtensions.html).
- **StringExtensions** - Extensions methods for strings that validate whether they contain other phrases with configurable comparison options, or whether a string is like another using wildcard patterns similar to Visual Basic's like operator. [Discover the extensions available for validating strings](https://made-apps.github.io/MADE.NET/api/MADE.Data.Validation.Extensions.StringExtensions.html).

## Getting started

### Get the library

You can install the Data Validation library into your dotnet application by running the following command:

```bash
dotnet add package MADE.Data.Validation
```

Or by adding the `MADE.Data.Validation` package in your NuGet package manager of choice.

## Contributing 🤝🏻

Contributions, issues and feature requests are welcome!

Feel free to check the [issues page](https://github.com/MADE-Apps/MADE.NET/issues). You can also take a look at the [contributing guide](https://github.com/MADE-Apps/MADE.NET/blob/main/CONTRIBUTING.md).

We actively encourage you to jump in and help with any issues, and if you find one, don't forget to log it!

## Support this project 💗

As many developers know, projects like this are built and maintained in maintainers' spare time. If you find this project useful, [please **Star** or sponsor the repo on GitHub](https://github.com/MADE-Apps/MADE.NET).

## License

This project is made available under the terms and conditions of the [MIT license](LICENSE).
