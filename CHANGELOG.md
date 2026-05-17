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
- `AddTypeMigrationAsync` has been renamed to `AddTypeMigration` and is now synchronous (uses `lock` instead of `SemaphoreSlim`).

#### IEventLogger Methods Changed from void to Task

All 15 methods on `IEventLogger` (`WriteDebug`, `WriteInfo`, `WriteWarning`, `WriteError`, `WriteCritical` and their overloads) now return `Task` instead of `void`. Implementations must be updated accordingly.

#### Timer Moved from MADE.Runtime to MADE.Threading

`Timer` and `ITimer` have moved from the `MADE.Runtime` namespace to `MADE.Threading`. Update `using` statements accordingly.

#### Removed Windows UWP Converters

The obsolete Windows UWP-specific partial classes `BooleanToStringValueConverter.Windows.cs` and `DateTimeToStringValueConverter.Windows.cs` (guarded by `#if WINDOWS_UWP`) have been removed.

### Removed Dependencies

| Library | Removed Dependency | Replacement |
| --- | --- | --- |
| MADE.Networking | `Newtonsoft.Json` | `System.Text.Json` (built-in) |
| MADE.Web | `Newtonsoft.Json` | `System.Text.Json` (built-in) |
| MADE.Web.Mvc | `Newtonsoft.Json` | `System.Text.Json` (built-in) |
| MADE.Data.Serialization | `Newtonsoft.Json` | `System.Text.Json` (built-in) |
| MADE.Data.EFCore | `Z.EntityFramework.Plus.EFCore` | Custom implementation using Expression trees |

### New Features

#### MADE.Threading

- **`AdaptiveSemaphore`** - A semaphore that dynamically adjusts its concurrency limit at runtime. Supports `TryShrinkAsync()` to reduce and `TryGrow()` to increase the permit count, bounded by configurable minimum and maximum values.
- **`AsyncLazy<T>`** - Thread-safe lazy initialization for async factories. Supports `await` directly via `GetAwaiter()` or explicitly via `GetValueAsync()`.
- **`Debouncer`** - Delays execution of an action until a configurable quiet period (default 300ms) has elapsed since the last invocation. Supports both sync (`Debounce`) and async (`DebounceAsync`) actions.
- **`Throttler`** - Limits execution of an action to at most once per configurable interval (default 300ms). Supports both sync (`Throttle`) and async (`ThrottleAsync`) actions.

#### MADE.Networking

- **`INetworkRequestFactory` / `NetworkRequestFactory`** - Factory for creating typed network requests (`Get`, `Post`, `Put`, `Patch`, `Delete`, `GetStream`, `PostMultipart`). Integrates with `IHttpClientFactory` and supports named clients via `WithClient(string clientName)`.
- **`MultipartFormDataPostNetworkRequest`** - Network request for sending multipart form data, with fluent `AddStringContent`, `AddStreamContent`, and `AddByteArrayContent` methods.
- **`RetryDelegatingHandler`** - An `HttpMessageHandler` that retries failed requests with exponential backoff. Configurable max retries (default 3) and initial delay.
- **`ServiceCollectionExtensions.AddNetworkRequestFactory()`** - DI registration for `INetworkRequestFactory` with optional named `HttpClient` configuration.

#### MADE.Data.Converters

- **`DateTimeToUnixTimestampValueConverter`** - Converts between `DateTime` and Unix timestamps (`long`).
- **`StringToEnumValueConverter<TEnum>`** - Generic converter between `string` and any `Enum` type, with configurable case sensitivity.
- **`FileSizeExtensions.ToHumanReadableFileSize()`** - Extension methods on `long` and `double` to format byte counts as human-readable strings (e.g., "1.5 MB").
- **`TimeSpanExtensions`** - `ToHumanReadableString()` for friendly duration formatting and `TotalWeeks()` for week count.

#### MADE.Data.EFCore

- **`ISoftDeletable`** - Interface for entities supporting soft delete, with `IsDeleted` and `DeletedDate` properties.
- **`IAuditableEntity`** - Interface for entities tracking `CreatedBy` and `UpdatedBy`.
- **`SoftDeleteExtensions`** - Extension methods for applying global soft-delete query filters (`ApplySoftDeleteFilter`), marking entities as soft-deleted (`SoftDelete`), restoring them (`Restore`), and intercepting deletions to convert them to soft deletes (`InterceptSoftDeletions`).
- **`QueryableExtensions.Page()`** - Pagination extension using `Skip`/`Take`.
- **`QueryableExtensions.OrderBy()`** - Dynamic ordering by property name using Expression trees, replacing `Z.EntityFramework.Plus.EFCore`.

#### MADE.Data.Validation

- **`IAsyncValidator`** - Async counterpart to `IValidator`, with `ValidateAsync(object value, CancellationToken)`.
- **`AsyncValidatorCollection`** - Collection of `IAsyncValidator` instances with aggregate validation via `ValidateAsync()`, exposing `IsInvalid`, `IsDirty`, and `FeedbackMessages`.

#### MADE.Web.Mvc

- **`ForbiddenObjectResult`** - An `ObjectResult` that returns HTTP 403 Forbidden with a response body.

#### MADE.Testing (New Library)

A new test-framework-agnostic assertion library with fluent `Should*` extension methods:

- **`BooleanAssertExtensions`** - `ShouldBeTrue()`, `ShouldBeFalse()`
- **`ObjectAssertExtensions`** - `ShouldBeNull()`, `ShouldNotBeNull()`
- **`StringAssertExtensions`** - `ShouldContain()`, `ShouldNotContain()`, `ShouldStartWith()`, `ShouldEndWith()`
- **`ComparableAssertExtensions`** - `ShouldBeGreaterThan()`, `ShouldBeGreaterThanOrEqualTo()`, `ShouldBeLessThan()`, `ShouldBeLessThanOrEqualTo()`
- **`ExceptionAssertExtensions`** - `ShouldThrow<T>()`, `ShouldThrowAsync<T>()`, `ShouldNotThrow()`, `ShouldNotThrowAsync()`

### Bug Fixes

- **Timer.Start dueTime calculation** - Fixed `Timer.Start(TimeSpan dueTime)` using `dueTime.Milliseconds` (the milliseconds component only, 0-999) instead of `(int)Math.Ceiling(dueTime.TotalMilliseconds)` (the full duration). This caused timers with due times of 1 second or more to use incorrect delays.
- **Timer.Start not updating DueTime property** - `Start(TimeSpan dueTime)` and `Start(int dueTime)` overloads now correctly update the `DueTime` property before starting the timer.

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

#### Timer Namespace Change

```csharp
// Before (v2)
using MADE.Runtime;

// After (v3)
using MADE.Threading;
```

### Code Quality Improvements

- **File-scoped namespaces**: All source files converted to file-scoped namespace declarations.
- **ConfigureAwait(false)**: Added to all `await` expressions in library code to prevent deadlocks in synchronization-context-bound environments.
- **ArgumentNullException.ThrowIfNull**: Replaced manual null-check-and-throw patterns with `ArgumentNullException.ThrowIfNull()`.
- **Nullable reference type annotations**: Added `?` annotations to parameters, return types, fields, and properties that accept or return `null`.
- **Async correctness**: `FileEventLogger` and `AppDiagnostics` rewritten for proper async patterns, removing `async void` methods.
- **Comprehensive .editorconfig**: Added modern .NET analysis rules including CA2007, CA1822, CA1849, and async naming conventions.
