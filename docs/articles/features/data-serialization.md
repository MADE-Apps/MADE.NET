---
uid: package-data-serialization
title: Using the Data Serialization package
---

# Using the Data Serialization package

The Data Serialization package provides a collection of helpers and extensions for data serialization in different types, e.g. JSON.

## Handling type changes in JSON objects serialized with JSON.NET with TypeNameHandling set to All

There are many ways to use JSON.NET in your applications to serialize and deserialize data. This includes the ability to set the `TypeNameHandling` property to `All` include your .NET type information within your serialized data.

This can come with challenges when you want to use the same data in different solutions, or when you want to perform refactors or data restructures in your codebase.

The `JsonTypeMigrationSerializationBinder` class provides a way to handle type changes in JSON objects serialized with JSON.NET, migrating from one type to another (whether known within your codebase or not).

Here's how to setup your application for migrating JSON objects from one type to another.

```csharp
namespace App.Migrations
{
    using MADE.Data.Serialization.Json;
    using MADE.Data.Serialization.Json.Binders;

    public class JsonSerializer
    {
        public JsonSerializer()
        {
            JsonSerializerSettings.Default.TypeNameHandling = TypeNameHandling.All;
            JsonSerializerSettings.Default.Binder = new JsonTypeMigrationSerializationBinder(
              new JsonTypeMigration(typeof(OldType), typeof(NewType)),
              new JsonTypeMigration("App.Migrations", "App.Migrations.Data.OldDataType", typeof(NewType))
            );
        }

        public T Deserialize<T>(string serializedJson) 
        {
            return JsonConvert.DeserializeObject<T>(serializedJson);
        }
    }
}
```
