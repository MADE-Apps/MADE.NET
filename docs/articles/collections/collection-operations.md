---
uid: collections-collection-operations
title: Collection operations
---

# Collection operations

The `MADE.Collections.CollectionExtensions` class provides extension methods for `ICollection<T>`, `IList<T>`, and `IEnumerable<T>` that handle common operations the standard library doesn't include out of the box. These are the methods you'd otherwise write as private helpers in every project.

## Synchronizing collections with MakeEqualTo

One of the most common UI patterns is keeping a displayed collection in sync with a data source. The naive approach - clearing and re-adding everything - breaks selection state, animations, and scroll position in UI frameworks. The `MakeEqualTo` extension handles this correctly by only adding and removing the items that actually changed.

```csharp
using MADE.Collections;

var displayedUsers = new ObservableCollection<User> { alice, bob, charlie };
var latestUsers = await userService.GetAllAsync();

// Adds missing items, removes items no longer present
displayedUsers.MakeEqualTo(latestUsers);
```

This is particularly useful in MVVM applications where your `ObservableCollection` is bound to a UI control and you need to update the data without disrupting the user experience.

## Verifying collection equivalence with AreEquivalent

When you need to check whether two collections contain the same items regardless of order, `AreEquivalent` gives you a clean one-liner. This is useful for validation, testing, or confirming that a sync operation completed successfully.

```csharp
var expected = new List<string> { "read", "write", "delete" };
var actual = await GetUserPermissionsAsync(userId);

if (!actual.AreEquivalent(expected))
{
    throw new SecurityException("User permissions do not match expected set.");
}
```

## Bulk operations with AddRange and RemoveRange

The `ICollection<T>` interface only exposes `Add` and `Remove` for single items. `AddRange` and `RemoveRange` let you work with multiple items at once:

```csharp
var permissions = new List<Permission>();
permissions.AddRange(adminPermissions);
permissions.RemoveRange(revokedPermissions);
```

## Updating items in place with Update

The `Update` extension on `IList<T>` finds an item by a predicate and replaces it, returning `true` if the item was found and updated:

```csharp
var updated = products.Update(
    updatedProduct,
    (existing, replacement) => existing.Id == replacement.Id);

if (!updated)
{
    products.Add(updatedProduct);
}
```

This avoids the pattern of finding an index, removing the old item, and inserting the new one manually.

## Conditional operations with AddIf and RemoveIf

When adding or removing items depends on a condition, `AddIf` and `RemoveIf` keep your code concise:

```csharp
// Only add if the user has admin privileges
permissions.AddIf(deletePermission, () => currentUser.IsAdmin);

// Remove expired items
sessions.RemoveIf(session, () => session.ExpiresAt < DateTime.UtcNow);
```

The bulk variants `AddRangeIf` and `RemoveRangeIf` apply the same pattern to collections of items.

## Maintaining sort order with InsertAtPotentialIndex

If you're working with a sorted collection and need to insert an item at the correct position, `InsertAtPotentialIndex` finds the right spot using a comparison predicate:

```csharp
var sortedScores = new List<int> { 10, 20, 30, 50 };
sortedScores.InsertAtPotentialIndex(25, (newItem, existing) => newItem > existing);
// Result: [10, 20, 25, 30, 50]
```

Use the companion `PotentialIndexOf` extension when you need the index without performing the insertion.

## Iterating with ForEach

A convenience extension that lets you execute an action on each element of an `IEnumerable<T>` without writing a `foreach` loop:

```csharp
users.ForEach(user => SendWelcomeEmail(user));
```

## Chunking collections

When processing large collections in batches, `Chunk` splits an `IEnumerable<T>` into groups of a specified size:

```csharp
foreach (var batch in messages.Chunk(10))
{
    await messageQueue.SendBatchAsync(batch);
}
```

There is also a `Chunk` extension for `IQueryable<T>` in `QueryableExtensions` for splitting database queries into smaller operations. See [Queryable extensions](queryable-extensions.md).

## Shuffling with Shuffle

Randomly reorders the elements of a collection:

```csharp
var shuffledQuestions = quizQuestions.Shuffle();
```

## Checking for null or empty with IsNullOrEmpty

A null-safe check that handles both null references and empty collections:

```csharp
if (results.IsNullOrEmpty())
{
    return NoContent();
}
```

## Best practices

- **Use `MakeEqualTo` for UI-bound collections** instead of clearing and re-populating. It preserves the existing items and only makes the minimum changes needed.
- **Prefer `AreEquivalent` over `SequenceEqual`** when order doesn't matter. `SequenceEqual` requires items to be in the same order, which is rarely what you want for set comparison.
- **Use `AddIf`/`RemoveIf` for authorization-gated operations** to keep permission logic readable at the call site.
