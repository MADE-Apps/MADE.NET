---
uid: threading-async-lazy
title: AsyncLazy
---

# AsyncLazy

`Lazy<T>` is the standard way to defer initialization until first use, but it doesn't support async factory methods. If your initialization involves I/O - loading configuration from a file, fetching a token from an API, warming up a cache - you need an async equivalent.

`AsyncLazy<T>` provides thread-safe lazy initialization with an async factory. The value is computed once on first access and cached for all subsequent uses.

## Basic usage

```csharp
using MADE.Threading;

private readonly AsyncLazy<Configuration> config = new(async () =>
{
    return await LoadConfigurationFromFileAsync();
});

public async Task DoWorkAsync()
{
    var configuration = await config;
    // Use configuration - already loaded and cached
}
```

The factory runs only on the first `await`. All subsequent awaits return the cached value immediately.

## Checking initialization state

Use `IsValueCreated` to check whether the value has been computed without triggering initialization:

```csharp
if (config.IsValueCreated)
{
    // Value is already loaded
}
```

## Explicit access

If you prefer an explicit method call over awaiting the object directly, use `GetValueAsync()`:

```csharp
var configuration = await config.GetValueAsync();
```

## Common use cases

### Caching an expensive computation

```csharp
private readonly AsyncLazy<IReadOnlyList<Country>> countries = new(async () =>
{
    return await geoService.GetAllCountriesAsync();
});
```

### Lazy connection initialization

```csharp
private readonly AsyncLazy<IDbConnection> connection = new(async () =>
{
    var conn = new SqlConnection(connectionString);
    await conn.OpenAsync();
    return conn;
});
```

### One-time token acquisition

```csharp
private readonly AsyncLazy<string> accessToken = new(async () =>
{
    var result = await authClient.AcquireTokenAsync(scopes);
    return result.AccessToken;
});
```

## Thread safety

`AsyncLazy<T>` is thread-safe. If multiple threads await the value simultaneously before it's been created, only one factory invocation occurs. All threads receive the same cached result.
