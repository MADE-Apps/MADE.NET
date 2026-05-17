---
uid: data-converters-overview
title: Data Converters
---

# Data Converters

Type conversion is one of those tasks that's deceptively simple until you're doing it everywhere. Converting between dates and strings, formatting booleans for display, parsing strings to numbers, generating URL slugs, converting units - it's all straightforward code, but it adds up fast.

`MADE.Data.Converters` provides a collection of value converters and extension methods that handle these common transformations. The converters follow a consistent `IValueConverter<TFrom, TTo>` pattern with `Convert` and `ConvertBack` methods, making them predictable and easy to compose.

```bash
dotnet add package MADE.Data.Converters
```

## What's included

| Guide | What it covers |
| --- | --- |
| [Value converters](value-converters.md) | `BooleanToStringValueConverter`, `DateTimeToStringValueConverter`, `StringToEnumValueConverter`, `DateTimeToUnixTimestampValueConverter`, and building your own. |
| [String extensions](string-extensions.md) | `ToTitleCase`, `Truncate`, `ToSlug`, `ToBase64`/`FromBase64`, `ToInt`, `ToBoolean`, and other string parsing/formatting. |
| [DateTime extensions](datetime-extensions.md) | `ToCurrentAge`, `StartOfDay`/`EndOfDay`, `StartOfWeek`/`EndOfWeek`, `StartOfMonth`/`EndOfMonth`, `SetTime`, and more. |
| [Math and units](math-and-units.md) | Angle conversions, length unit conversions, file size formatting, TimeSpan utilities, and boolean formatting. |

## When to use this package

- You need bidirectional type conversion (e.g., `bool` to display string and back) and want a consistent, reusable pattern.
- You're working with date manipulation - calculating ages, finding period boundaries, rounding to the nearest hour.
- You need string utilities like slug generation, Base64 encoding, or safe numeric parsing that returns `null` instead of throwing.
- You want human-readable formatting for file sizes, time spans, or boolean values.

## Quick example

```csharp
using MADE.Data.Converters;
using MADE.Data.Converters.Extensions;

// Bidirectional bool conversion
var converter = new BooleanToStringValueConverter { TrueValue = "Active", FalseValue = "Inactive" };
string label = converter.Convert(true);   // "Active"
bool value = converter.ConvertBack("Inactive"); // false

// String utilities
string slug = "My Blog Post Title!".ToSlug(); // "my-blog-post-title"
int? parsed = "not-a-number".ToNullableInt(); // null (no exception)

// Date helpers
DateTime dob = new(1990, 6, 15);
int age = dob.ToCurrentAge(); // calculates current age in years

// File size formatting
long bytes = 1_572_864;
string size = bytes.ToHumanReadableFileSize(); // "1.50 MB"
```
