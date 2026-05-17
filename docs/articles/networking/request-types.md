---
uid: networking-request-types
title: Request types
---

# Request types

`MADE.Networking` provides typed request classes for each HTTP method. Each request type handles serialization and deserialization so you don't need to manually call `JsonSerializer` at every call site.

## JSON request types

All JSON request types use `System.Text.Json` with `PropertyNameCaseInsensitive = true` for deserialization.

### JsonGetNetworkRequest

Makes an HTTP GET request and deserializes the JSON response:

```csharp
var request = requestFactory.Get("https://api.example.com/users/123");
var user = await request.ExecuteAsync<User>();
```

### JsonPostNetworkRequest

Makes an HTTP POST request with a JSON body and deserializes the response:

```csharp
var request = requestFactory.Post(
    "https://api.example.com/users",
    JsonSerializer.Serialize(newUser));
var created = await request.ExecuteAsync<User>();
```

### JsonPutNetworkRequest

Makes an HTTP PUT request with a JSON body:

```csharp
var request = requestFactory.Put(
    "https://api.example.com/users/123",
    JsonSerializer.Serialize(updatedUser));
await request.ExecuteAsync<bool>();
```

### JsonPatchNetworkRequest

Makes an HTTP PATCH request with a JSON body:

```csharp
var request = requestFactory.Patch(
    "https://api.example.com/users/123",
    JsonSerializer.Serialize(patchData));
await request.ExecuteAsync<User>();
```

### JsonDeleteNetworkRequest

Makes an HTTP DELETE request and optionally deserializes the response:

```csharp
var request = requestFactory.Delete("https://api.example.com/users/123");
await request.ExecuteAsync<bool>();
```

## Stream request types

### StreamGetNetworkRequest

Makes an HTTP GET request and returns the response as a `Stream`. Useful for downloading files or large payloads:

```csharp
var request = new StreamGetNetworkRequest(client, "https://api.example.com/files/report.pdf");
var stream = await request.ExecuteAsync<Stream>();
```

## Request identity

Every `NetworkRequest` instance has a `Guid` identifier. This is used by the `NetworkRequestManager` to track and update pending requests. If you create a new request with the same ID, it replaces the pending one in the queue. See [Request manager](request-manager.md).
