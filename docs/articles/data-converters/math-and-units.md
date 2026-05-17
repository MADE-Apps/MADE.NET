---
uid: data-converters-math-and-units
title: Math and units
---

# Math and units

`MADE.Data.Converters` includes several extension classes for unit conversion, mathematical operations, and human-readable formatting. These are small utilities that come up frequently enough to justify having them available as extension methods.

## File size formatting

The `FileSizeExtensions` class converts byte counts to human-readable file size strings. This is useful for upload progress indicators, storage dashboards, or any UI that displays file sizes.

```csharp
using MADE.Data.Converters.Extensions;

long bytes = 1_572_864;
string size = bytes.ToHumanReadableFileSize(); // "1.50 MB"

long small = 256;
string smallSize = small.ToHumanReadableFileSize(); // "256 B"

long large = 2_147_483_648;
string largeSize = large.ToHumanReadableFileSize(); // "2.00 GB"
```

## TimeSpan utilities

The `TimeSpanExtensions` class provides two extensions for working with `TimeSpan` values:

### Human-readable duration strings

```csharp
using MADE.Data.Converters.Extensions;

var duration = TimeSpan.FromHours(2.5);
string readable = duration.ToHumanReadableString(); // "2 hours 30 minutes"
```

### Total weeks

```csharp
var span = TimeSpan.FromDays(21);
int weeks = span.TotalWeeks(); // 3
```

## Boolean formatting

The `BooleanExtensions` class provides `ToFormattedString` for converting boolean values to custom string representations:

```csharp
using MADE.Data.Converters.Extensions;

bool isActive = true;
string status = isActive.ToFormattedString("Active", "Inactive"); // "Active"

bool hasAccess = false;
string access = hasAccess.ToFormattedString("Granted", "Denied"); // "Denied"
```

## Angle conversions

The `MathExtensions` class provides conversions between degrees and radians:

```csharp
using MADE.Data.Converters.Extensions;

double radians = 180.0.ToRadians(); // 3.14159...
double degrees = Math.PI.ToDegrees(); // 180.0
```

## Length conversions

The `LengthExtensions` class provides conversions between common length units, with meters as the base unit:

```csharp
using MADE.Data.Converters.Extensions;

// Miles and meters
double meters = 1.0.ToMeters();   // 1609.344 (1 mile in meters)
double miles = 1609.344.ToMiles(); // 1.0

// Kilometers
double km = 1000.0.ToKilometers();       // 1.0
double m = 1.0.KilometersToMeters();     // 1000.0

// Feet and inches
double feet = 1.0.ToFeet();             // 3.28084...
double inches = 1.0.ToInches();         // 39.3701...
double mFromFeet = 1.0.FeetToMeters();  // 0.3048
double mFromIn = 1.0.InchesToMeters();  // 0.0254
```
