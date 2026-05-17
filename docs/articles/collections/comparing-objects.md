---
uid: collections-comparing-objects
title: Comparing objects
---

# Comparing objects

LINQ methods like `Union`, `Distinct`, `Intersect`, and `Except` require an `IEqualityComparer<T>` when you want to compare objects by something other than their default equality. The standard approach is to create a full class that implements `IEqualityComparer<T>` with both `Equals` and `GetHashCode` - which is a lot of ceremony for what's usually a one-line comparison.

`GenericEqualityComparer<T>` lets you define that comparison inline with a lambda expression.

## Using GenericEqualityComparer

The comparer takes a function that extracts the value to compare. Two objects are considered equal when the function returns the same value for both.

```csharp
using MADE.Collections.Compare;

var comparer = new GenericEqualityComparer<Product>(p => p.Id);

// Use with any LINQ method that accepts IEqualityComparer
var uniqueProducts = allProducts.Distinct(comparer);
var combined = listA.Union(listB, comparer);
var common = listA.Intersect(listB, comparer);
```

## Practical example: merging permission sets

A common scenario is combining multiple collections while ensuring no duplicates. Without `GenericEqualityComparer`, you'd need a dedicated comparer class or resort to manual deduplication:

```csharp
using MADE.Collections.Compare;

public static class ApplicationPermissions
{
    private static readonly GenericEqualityComparer<Permission> PermissionComparer =
        new(permission => permission.Id);

    private static readonly IEnumerable<Permission> AdminPermissions = new List<Permission>();
    private static readonly IEnumerable<Permission> UserPermissions = new List<Permission>();

    public static IEnumerable<Permission> GetAllPermissions()
    {
        return AdminPermissions.Union(UserPermissions, PermissionComparer);
    }
}
```

## When to use this vs other approaches

**Use `GenericEqualityComparer`** when you need a quick equality comparison for LINQ operations and the comparison logic is simple (comparing by a single property or a computed value).

**Use a full `IEqualityComparer<T>` implementation** when you need the comparer in multiple places across your codebase, or when the equality logic is complex enough to warrant its own testable class.

**Use records** if you're on C# 9+ and can define your type as a `record`, which gives you value-based equality for free. But when you're working with existing classes or need to compare by a specific property rather than all properties, `GenericEqualityComparer` is the pragmatic choice.
