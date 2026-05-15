---
uid: package-data-efcore
title: Using the Data EF Core package
---

# Using the Data Entity Framework Core package

The Data Entity Framework Core package provides a collection of helpers, extensions, and converters for applications taking advantage of the `Microsoft.EntityFrameworkCore` library.

## Standardizing your entities with EntityBase

When setting up your entities, there are some common standard properties you'll usually want to include in most circumstances.

These are:

- An identifier
- A date the entity was created
- A date the entity was last updated

This is what the `MADE.Data.EFCore.EntityBase` type provides for you. It initializes your created and last updated date values when you create your object.

To use it for your own entities, inherit from the `EntityBase` type. By default, it uses a `Guid` identifier.

```csharp
namespace MyApp.Data
{
    using MADE.Data.EFCore;

    public class User : EntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }
    }
}
```

### Using a custom key type with EntityBase

If you need a different identifier type, use the generic `EntityBase<TKey>`:

```csharp
public class Product : EntityBase<int>
{
    public string Name { get; set; }

    public decimal Price { get; set; }
}
```

### Entity interfaces

The following interfaces are available for implementing custom entity types:

- `IDatedEntity` - Defines `CreatedDate` and `UpdatedDate` properties.
- `IEntityBase<TKey>` - Extends `IDatedEntity` with a typed `Id` property.
- `IEntityBase` - A convenience interface that uses `Guid` as the key type.

## Configuring entities with EntityBaseExtensions

The `MADE.Data.EFCore.Extensions.EntityBaseExtensions` class provides extensions for configuring entity types in your `DbContext` model builder.

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Configures the entity key and UTC date properties for a Guid-based entity
    modelBuilder.Entity<User>().Configure();

    // For entities with a custom key type
    modelBuilder.Entity<Product>().ConfigureWithKey<Product, int>();
}
```

The `ConfigureDateProperties` extension can be used independently to configure UTC date properties on any entity implementing `IDatedEntity`.

## Storing dates in UTC with UtcDateTimeConverter

The `MADE.Data.EFCore.Converters.UtcDateTimeConverter` helps ensure that entity model dates are stored and read in UTC format.

Use the `IsUtc()` annotation on date properties in your entity configuration, then apply the converter to the model builder:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.ApplyUtcDateTimeConverter();
}
```

## DbContext extensions

The `MADE.Data.EFCore.Extensions.DbContextExtensions` class provides additional helpers:

- `UpdateAsync<T>` - Updates an entity and saves changes in a single call.
- `RemoveWhere<T>` - Removes entities from a `DbSet` matching a predicate.
- `SetEntityDates` - Automatically sets `CreatedDate` and `UpdatedDate` on tracked entities. Best called from an override of `SaveChangesAsync`.
- `TrySaveChangesAsync` - Attempts to save changes to the database and handles concurrency exceptions.

## Query extensions

The `MADE.Data.EFCore.Extensions.QueryableExtensions` class provides helpers for querying:

- `Page<T>` - Applies skip and take pagination to a query based on page number and page size.
- `OrderBy<T>` - Dynamically orders query results by a property name string, with optional descending sort.

## Soft-delete support with ISoftDeletable

The `MADE.Data.EFCore.ISoftDeletable` interface adds soft-delete support to your entities. Instead of permanently removing records, entities are marked as deleted and filtered out of queries by default.

```csharp
public class User : EntityBase, ISoftDeletable
{
    public string Name { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }
}
```

### Applying a global query filter

Use `ApplySoftDeleteFilter` in your model builder to automatically exclude soft-deleted entities from all queries:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.ApplySoftDeleteFilter();
}
```

To query soft-deleted entities, use `IgnoreQueryFilters()` on a specific query.

### Automatic soft-delete interception

Use `InterceptSoftDeletions` in your `SaveChangesAsync` override to automatically convert hard deletes to soft deletes:

```csharp
public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
{
    this.InterceptSoftDeletions();
    this.SetEntityDates();
    return await base.SaveChangesAsync(cancellationToken);
}
```

### Manual soft-delete and restore

The `SoftDelete` and `Restore` extension methods allow you to explicitly manage the soft-delete state:

```csharp
user.SoftDelete();  // Sets IsDeleted = true and DeletedDate
user.Restore();     // Clears IsDeleted and DeletedDate
```

## Audit trail support with IAuditableEntity

The `MADE.Data.EFCore.IAuditableEntity` interface adds user tracking to your entities, recording who created and last updated each record.

```csharp
public class Order : EntityBase, IAuditableEntity
{
    public string Description { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }
}
```

### Automatic audit info tracking

Use `SetEntityAuditInfo` in your `SaveChangesAsync` override to automatically set audit fields:

```csharp
public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
{
    this.SetEntityDates();
    this.SetEntityAuditInfo(currentUserId);
    return await base.SaveChangesAsync(cancellationToken);
}
```
