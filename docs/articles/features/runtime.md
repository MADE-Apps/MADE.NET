---
uid: package-runtime
title: Using the Runtime package
---

# Using the Runtime package

The Runtime package provides additional types for .NET to provide extensibility over existing `System` types.

## Improving callback memory management with WeakReferenceCallback

The `MADE.Runtime.WeakReferenceCallback` is a wrapper type for a `WeakReference`. It is capable of taking a delegate action and ensuring that it is available to be garbage collected if the referring object is disposed. 

A good scenario of a use case for this is within the MADE library's `MADE.Networking.Http.NetworkRequestManager` to provide lazy callbacks on completed network requests.

```csharp
public void AddOrUpdate<TRequest, TResponse, TErrorResponse>(
    TRequest request,
    Action<TResponse> successCallback,
    Action<TErrorResponse> errorCallback)
    where TRequest : NetworkRequest
{
    var weakSuccessCallback = new WeakReferenceCallback(successCallback, typeof(TResponse));
    var weakErrorCallback = new WeakReferenceCallback(errorCallback, typeof(TErrorResponse));
    var requestCallback = new NetworkRequestCallback(request, weakSuccessCallback, weakErrorCallback);

    this.CurrentQueue.AddOrUpdate(
        request.Identifier.ToString(),
        requestCallback,
        (s, callback) => requestCallback);
}
```

In the above example, an action can be passed from a requesting object for when a network request is a success or errors. By using a `WeakReferenceCallback` instead of keeping a hold on the action reference, the requesting object can be disposed when it is no longer required. This is useful in application development scenarios, for example, a page view-model.
