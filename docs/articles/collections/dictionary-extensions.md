---
uid: collections-dictionary-extensions
title: Dictionary extensions
---

# Dictionary extensions

The `MADE.Collections.DictionaryExtensions` class provides two commonly needed operations for `Dictionary<TKey, TValue>` that the standard library doesn't include.

## AddOrUpdate

Adds a key-value pair to the dictionary if the key doesn't exist, or updates the value if it does. This replaces the pattern of checking `ContainsKey` before deciding whether to add or update:

```csharp
using MADE.Collections;

var settings = new Dictionary<string, string>();

// Adds the key
settings.AddOrUpdate("Theme", "Dark");

// Updates the existing key
settings.AddOrUpdate("Theme", "Light");
```

This is useful for caching scenarios, configuration builders, or any situation where you're accumulating key-value pairs and duplicates should overwrite rather than throw.

## GetValueOrDefault

Gets a value from the dictionary by key, or returns a specified default if the key doesn't exist:

```csharp
using MADE.Collections;

var config = new Dictionary<string, string>
{
    ["DatabaseTimeout"] = "30",
};

var timeout = config.GetValueOrDefault("DatabaseTimeout", "60"); // "30"
var retries = config.GetValueOrDefault("MaxRetries", "3");       // "3" (default)
```

> **Note:** .NET 8+ includes `CollectionExtensions.GetValueOrDefault` in the base class library. If you're targeting `net8.0` or later exclusively, you can use the built-in version. The MADE extension remains available for compatibility and for scenarios where you want a non-null default value.
