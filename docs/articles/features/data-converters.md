---
uid: package-data-converters
title: Using the Data Converters package
---

# Using the Data Converters package

The Data Converters package provides a collection of value converters and extensions to manipulate data in your applications.

## Converting a bool to a String using the BooleanToStringValueConverter

The `MADE.Data.Converters.BooleanToStringValueConverter` converts `bool` values to configurable `String` representations using the `TrueValue` and `FalseValue` properties.

```csharp
var converter = new BooleanToStringValueConverter
{
    TrueValue = "Yes",
    FalseValue = "No"
};

string result = converter.Convert(true); // "Yes"
bool original = converter.ConvertBack("No"); // false
```

## Converting a DateTime to a String using the DateTimeToStringValueConverter

The `MADE.Data.Converters.DateTimeToStringValueConverter` converts a `DateTime` value to a `String` using a format parameter. The format parameter must be a valid `DateTime` string format [based on the Microsoft documentation](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings).

Below is an example of this in use.

```csharp
namespace App.Conversions
{
    using MADE.Data.Converters;

    public class ApplicationConverters
    {
        private readonly DateTimeToStringValueConverter DateTimeToStringConverter = new DateTimeToStringValueConverter();

        public string ConvertDateToString(DateTime date)
        {
            return DateTimeToStringConverter.Convert(date, "g");
        }

        public DateTime ConvertStringToDate(string dateString)
        {
            return DateTimeToStringConverter.ConvertBack(dateString, "g");
        }
    }
}
```

## Creating your own custom value converters

If you want to take advantage of what goes into a value converter, you can build your own using the `MADE.Data.Converters.IValueConverter<TFrom, TTo>` interface which provides the signatures for the `Convert` and `ConvertBack` methods.

These can be used to convert any type to another. Whatever data conversion you think you may need, you'll be able to build out a value converter to satisfy that need for your project.

If there is a common value converter you think is missing from MADE.NET, [raise a tracking item on GitHub](https://github.com/MADE-Apps/MADE.NET/issues/new/choose) and we'll get it implemented.

## DateTime extensions

The `MADE.Data.Converters.Extensions.DateTimeExtensions` class provides a comprehensive set of extensions for working with `DateTime` values:

- `ToCurrentAge()` - Calculates an age in years from a date to today.
- `ToDaySuffix()` - Returns the day suffix (st, nd, rd, th) for a date.
- `ToNearestHour()` - Rounds a date to the nearest hour.
- `StartOfDay()` / `EndOfDay()` - Gets the start or end of the day.
- `StartOfWeek()` / `EndOfWeek()` - Gets the start or end of the week.
- `StartOfMonth()` / `EndOfMonth()` - Gets the start or end of the month.
- `StartOfYear()` / `EndOfYear()` - Gets the start or end of the year.
- `SetTime()` - Overrides the time part of a `DateTime` value (multiple overloads).

## String extensions

The `MADE.Data.Converters.Extensions.StringExtensions` class provides extensions for manipulating `String` values:

- `ToTitleCase()` - Converts a string to title case.
- `ToDefaultCase()` - Converts a string to default (lower) case.
- `Truncate()` - Truncates a string to a specified length.
- `ToBase64()` / `FromBase64()` - Converts to and from Base64 encoding.
- `ToMemoryStreamAsync()` - Converts a string to a `MemoryStream`.
- `ToInt()` / `ToNullableInt()` - Parses a string to an integer.
- `ToFloat()` / `ToNullableFloat()` - Parses a string to a float.
- `ToDouble()` / `ToNullableDouble()` - Parses a string to a double.
- `ToBoolean()` - Parses a string to a boolean.

## Boolean extensions

The `MADE.Data.Converters.Extensions.BooleanExtensions` class provides the `ToFormattedString` extension for formatting `bool` values to custom string representations.

```csharp
bool isActive = true;
string result = isActive.ToFormattedString("Active", "Inactive"); // "Active"
```

## Math extensions

The `MADE.Data.Converters.Extensions.MathExtensions` class provides extensions for common mathematic expressions including `ToRadians` to convert a degrees value to radians.

## Length extensions

The `MADE.Data.Converters.Extensions.LengthExtensions` class provides extensions for converting length values:

- `ToMeters()` - Converts a value from miles to meters.
- `ToMiles()` - Converts a value from meters to miles.
