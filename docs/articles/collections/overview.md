---
uid: collections-overview
title: Collections
---

# Collections

Every .NET project involves working with lists, dictionaries, and collections. The standard library covers the basics, but you'll quickly find yourself writing the same helper methods across projects - syncing two collections, conditionally adding items, sorting an `ObservableCollection`, or checking if a collection is null or empty.

`MADE.Collections` provides a set of extension methods and types that fill these gaps. They're small, focused, and designed to reduce the boilerplate you'd otherwise write yourself.

```bash
dotnet add package MADE.Collections
```

## What's included

| Feature | What it does |
| --- | --- |
| [Collection operations](collection-operations.md) | `MakeEqualTo`, `AddRange`, `RemoveRange`, `Update`, `AddIf`/`RemoveIf`, `Shuffle`, `IsNullOrEmpty`, and more. |
| [Comparing objects](comparing-objects.md) | `GenericEqualityComparer` for custom equality logic without implementing `IEqualityComparer` from scratch. |
| [Observable collections](observable-collections.md) | `ObservableItemCollection` with item-level change tracking, plus `Sort`/`SortDescending` for `ObservableCollection`. |
| [Dictionary extensions](dictionary-extensions.md) | `AddOrUpdate` and `GetValueOrDefault` for cleaner dictionary operations. |
| [Queryable extensions](queryable-extensions.md) | `Chunk` for splitting `IQueryable` sources into smaller batch queries. |

## When to use this package

- You're synchronizing UI collections with data source changes and need `MakeEqualTo` instead of manually diffing.
- You want conditional add/remove logic without wrapping every operation in an `if` statement.
- You're using `ObservableCollection` and need sorting that raises the correct change notifications.
- You need a quick equality comparer for LINQ operations like `Union` or `Distinct` without creating a full `IEqualityComparer<T>` class.

## Quick example

```csharp
using MADE.Collections;

// Sync a UI collection to match a data source
var displayed = new ObservableCollection<Product> { productA, productB, productC };
var latest = await GetProductsAsync();
displayed.MakeEqualTo(latest);

// Sort in place with proper change notifications
displayed.Sort(p => p.Name);

// Conditional operations
displayed.AddIf(newProduct, () => newProduct.IsActive);
```
