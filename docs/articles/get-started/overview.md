---
uid: get-started-overview
title: Overview
---

# Overview

MADE.NET is a collection of lightweight .NET libraries that handle the repetitive plumbing code you end up writing in every project. Data validation, HTTP networking, Entity Framework Core conventions, collection manipulation, threading primitives - the kind of code that's too small to justify a framework but too common to keep rewriting.

Every library in the toolkit targets `net8.0` and `net10.0`, ships as an independent NuGet package, and has zero opinions about your architecture. Pick the packages you need, ignore the ones you don't.

## What's in the toolkit

| Package | What it does |
| --- | --- |
| [MADE.Collections](../collections/overview.md) | Extensions for lists, dictionaries, observables, and queryables that reduce collection boilerplate. |
| [MADE.Data.Converters](../data-converters/overview.md) | Value converters and extension methods for transforming between types - dates, strings, enums, units, and more. |
| [MADE.Data.EFCore](../data-efcore/overview.md) | Entity base classes, soft-delete, audit tracking, UTC date handling, and query helpers for Entity Framework Core. |
| [MADE.Data.Serialization](../data-serialization/overview.md) | A `System.Text.Json` converter for migrating type metadata in serialized JSON when you refactor your codebase. |
| [MADE.Data.Validation](../data-validation/overview.md) | A framework-agnostic validation pipeline with 30+ built-in validators, async support, and FluentValidation integration. |
| [MADE.Diagnostics](../diagnostics/overview.md) | File-based event logging and application-wide unhandled exception handling. |
| [MADE.Networking](../networking/overview.md) | Typed HTTP request handlers with JSON serialization, retry with exponential backoff, and `IHttpClientFactory` integration. |
| [MADE.Runtime](../runtime/overview.md) | Fluent action chaining, weak reference callbacks, and reflection helpers. |
| [MADE.Testing](../testing/overview.md) | Framework-agnostic fluent test assertions that work with NUnit, xUnit, MSTest, or any other test runner. |
| [MADE.Threading](../threading/overview.md) | A modern timer, debouncer, throttler, async lazy initialization, and task extensions. |
| [MADE.Web](../web/overview.md) | ASP.NET Core middleware for exception handling, pagination types, authentication helpers, and API versioning. |
| [MADE.Web.Mvc](../web-mvc/overview.md) | Additional action result types and controller extensions for ASP.NET Core MVC. |

## Installation

Every package is available on NuGet. Install the ones you need via the .NET CLI:

```bash
dotnet add package MADE.Collections
dotnet add package MADE.Data.Validation
dotnet add package MADE.Networking
```

Or via the NuGet Package Manager in Visual Studio - search for `MADE.` to see all available packages.

## Design principles

**Independent packages.** Each library is a standalone NuGet package with minimal dependencies. You never pull in code you don't use.

**No architecture opinions.** MADE.NET provides building blocks, not a framework. Use them in ASP.NET Core, console apps, MAUI, Uno Platform, or anything else that runs on .NET.

**Familiar APIs.** Everything follows standard .NET patterns - extension methods, interfaces, dependency injection registration. If you know .NET, you already know how to use MADE.NET.

## Next steps

- **[Quickstart](quickstart.md)** - Install a package and write your first code in under 5 minutes.
- **[What's new in v3.0](whats-new/v3.md)** - See what changed in the latest major release.
- **[Migrating from v2 to v3](migration/v2-to-v3.md)** - Step-by-step upgrade guide for existing users.
