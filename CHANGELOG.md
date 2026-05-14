# Changelog

## v3.0.0

### Breaking Changes

#### Target Framework Updates

- All libraries now target `net8.0` and `net10.0`. Previous target frameworks have been removed.

#### Newtonsoft.Json Replaced with System.Text.Json

The following libraries have migrated from `Newtonsoft.Json` to `System.Text.Json`. All public APIs that previously accepted `Newtonsoft.Json.JsonSerializerSettings` now accept `System.Text.Json.JsonSerializerOptions`.

**MADE.Web.Mvc**

- `JsonResult` constructor parameter type changed from `JsonSerializerSettings` to `JsonSerializerOptions`.
- `JsonResult.SerializerOptions` property type changed from `JsonSerializerSettings` to `JsonSerializerOptions`.
- `ControllerBaseExtensions.Json()` parameter type changed from `JsonSerializerSettings` to `JsonSerializerOptions`.

**MADE.Web**

- All `HttpResponseExtensions.WriteJsonAsync()` overloads that accepted `JsonSerializerSettings` now accept `JsonSerializerOptions`.

**MADE.Networking**

- Internal serialization switched from `Newtonsoft.Json` to `System.Text.Json`. All deserialization uses `PropertyNameCaseInsensitive = true` to maintain behavioral compatibility.
- No public API signature changes.

**MADE.Data.Serialization**

- `JsonTypeMigrationSerializationBinder` has been removed. Use `JsonTypeMigrationConverter` instead (see migration guide below).

### Removed Dependencies

| Library | Removed Dependency | Replacement |
| --- | --- | --- |
| MADE.Networking | `Newtonsoft.Json` | `System.Text.Json` (built-in) |
| MADE.Web | `Newtonsoft.Json` | `System.Text.Json` (built-in) |
| MADE.Web.Mvc | `Newtonsoft.Json` | `System.Text.Json` (built-in) |
| MADE.Data.Serialization | `Newtonsoft.Json` | `System.Text.Json` (built-in) |
| MADE.Data.EFCore | `Z.EntityFramework.Plus.EFCore` | Custom implementation using Expression trees |

### Migration Guide

#### Newtonsoft.Json to System.Text.Json

Replace `using Newtonsoft.Json` with `using System.Text.Json` and update any `JsonSerializerSettings` references to `JsonSerializerOptions`.

```csharp
// Before (v2)
using Newtonsoft.Json;

var result = controller.Json(value, HttpStatusCode.OK, new JsonSerializerSettings
{
    NullValueHandling = NullValueHandling.Ignore
});

// After (v3)
using System.Text.Json;

var result = controller.Json(value, HttpStatusCode.OK, new JsonSerializerOptions
{
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
});
```

#### JsonTypeMigrationSerializationBinder to JsonTypeMigrationConverter

The Newtonsoft.Json-based `JsonTypeMigrationSerializationBinder` in the `MADE.Data.Serialization.Json.Binders` namespace has been replaced with `JsonTypeMigrationConverter` in the `MADE.Data.Serialization.Json.Converters` namespace.

```csharp
// Before (v2)
using MADE.Data.Serialization.Json.Binders;

var binder = new JsonTypeMigrationSerializationBinder();
binder.AddTypeMigration(new JsonTypeMigration("OldAssembly", "OldNamespace.OldType", typeof(NewType)));

var settings = new JsonSerializerSettings
{
    TypeNameHandling = TypeNameHandling.All,
    SerializationBinder = binder
};
var result = JsonConvert.DeserializeObject<object>(json, settings);

// After (v3)
using MADE.Data.Serialization.Json.Converters;

var converter = new JsonTypeMigrationConverter();
converter.AddTypeMigration(new JsonTypeMigration("OldAssembly", "OldNamespace.OldType", typeof(NewType)));

var options = new JsonSerializerOptions();
options.Converters.Add(converter);
var result = JsonSerializer.Deserialize<object>(json, options);
```

#### IEventLogger Methods Changed from void to Task

All 15 methods on `IEventLogger` (`WriteDebug`, `WriteInfo`, `WriteWarning`, `WriteError`, `WriteCritical` and their overloads) now return `Task` instead of `void`. Implementations must be updated accordingly.

#### JsonTypeMigrationConverter Simplified

- `AddTypeMigrationAsync` has been renamed to `AddTypeMigration` and is now synchronous (uses `lock` instead of `SemaphoreSlim`).

### Code Quality Improvements

- **File-scoped namespaces**: All source files converted to file-scoped namespace declarations.
- **ConfigureAwait(false)**: Added to all `await` expressions in library code (52 locations across 17 files) to prevent deadlocks in synchronization-context-bound environments.
- **ArgumentNullException.ThrowIfNull**: Replaced manual null-check-and-throw patterns with `ArgumentNullException.ThrowIfNull()` (22 locations across 7 files).
- **Nullable reference type annotations**: Added `?` annotations to parameters, return types, fields, and properties that accept or return `null` (17 fixes across 10 files).
- **Async correctness**: `FileEventLogger` and `AppDiagnostics` rewritten for proper async patterns, removing `async void` methods.
- **Comprehensive .editorconfig**: Added modern .NET analysis rules including CA2007, CA1822, CA1849, and async naming conventions.
