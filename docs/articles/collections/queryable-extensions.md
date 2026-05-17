---
uid: collections-queryable-extensions
title: Queryable extensions
---

# Queryable extensions

The `MADE.Collections.QueryableExtensions` class provides a `Chunk` extension for `IQueryable<T>` sources, designed for batch processing scenarios where you need to split a database query into smaller operations.

## Chunking queryable sources

When processing large datasets from Entity Framework Core or another `IQueryable` provider, loading everything into memory at once isn't practical. The `Chunk` extension splits the query into smaller batches:

```csharp
using MADE.Collections;

public async Task ProcessAllOrdersAsync(IQueryable<Order> orders)
{
    foreach (var batch in orders.Chunk(100))
    {
        foreach (var order in batch)
        {
            await ProcessOrderAsync(order);
        }
    }
}
```

Each chunk executes a separate query against the data source with the appropriate `Skip` and `Take` parameters, keeping memory usage predictable regardless of the total dataset size.

## When to use this vs EF Core's built-in chunking

EF Core 6+ provides `ExecuteUpdate` and `ExecuteDelete` for bulk operations. Use those when you're performing simple update/delete operations.

Use `Chunk` when you need to process each record individually with custom logic - sending emails, calling external APIs, or performing complex transformations that can't be expressed in a single SQL statement.
