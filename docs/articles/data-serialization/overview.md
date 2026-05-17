---
uid: data-serialization-overview
title: Data Serialization
---

# Data Serialization

When you rename a class, move it to a different namespace, or restructure your assemblies, any JSON data that was serialized with .NET type metadata (`$type` properties) will fail to deserialize. The old type name no longer resolves, and `System.Text.Json` throws an exception.

`MADE.Data.Serialization` provides a `JsonTypeMigrationConverter` that maps old type names to their new types, letting you evolve your codebase without breaking existing serialized data.

```bash
dotnet add package MADE.Data.Serialization
```

## When to use this package

- You have serialized JSON data in a database, message queue, or file system that includes `$type` metadata.
- You've renamed classes, moved them to different namespaces, or refactored assemblies.
- You need to deserialize historical data that references types that no longer exist under their original names.

## Using JsonTypeMigrationConverter

Register type migrations that map old type identifiers to their new types, then add the converter to your `JsonSerializerOptions`:

```csharp
using System.Text.Json;
using MADE.Data.Serialization.Json;
using MADE.Data.Serialization.Json.Converters;

// Create the converter with type migrations
var converter = new JsonTypeMigrationConverter(
    new JsonTypeMigration(typeof(OldOrder), typeof(Order)),
    new JsonTypeMigration("MyApp.Legacy", "MyApp.Legacy.CustomerRecord", typeof(Customer))
);

var options = new JsonSerializerOptions();
options.Converters.Add(converter);

// Deserialize data that references old type names
var result = JsonSerializer.Deserialize<object>(historicalJson, options);
```

### Adding migrations dynamically

You can add migrations after construction, which is useful when you discover old type names at runtime or want to register migrations from configuration:

```csharp
var converter = new JsonTypeMigrationConverter();
converter.AddTypeMigration(new JsonTypeMigration(typeof(LegacyOrder), typeof(Order)));
converter.AddTypeMigration(new JsonTypeMigration(
    "OldAssembly", "OldNamespace.OldType", typeof(NewType)));
```

### Migration overloads

`JsonTypeMigration` supports two constructor patterns:

| Constructor | Use case |
| --- | --- |
| `JsonTypeMigration(Type fromType, Type toType)` | When the old type still exists in your codebase (e.g., during a gradual migration). |
| `JsonTypeMigration(string fromAssemblyName, string fromTypeName, Type toType)` | When the old type has been completely removed and you only have its original assembly and type name. |

## Practical example: database migration

A common scenario is migrating serialized event data in a database. Suppose you've refactored your domain events from a flat namespace to a structured one:

```csharp
// Old types (no longer exist):
//   MyApp.Events.OrderCreated
//   MyApp.Events.OrderShipped

// New types:
//   MyApp.Domain.Orders.Events.OrderCreated
//   MyApp.Domain.Orders.Events.OrderShipped

var converter = new JsonTypeMigrationConverter(
    new JsonTypeMigration("MyApp", "MyApp.Events.OrderCreated",
        typeof(MyApp.Domain.Orders.Events.OrderCreated)),
    new JsonTypeMigration("MyApp", "MyApp.Events.OrderShipped",
        typeof(MyApp.Domain.Orders.Events.OrderShipped))
);

var options = new JsonSerializerOptions();
options.Converters.Add(converter);

// Now you can deserialize historical events stored with old type names
foreach (var row in await db.EventStore.ToListAsync())
{
    var domainEvent = JsonSerializer.Deserialize<IDomainEvent>(row.Payload, options);
    await Process(domainEvent);
}
```

## Best practices

- **Register all known type migrations upfront** rather than adding them on-the-fly. This makes it easy to audit which migrations exist and ensures consistency.
- **Use the string-based constructor** for types that have been completely removed from your codebase. This avoids keeping dead types around just for migration purposes.
- **Consider a data migration script** for high-volume scenarios. Rather than paying the migration cost on every read, update the serialized data in place so future reads are clean.
