---
uid: get-started-quickstart
title: Quickstart
---

# Quickstart

This guide gets you from zero to working code in under five minutes. You'll install a MADE.NET package, use it to solve a common problem, and see the result.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- A .NET project (console app, web API, class library - anything works)

## Install your first package

For this quickstart, we'll use `MADE.Data.Validation` to validate user input. Install it via the .NET CLI:

```bash
dotnet add package MADE.Data.Validation
```

## Validate a value in three lines

The validation library gives you a collection of ready-made validators that you compose together. Here's a complete example that validates an email address:

```csharp
using MADE.Data.Validation;
using MADE.Data.Validation.Validators;

var validators = new ValidatorCollection
{
    new RequiredValidator(),
    new EmailValidator(),
};

validators.Validate("user@example.com");
Console.WriteLine(validators.IsInvalid ? "Invalid" : "Valid");
// Output: Valid

validators.Validate("");
Console.WriteLine(validators.IsInvalid ? "Invalid" : "Valid");
// Output: Invalid
```

The `ValidatorCollection` runs every validator against the value and aggregates the results. You get a single `IsInvalid` flag and a `FeedbackMessages` collection with human-readable error descriptions.

## Try another package

Here's a taste of what other packages look like in practice:

### Collections - compare and sync collections

```bash
dotnet add package MADE.Collections
```

```csharp
using MADE.Collections;

var current = new List<string> { "Alice", "Bob", "Charlie" };
var updated = new List<string> { "Alice", "Diana" };

current.MakeEqualTo(updated);
// current is now ["Alice", "Diana"]
```

### Threading - debounce rapid actions

```bash
dotnet add package MADE.Threading
```

```csharp
using MADE.Threading;

var debouncer = new Debouncer { Delay = TimeSpan.FromMilliseconds(300) };

// Only the last call within 300ms actually executes
debouncer.Debounce(() => Console.WriteLine("Search executed"));
```

### Networking - make typed HTTP requests

```bash
dotnet add package MADE.Networking
```

```csharp
// In your service registration
services.AddNetworkRequestFactory();

// In your service
public class WeatherService(INetworkRequestFactory requestFactory)
{
    public async Task<WeatherForecast> GetForecastAsync()
    {
        var request = requestFactory.Get("https://api.example.com/weather");
        return await request.ExecuteAsync<WeatherForecast>();
    }
}
```

## What's next

Each package has its own section in the docs with an overview, detailed guides, and best practices:

- **[Collections](../collections/overview.md)** - Extensions for lists, dictionaries, and observables.
- **[Data Validation](../data-validation/overview.md)** - 30+ built-in validators with async and FluentValidation support.
- **[Networking](../networking/overview.md)** - Typed HTTP requests with retry and DI integration.
- **[Threading](../threading/overview.md)** - Timer, debouncer, throttler, and async patterns.
- **[Browse all packages](overview.md)** - See the complete package list.
