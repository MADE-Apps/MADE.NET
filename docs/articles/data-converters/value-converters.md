---
uid: data-converters-value-converters
title: Value converters
---

# Value converters

Value converters provide bidirectional type conversion through a consistent `Convert` and `ConvertBack` API. They're useful when you need to transform data for display and then parse it back - form inputs, configuration values, serialization, or UI binding.

All converters implement `IValueConverter<TFrom, TTo>`, which means they're predictable: you always know the input type, the output type, and that you can reverse the conversion.

## BooleanToStringValueConverter

Converts `bool` values to configurable string representations. This is useful for displaying boolean state in UIs where "Yes/No", "Active/Inactive", or "Enabled/Disabled" is more meaningful than "True/False".

```csharp
using MADE.Data.Converters;

var converter = new BooleanToStringValueConverter
{
    TrueValue = "Yes",
    FalseValue = "No",
};

string display = converter.Convert(true);     // "Yes"
bool original = converter.ConvertBack("No");  // false
```

## DateTimeToStringValueConverter

Converts between `DateTime` and `String` using standard .NET format strings. The format is passed as a parameter to `Convert` and `ConvertBack`, giving you flexibility at the call site.

```csharp
using MADE.Data.Converters;

var converter = new DateTimeToStringValueConverter();

string formatted = converter.Convert(DateTime.Now, "yyyy-MM-dd"); // "2026-05-17"
DateTime parsed = converter.ConvertBack("2026-05-17", "yyyy-MM-dd");
```

See the [Microsoft documentation on date format strings](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings) for all available format specifiers.

## StringToEnumValueConverter

Converts between string values and enum types with case-insensitive matching by default:

```csharp
using MADE.Data.Converters;

var converter = new StringToEnumValueConverter<DayOfWeek>();

DayOfWeek day = converter.Convert("monday");    // DayOfWeek.Monday
string name = converter.ConvertBack(DayOfWeek.Friday); // "Friday"
```

Set `IgnoreCase` to `false` if you need exact case matching. The converter throws `InvalidDataConversionException` if the string can't be parsed as the target enum type.

## DateTimeToUnixTimestampValueConverter

Converts between `DateTime` and Unix timestamps (seconds since 1970-01-01 UTC). Useful for interop with APIs that use Unix time:

```csharp
using MADE.Data.Converters;

var converter = new DateTimeToUnixTimestampValueConverter();

long timestamp = converter.Convert(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc));
DateTime dateTime = converter.ConvertBack(timestamp);
```

## Building your own converters

If you need a conversion that isn't covered by the built-in converters, implement `IValueConverter<TFrom, TTo>`:

```csharp
using MADE.Data.Converters;

public class CelsiusToFahrenheitConverter : IValueConverter<double, double>
{
    public double Convert(double celsius)
    {
        return (celsius * 9.0 / 5.0) + 32.0;
    }

    public double ConvertBack(double fahrenheit)
    {
        return (fahrenheit - 32.0) * 5.0 / 9.0;
    }
}
```

The interface gives you a consistent pattern that other developers on your team will immediately understand, and it makes your converters easy to register in DI containers or use in data binding pipelines.

If you think a converter would be broadly useful, [raise a feature request on GitHub](https://github.com/MADE-Apps/MADE.NET/issues/new/choose).
