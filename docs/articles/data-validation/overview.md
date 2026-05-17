---
uid: data-validation-overview
title: Data Validation
---

# Data Validation

Input validation is one of those things that every application needs, but the approach varies wildly between projects. Some developers inline validation logic in controllers, others scatter it across service layers, and some build custom frameworks that are hard to extend. The result is usually inconsistent validation that's difficult to test and maintain.

`MADE.Data.Validation` provides a lightweight, composable validation pipeline. You pick validators from a library of 30+ built-in types, compose them into a `ValidatorCollection`, and validate values with a single call. The framework is agnostic of your application architecture - it works in ASP.NET Core, MAUI, console apps, or anywhere else.

```bash
dotnet add package MADE.Data.Validation
```

## What's included

| Guide | What it covers |
| --- | --- |
| [Built-in validators](built-in-validators.md) | All 30+ `IValidator` types: `RequiredValidator`, `EmailValidator`, `BetweenValidator`, `RegexValidator`, and more. |
| [Custom validators](custom-validators.md) | How to implement `IValidator` for your own validation logic. |
| [Async validation](async-validation.md) | `IAsyncValidator` and `AsyncValidatorCollection` for validation that requires I/O operations. |
| [FluentValidation integration](fluent-validation.md) | Using FluentValidation's `AbstractValidator` types with MADE's validation pipeline. |

## When to use this package

- You want a consistent validation pattern across your application without adopting a full framework.
- You need composable validators that can be combined, reused, and tested independently.
- You need async validation (e.g., checking uniqueness against a database).
- You want to use FluentValidation validators alongside MADE validators in the same pipeline.

## Quick example

```csharp
using MADE.Data.Validation;
using MADE.Data.Validation.Validators;

// Compose validators
var validators = new ValidatorCollection
{
    new RequiredValidator(),
    new MinLengthValidator { MinLength = 3 },
    new MaxLengthValidator { MaxLength = 50 },
    new AlphaNumericValidator(),
};

// Validate a value
validators.Validate("hello123");

if (validators.IsInvalid)
{
    foreach (var message in validators.FeedbackMessages)
    {
        Console.WriteLine(message);
    }
}
```

The `ValidatorCollection` runs every validator and aggregates the results. You get:

- **`IsInvalid`** - `true` if any validator failed.
- **`FeedbackMessages`** - human-readable error descriptions from each failed validator.
- **`Validated`** event - fires after validation completes, useful for UI binding.
