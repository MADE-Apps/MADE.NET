---
uid: made-intro
title: Getting Started
---

# Getting Started

MADE.NET is a toolkit of easy-to-use extensions, helpers, and controls to make app development easier for .NET developers.

Whether you're building an ASP.NET Core Web API or a native UI application with Uno Platform, MADE.NET has something for everyone.

MADE.NET has been built on common code from projects built by the MADE team, and is now a home for all those bits of code that you know you will reuse in another project!

Most packages support the `netstandard2.0` target framework, with some packages opting to support platform specific frameworks such as .NET 6, UWP, and Xamarin.

## Installation

All packages listed below can be installed via NuGet or via the dotnet CLI by running the following command using a listed package name:

```
dotnet add package MADE.Collections
```

### Available packages

| Package | Version |
| --- | --- |
| MADE.Collections | [![NuGet](https://img.shields.io/nuget/v/MADE.Collections)](https://www.nuget.org/packages/MADE.Collections/) |
| MADE.Data.Converters | [![NuGet](https://img.shields.io/nuget/v/MADE.Data.Converters)](https://www.nuget.org/packages/MADE.Data.Converters/) |
| MADE.Data.EFCore | [![NuGet](https://img.shields.io/nuget/v/MADE.Data.EFCore)](https://www.nuget.org/packages/MADE.Data.EFCore/) |
| MADE.Data.Validation | [![NuGet](https://img.shields.io/nuget/v/MADE.Data.Validation)](https://www.nuget.org/packages/MADE.Data.Validation/) |
| MADE.Data.Validation.FluentValidation | [![NuGet](https://img.shields.io/nuget/v/MADE.Data.Validation.FluentValidation)](https://www.nuget.org/packages/MADE.Data.Validation.FluentValidation/) |
| MADE.Diagnostics | [![NuGet](https://img.shields.io/nuget/v/MADE.Diagnostics)](https://www.nuget.org/packages/MADE.Diagnostics/) |
| MADE.Networking | [![NuGet](https://img.shields.io/nuget/v/MADE.Networking)](https://www.nuget.org/packages/MADE.Networking/) |
| MADE.Runtime | [![NuGet](https://img.shields.io/nuget/v/MADE.Runtime)](https://www.nuget.org/packages/MADE.Runtime/) |
| MADE.Testing | [![NuGet](https://img.shields.io/nuget/v/MADE.Testing)](https://www.nuget.org/packages/MADE.Testing/) |
| MADE.Threading | [![NuGet](https://img.shields.io/nuget/v/MADE.Threading)](https://www.nuget.org/packages/MADE.Threading/) |
| MADE.Web | [![NuGet](https://img.shields.io/nuget/v/MADE.Web)](https://www.nuget.org/packages/MADE.Web/) |
| MADE.Web.Mvc | [![NuGet](https://img.shields.io/nuget/v/MADE.Web.Mvc)](https://www.nuget.org/packages/MADE.Web.Mvc/) |

#### Collections

The Collections package is designed to provide helpful extensions and additional types for working with enumerable objects in your applications.

It includes features such as:

- GenericEqualityComparer, a `IEqualityComparer` implementation for comparing two objects using a simple comparison function.
- ObservableItemCollection, a `ObservableCollection` implementation that takes a `INotifyPropertyChanged` item type which manages and surfaces up property changed events.
- CollectionExtensions, a collection of extensions for enumerable objects including `Update` (to update an existing item), `MakeEqualTo` (to update a collection's items to be equal to another), and `AreEquivalent` (to compare two collections contain the same items ignoring order).

<span class="button">

[Discover Collections](features/collections.md)

</span>

#### Data.Converters

The Data Converters package provides a collection of value converters and extensions to manipulate data in your applications.

It includes features such as:

- DateTimeToStringValueConverter, a value converter that takes a `DateTime` string format parameter to convert a `DateTime` value to a `String`, with the capability to convert back.
- DateTimeExtensions, a collection of useful extensions for interacting with `DateTime` values including `ToCurrentAge` (to get an age in years based on a given date from today) and `SetTime` (to override the time part of a `DateTime` value).
- MathExtensions, a collection of extensions for common mathematic expressions including `ToRadians` (to convert a degrees value to radians).
- StringExtensions, a collection of extensions for manipulating `String` values such as `ToTitleCase`, `ToDefaultCase`, `ToInt`, `ToBoolean`, `ToFloat`, and `ToDouble`.

<span class="button">

[Discover Data.Converters](features/data-converters.md)

</span>

#### Data.EFCore

The Data Entity Framework Core package provides a collection of helpers, extensions, and converters for applications taking advantage of the `Microsoft.EntityFrameworkCore` library.

It includes features such as:

- DbContextExtensions, for additional helpers to EF data contexts such as asynchronous update & save.
- EntityBase, for providing a base definition for entities including a GUID identifier, created, and updated dates.
- UtcDateTimeConverter, to help with the storing of entity model dates in a UTC format.

<span class="button">

[Discover Data.EFCore](features/data-efcore.md)

</span>

#### Data.Validation

The Data Validation package is designed to provide out-of-the-box data validation to applications built with C#. 

It provides easy-to-use validation helpers such as:

- AlphaValidator, for validating a string contains only alpha characters.
- AlphaNumericValidator, for validating a string contains only alphanumeric characters.
- BetweenValidator, for validating whether a value is within a minimum and maximum range.
- EmailValidator, for validating whether a value is a valid email address using a regular expression.
- IpAddressValidator, for validating whether a value is a valid IP address.
- MaxValueValidator, for validating whether a value is below a maximum value.
- MinValueValidator, for validating whether a value is above a minimum value.
- RegexValidator, for validating a value based on a specified regular expression.
- RequiredValidator, for validating a value is not null, false, whitespace, or empty.

<span class="button">

[Discover Data.Validation](features/data-validation.md)

</span>

#### Diagnostics

The Diagnostic package contains a set of simple application logging mechanisms for applications.

It includes features such as:

- FileEventLogger, for logging debug, info, warning, error, and critical messages to a file.
- StopwatchHelper, for aiding with tracking time for multiple long running operations.
- AppDiagnostics, for providing an application-wide exception handler.

<span class="button">

[Discover Diagnostics](features/diagnostics.md)

</span>

#### Networking

The Networking package contains a collection of helpers for applications that use `HttpClient` for making network requests to APIs.

It includes features such as:

- NetworkRequestManager, for managing a queue of HTTP network requests with success and error callbacks.
- JsonGetNetworkRequest, for making a HTTP GET request with a JSON response, deserializing to a specified type.
- JsonPostNetworkRequest, for making a HTTP POST request with a JSON payload, and a JSON response.
- JsonPutNetworkRequest, for making a HTTP PUT request with a JSON payload, and a JSON response.
- JsonPatchNetworkRequest, for making a HTTP PATCH request with a JSON payload, and a JSON response.
- JsonDeleteNetworkRequest, for making a HTTP DELETE request with a JSON response.
- StreamGetNetworkRequest, for making a HTTP GET request with a data stream response.

<span class="button">

[Discover Networking](features/networking.md)

</span>

#### Runtime

The Runtime package provides additional types for .NET to provide extensibility over existing `System` types.

This includes features such as:

- WeakReferenceCallback, a wrapper type for `WeakReference` which allows an action to be stored and invoked at a later point in time, if the object the action is associated with is still alive at the point of invocation. 

<span class="button">

[Discover Runtime](features/runtime.md)

</span>

#### Testing

The Testing package is an extension library for assertions in unit testing projects, agnostic of unit testing framework.

It provides additional assertions such as:

- CollectionAssertExtensions, a collection of extensions for asserting enumerable objects including `ShouldBeEquivalentTo` (comparing two collections to ensure they contain the same items ignoring order), and `ShouldNotBeEquivalentTo` (comparing two collection to ensure they do not contain the same items ignoring order).

<span class="button">

[Discover Testing](features/testing.md)

</span>

#### Threading

The Threading package contains a collection of `System.Threading` extensions and helpers to improve the developer experience.

It includes features such as:

- Timer, a modern take on `System.Threading.Timer` providing properties for configuring the `Interval` and `DueTime`, plus an event handler for `Tick`. It includes simple methods to `Start` and `Stop` the timer running.

<span class="button">

[Discover Threading](features/threading.md)

</span>

#### Web

The Web library contains a collection of helpers and extensions that sit on top of ASP.NET Core, to provide useful components to complement your web applications.

This includes features such as:

- PaginatedRequest, a simple request object that provides the expected return type, with properties for the current `Page`, the `PageSize`, and the number of items to `Skip` and `Take`.
- PaginatedResponse, a complementary response return type for the `PaginatedRequest`, with properties including the `Items` collection, the current `Page` and `PageSize`, plus the `AvailableCount` of requestable items, and the `TotalPages` based on the available count and page size requested.
- HttpContextExceptionsMiddleware, a middleware that manages the handling of exceptions thrown within a `HttpContext` to serve up meaningful exception details to the requesting client using exception handlers.

<span class="button">

[Discover Web](features/web.md)

</span>

#### Web.Mvc

The Web MVC library is a complementary extension package to ASP.NET Core MVC applications, providing additional helpers for building applications following the MVC pattern.

Included in this package is:

- InternalServerErrorObjectResult, an `ObjectResult` type that returns an Internal Server Error (500) with the optional `ModelStateDictionary` of validation errors.

<span class="button">

[Discover Web.Mvc](features/web-mvc.md)

</span>
