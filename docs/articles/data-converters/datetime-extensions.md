---
uid: data-converters-datetime-extensions
title: DateTime extensions
---

# DateTime extensions

The `MADE.Data.Converters.Extensions.DateTimeExtensions` class provides extension methods for common date and time calculations. These handle the edge cases that make date arithmetic tedious - month boundaries, week starts, age calculation, and time rounding.

## Calculating age with ToCurrentAge

Calculates a person's current age in years from a birth date:

```csharp
using MADE.Data.Converters.Extensions;

DateTime birthDate = new(1990, 6, 15);
int age = birthDate.ToCurrentAge(); // Correctly handles leap years and month boundaries
```

This handles the common pitfall of naive age calculation (dividing days by 365.25) by properly accounting for whether the birthday has occurred yet this year.

## Period boundaries

These extensions are useful for reporting, filtering, and analytics where you need to query data within a specific time period.

### Day boundaries

```csharp
DateTime now = DateTime.Now;

DateTime startOfDay = now.StartOfDay();  // Today at 00:00:00
DateTime endOfDay = now.EndOfDay();      // Today at 23:59:59.999
```

### Week boundaries

```csharp
DateTime startOfWeek = now.StartOfWeek();  // Most recent Monday at 00:00:00
DateTime endOfWeek = now.EndOfWeek();      // Coming Sunday at 23:59:59.999
```

### Month boundaries

```csharp
DateTime startOfMonth = now.StartOfMonth();  // 1st of current month at 00:00:00
DateTime endOfMonth = now.EndOfMonth();      // Last day of current month at 23:59:59.999
```

### Year boundaries

```csharp
DateTime startOfYear = now.StartOfYear();  // January 1st at 00:00:00
DateTime endOfYear = now.EndOfYear();      // December 31st at 23:59:59.999
```

## Rounding to the nearest hour

```csharp
var time = new DateTime(2026, 5, 17, 14, 42, 0);
DateTime rounded = time.ToNearestHour(); // 2026-05-17 15:00:00
```

## Day suffix

Returns the ordinal suffix for the day of the month:

```csharp
DateTime date = new(2026, 5, 1);
string suffix = date.ToDaySuffix(); // "st"
// Useful for formatting: "May 1st", "May 2nd", "May 3rd", "May 4th"
```

## Setting the time component

Overrides the time portion of a `DateTime` while keeping the date:

```csharp
DateTime date = DateTime.Today;

// Set to specific time
DateTime withTime = date.SetTime(14, 30);        // 2:30 PM
DateTime precise = date.SetTime(14, 30, 45);     // 2:30:45 PM
DateTime exact = date.SetTime(14, 30, 45, 500);  // 2:30:45.500 PM
```

## Practical example: building a date range query

These extensions compose naturally for common data access patterns:

```csharp
public async Task<IEnumerable<Order>> GetThisWeeksOrdersAsync()
{
    var weekStart = DateTime.UtcNow.StartOfWeek();
    var weekEnd = DateTime.UtcNow.EndOfWeek();

    return await dbContext.Orders
        .Where(o => o.CreatedDate >= weekStart && o.CreatedDate <= weekEnd)
        .ToListAsync();
}
```
