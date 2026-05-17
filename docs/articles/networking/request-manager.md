---
uid: networking-request-manager
title: Request manager
---

# Request manager

The `NetworkRequestManager` provides a queue-based approach to managing multiple concurrent HTTP requests. You submit requests with success and error callbacks, and the manager handles execution, response routing, and request deduplication.

This is particularly useful in UI applications where multiple components may be making requests simultaneously, and you want centralized management of the request lifecycle.

## Setting up the request manager

```csharp
using MADE.Networking.Http;

var manager = new NetworkRequestManager();
manager.Start();
```

The `Start()` method begins processing queued requests. Without calling it, requests are queued but not executed.

## Submitting requests with callbacks

Use `AddOrUpdate` to submit a request with a success callback:

```csharp
var request = requestFactory.Get("https://api.example.com/profile");

manager.AddOrUpdate<JsonGetNetworkRequest, Profile>(
    request,
    profile => UpdateProfileUI(profile));
```

You can also provide an error callback:

```csharp
manager.AddOrUpdate<JsonGetNetworkRequest, Profile, ErrorResponse>(
    request,
    profile => UpdateProfileUI(profile),
    error => ShowError(error.Message));
```

## Request deduplication

Every `NetworkRequest` has a `Guid` identifier. If you call `AddOrUpdate` with a request that has the same ID as a pending request, it replaces the pending one. This is useful for scenarios like search-as-you-type where you want to cancel the previous request when new input arrives.

## Memory management with WeakReferenceCallback

The request manager uses `WeakReferenceCallback` internally to hold references to your callbacks. This means that if the object that registered the callback is disposed (e.g., a view model that's no longer active), the callback reference is released and garbage collection can proceed normally.

This is important in UI applications where views and view models are created and destroyed as the user navigates. Without weak references, the request manager would keep disposed objects alive in memory. See [Weak references](../runtime/weak-references.md) for more details.

## Best practices

- **Call `Start()` early** in your application lifecycle, typically during initialization.
- **Use request deduplication** for user-driven requests (search, autocomplete) to avoid processing stale results.
- **Prefer `INetworkRequestFactory` directly** for simple request/response patterns in service classes. The request manager adds value primarily in UI scenarios with concurrent requests and lifecycle concerns.
