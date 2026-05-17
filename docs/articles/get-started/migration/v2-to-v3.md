---
uid: get-started-migration-v2-to-v3
title: Migrating from v2 to v3
---

# Migrating from v2 to v3

This guide covers all breaking changes in MADE.NET v3.0 and provides step-by-step instructions for updating your code.

## Target framework changes

All libraries now target `net8.0` and `net10.0`. If your project targets an earlier framework, you must update your `TargetFramework` before upgrading.

```xml
<!-- Before -->
<TargetFramework>net6.0</TargetFramework>

<!-- After -->
<TargetFramework>net8.0</TargetFramework>
```

## Newtonsoft.Json replaced with System.Text.Json

The following libraries have migrated from `Newtonsoft.Json` to `System.Text.Json`:

| Library | Impact |
| --- | --- |
| MADE.Web.Mvc | Public API signature changes |
| MADE.Web | Public API signature changes |
| MADE.Networking | Internal only, no public API changes |
| MADE.Data.Serialization | Type replaced (see below) |

### Updating MADE.Web.Mvc

Replace `JsonSerializerSettings` with `JsonSerializerOptions` in calls to `Json()` and when constructing `JsonResult`:

```csharp
// Before (v2)
using Newtonsoft.Json;

var result = controller.Json(value, HttpStatusCode.OK, new JsonSerializerSettings
{
    NullValueHandling = NullValueHandling.Ignore
});

// After (v3)
using System.Text.Json;
using System.Text.Json.Serialization;

var result = controller.Json(value, HttpStatusCode.OK, new JsonSerializerOptions
{
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
});
```

### Updating MADE.Web

All `HttpResponseExtensions.WriteJsonAsync()` overloads that accepted `JsonSerializerSettings` now accept `JsonSerializerOptions`. Update the parameter type at each call site.

### Updating MADE.Networking

No code changes required. Internal serialization now uses `System.Text.Json` with `PropertyNameCaseInsensitive = true` to maintain behavioral compatibility.

### Common Newtonsoft.Json to System.Text.Json mappings

| Newtonsoft.Json | System.Text.Json |
| --- | --- |
| `JsonSerializerSettings` | `JsonSerializerOptions` |
| `JsonConvert.SerializeObject()` | `JsonSerializer.Serialize()` |
| `JsonConvert.DeserializeObject<T>()` | `JsonSerializer.Deserialize<T>()` |
| `NullValueHandling.Ignore` | `DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull` |
| `ReferenceLoopHandling.Ignore` | `ReferenceHandler = ReferenceHandler.IgnoreCycles` |

### Removing the Newtonsoft.Json dependency

After updating your code, remove `Newtonsoft.Json` from any projects that only used it through MADE.NET:

```bash
dotnet remove package Newtonsoft.Json
```

## JsonTypeMigrationSerializationBinder replaced

The Newtonsoft.Json-based `JsonTypeMigrationSerializationBinder` in the `MADE.Data.Serialization.Json.Binders` namespace has been removed. Replace it with `JsonTypeMigrationConverter` in the `MADE.Data.Serialization.Json.Converters` namespace.

```csharp
// Before (v2)
using MADE.Data.Serialization.Json.Binders;

var binder = new JsonTypeMigrationSerializationBinder();
binder.AddTypeMigration(new JsonTypeMigration(
    "OldAssembly", "OldNamespace.OldType", typeof(NewType)));

var settings = new JsonSerializerSettings
{
    TypeNameHandling = TypeNameHandling.All,
    SerializationBinder = binder
};
var result = JsonConvert.DeserializeObject<object>(json, settings);

// After (v3)
using MADE.Data.Serialization.Json.Converters;

var converter = new JsonTypeMigrationConverter();
converter.AddTypeMigration(new JsonTypeMigration(
    "OldAssembly", "OldNamespace.OldType", typeof(NewType)));

var options = new JsonSerializerOptions();
options.Converters.Add(converter);
var result = JsonSerializer.Deserialize<object>(json, options);
```

The `AddTypeMigrationAsync` method has also been renamed to `AddTypeMigration` and is now synchronous.

## Z.EntityFramework.Plus.EFCore removed

The `Z.EntityFramework.Plus.EFCore` dependency has been removed from `MADE.Data.EFCore`. If you were using its `OrderBy` or pagination features through MADE, these are now provided by built-in `QueryableExtensions`:

- `query.Page(page, pageSize)` for pagination
- `query.OrderBy(propertyName, descending)` for dynamic ordering

The API is unchanged. If you used `Z.EntityFramework.Plus.EFCore` directly for other features, you can continue referencing it in your own project.

## IEventLogger methods changed from void to Task

All 15 methods on `IEventLogger` now return `Task` instead of `void`:

- `WriteDebug(string)`, `WriteDebug(string, Exception)`, `WriteDebug(Exception)`
- `WriteInfo(string)`, `WriteInfo(string, Exception)`, `WriteInfo(Exception)`
- `WriteWarning(string)`, `WriteWarning(string, Exception)`, `WriteWarning(Exception)`
- `WriteError(string)`, `WriteError(string, Exception)`, `WriteError(Exception)`
- `WriteCritical(string)`, `WriteCritical(string, Exception)`, `WriteCritical(Exception)`

If you implement `IEventLogger`, update your method signatures and `await` calls to the logger:

```csharp
// Before (v2)
public void WriteInfo(string message) { /* ... */ }

logger.WriteInfo("Starting process");

// After (v3)
public Task WriteInfo(string message) { /* ... */ }

await logger.WriteInfo("Starting process");
```

## Timer moved from MADE.Runtime to MADE.Threading

`Timer` and `ITimer` have moved from the `MADE.Runtime` namespace to `MADE.Threading`. Update your `using` statements and package references:

```csharp
// Before (v2)
using MADE.Runtime;

// After (v3)
using MADE.Threading;
```

If your project referenced `MADE.Runtime` solely for `Timer`, replace the package reference:

```xml
<!-- Before -->
<PackageReference Include="MADE.Runtime" />

<!-- After -->
<PackageReference Include="MADE.Threading" />
```

## Windows UWP converters removed

The obsolete Windows UWP partial classes `BooleanToStringValueConverter.Windows.cs` and `DateTimeToStringValueConverter.Windows.cs` have been removed. These were guarded by `#if WINDOWS_UWP` and marked `[Obsolete]` in v2. The cross-platform converter classes remain unchanged.

## Removed dependencies summary

| Library | Removed | Replacement |
| --- | --- | --- |
| MADE.Networking | `Newtonsoft.Json` | `System.Text.Json` (built-in) |
| MADE.Web | `Newtonsoft.Json` | `System.Text.Json` (built-in) |
| MADE.Web.Mvc | `Newtonsoft.Json` | `System.Text.Json` (built-in) |
| MADE.Data.Serialization | `Newtonsoft.Json` | `System.Text.Json` (built-in) |
| MADE.Data.EFCore | `Z.EntityFramework.Plus.EFCore` | Custom `QueryableExtensions` |
