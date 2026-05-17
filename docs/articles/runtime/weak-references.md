---
uid: runtime-weak-references
title: Weak references
---

# Weak references

In UI applications and event-driven systems, storing delegate callbacks can accidentally keep objects alive long after they should have been garbage collected. A view model that registers a callback with a long-lived service stays in memory as long as that service holds a reference to the callback, even if the view model's view has been navigated away from.

`WeakReferenceCallback` wraps a delegate in a `WeakReference`, allowing the garbage collector to reclaim the target object while the callback registration still exists.

## How it works

```csharp
using MADE.Runtime;

// Store a callback without preventing garbage collection of the target
var callback = new WeakReferenceCallback(myAction, typeof(MyResponse));
```

When the callback is invoked:
- If the target object is still alive, the delegate executes normally.
- If the target object has been garbage collected, the invocation is silently skipped.

## Practical example: network request callbacks

The `NetworkRequestManager` in `MADE.Networking` uses `WeakReferenceCallback` internally to store success and error callbacks:

```csharp
public void AddOrUpdate<TRequest, TResponse, TErrorResponse>(
    TRequest request,
    Action<TResponse> successCallback,
    Action<TErrorResponse> errorCallback)
    where TRequest : NetworkRequest
{
    var weakSuccess = new WeakReferenceCallback(successCallback, typeof(TResponse));
    var weakError = new WeakReferenceCallback(errorCallback, typeof(TErrorResponse));

    // Store weak references instead of strong references to the callbacks
    var requestCallback = new NetworkRequestCallback(request, weakSuccess, weakError);
    CurrentQueue.AddOrUpdate(request.Identifier.ToString(), requestCallback, (s, c) => requestCallback);
}
```

If the view model that registered the callback is disposed before the network request completes, the callback reference doesn't prevent garbage collection. The request still completes, but the callback is skipped.

## Reflection helpers

The `MADE.Runtime.Extensions.ReflectionExtensions` class provides `GetPropertyNames` for retrieving the public property names of an object:

```csharp
using MADE.Runtime.Extensions;

var user = new User { FirstName = "James", LastName = "Croft" };
IEnumerable<string> names = user.GetPropertyNames();
// ["FirstName", "LastName"]
```

## When to use WeakReferenceCallback

- **Use it** when storing callbacks in long-lived objects (managers, registries, caches) where the callback source may be short-lived.
- **Don't use it** for callbacks that must always execute (e.g., critical cleanup logic). If the target is collected, the callback won't run.
