---
uid: data-efcore-entity-base
title: Entity base classes
---

# Entity base classes

Most entities in an EF Core application share the same foundational properties: an identifier, a creation timestamp, and a last-updated timestamp. Rather than defining these on every entity, `EntityBase` gives you a standardized base class that handles this consistently.

## Using EntityBase

Inherit from `EntityBase` to get a `Guid` identifier and automatic date initialization:

```csharp
using MADE.Data.EFCore;

public class User : EntityBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
}
```

When you create a new `User` instance, `Id` is set to a new `Guid`, and `CreatedDate` and `UpdatedDate` are initialized to `DateTime.UtcNow`.

## Using a custom key type

If your entity needs a different identifier type (e.g., `int` for auto-increment or `string` for external IDs), use the generic `EntityBase<TKey>`:

```csharp
public class Product : EntityBase<int>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class ExternalEvent : EntityBase<string>
{
    public string Source { get; set; }
    public string Payload { get; set; }
}
```

## Entity interfaces

If you can't use inheritance (e.g., your entity already has a base class), implement the interfaces directly:

| Interface | Properties |
| --- | --- |
| `IDatedEntity` | `CreatedDate`, `UpdatedDate` |
| `IEntityBase<TKey>` | `Id` (typed), plus `IDatedEntity` properties |
| `IEntityBase` | Convenience interface using `Guid` as the key type |

```csharp
public class AuditLog : ExistingBaseClass, IEntityBase
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string Action { get; set; }
}
```

## Configuring entities in the model builder

The `EntityBaseExtensions` class provides extensions for configuring entity types in your `DbContext`:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Configures the key and UTC date properties for a Guid-based entity
    modelBuilder.Entity<User>().Configure();

    // For entities with a custom key type
    modelBuilder.Entity<Product>().ConfigureWithKey<Product, int>();
}
```

The `Configure` extension sets the entity's `Id` as the primary key and applies UTC date conversion to `CreatedDate` and `UpdatedDate`. You can also use `ConfigureDateProperties` independently if you only need the date configuration.

## Best practices

- **Use `EntityBase` as your default** unless you have a specific reason to use a different key type. `Guid` keys avoid the issues with sequential integer keys in distributed systems.
- **Call `Configure()` in `OnModelCreating`** for every entity that inherits from `EntityBase`. This ensures consistent key and date configuration.
- **Pair with `SetEntityDates`** in your `SaveChangesAsync` override to automatically update `UpdatedDate` on every save. See [DbContext extensions](dbcontext-extensions.md).
