---
uid: package-data-serialization
title: Using the Data Serialization package
---

# Using the Data Serialization package

The Data Serialization package provides a collection of helpers and extensions for data serialization in different types, e.g. JSON.

## Handling type changes in JSON objects with JsonTypeMigrationConverter

When working with serialized JSON data that includes .NET type information (e.g., a `$type` metadata property), type refactoring or restructuring in your codebase can cause deserialization to fail.

The `MADE.Data.Serialization.Json.Converters.JsonTypeMigrationConverter` is a `System.Text.Json` converter that reads `$type` metadata from JSON objects and resolves the target type using registered type migrations. It is designed to deserialize JSON that was previously serialized with type metadata (e.g., from Newtonsoft.Json's `TypeNameHandling.All`).

Here's how to set up your application for migrating JSON objects from one type to another.

```csharp
namespace App.Migrations
{
    using System.Text.Json;
    using MADE.Data.Serialization.Json;
    using MADE.Data.Serialization.Json.Converters;

    public class JsonMigrationSerializer
    {
        private readonly JsonSerializerOptions options;

        public JsonMigrationSerializer()
        {
            var converter = new JsonTypeMigrationConverter(
                new JsonTypeMigration(typeof(OldType), typeof(NewType)),
                new JsonTypeMigration("App.Migrations", "App.Migrations.Data.OldDataType", typeof(NewType))
            );

            this.options = new JsonSerializerOptions();
            this.options.Converters.Add(converter);
        }

        public T? Deserialize<T>(string serializedJson)
        {
            return JsonSerializer.Deserialize<T>(serializedJson, this.options);
        }
    }
}
```

### Adding migrations dynamically

You can also add migrations after construction using the `AddTypeMigration` method:

```csharp
var converter = new JsonTypeMigrationConverter();
converter.AddTypeMigration(new JsonTypeMigration(typeof(LegacyOrder), typeof(Order)));
```

The `JsonTypeMigration` class supports two constructor overloads:

- `JsonTypeMigration(Type fromType, Type toType)` - Migrates from one known type to another.
- `JsonTypeMigration(string fromAssemblyName, string fromTypeName, Type toType)` - Migrates from a type that may no longer exist in the codebase, identified by its original assembly and type name.
