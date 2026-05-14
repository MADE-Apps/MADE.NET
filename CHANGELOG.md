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
await converter.AddTypeMigrationAsync(new JsonTypeMigration("OldAssembly", "OldNamespace.OldType", typeof(NewType)));

var options = new JsonSerializerOptions();
options.Converters.Add(converter);
var result = JsonSerializer.Deserialize<object>(json, options);
```

Note: `AddTypeMigration` has been renamed to `AddTypeMigrationAsync` and is now asynchronous.
