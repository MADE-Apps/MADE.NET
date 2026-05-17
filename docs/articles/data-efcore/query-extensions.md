---
uid: data-efcore-query-extensions
title: Query extensions
---

# Query extensions

The `MADE.Data.EFCore.Extensions.QueryableExtensions` class provides pagination and dynamic ordering for EF Core queries. These are the two query patterns that almost every API endpoint needs, and they're simple enough that you shouldn't need to write them from scratch.

## Pagination with Page

The `Page` extension applies skip/take pagination to a query:

```csharp
using MADE.Data.EFCore.Extensions;

// Get page 2 with 25 items per page
var orders = await dbContext.Orders
    .OrderByDescending(o => o.CreatedDate)
    .Page(page: 2, pageSize: 25)
    .ToListAsync();
```

This translates to the appropriate `Skip` and `Take` calls. Pair it with `MADE.Web`'s `PaginatedRequest` and `PaginatedResponse` for a complete pagination solution. See [Pagination](../web/pagination.md).

## Dynamic ordering with OrderBy

The `OrderBy` extension orders query results by a property name string, with an optional descending flag. This is useful when the sort field comes from a query parameter or UI selection:

```csharp
using MADE.Data.EFCore.Extensions;

// Sort by a property name received from the API client
var products = await dbContext.Products
    .OrderBy("Price", descending: true)
    .Page(page: 1, pageSize: 50)
    .ToListAsync();
```

The property name is resolved using reflection against the entity type. If the property doesn't exist, the query is returned unmodified.

## UTC date conversion

The `UtcDateTimeConverter` ensures that date properties are stored and read in UTC format. Apply it globally in your model builder:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.ApplyUtcDateTimeConverter();
}
```

This is applied automatically when you use `EntityBase` with the `Configure()` extension, but `ApplyUtcDateTimeConverter()` lets you apply it to all date properties across your entire model if needed.

## Practical example: paginated API endpoint

```csharp
[HttpGet]
public async Task<PaginatedResponse<ProductDto>> GetProducts(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 25,
    [FromQuery] string sortBy = "Name",
    [FromQuery] bool descending = false)
{
    var query = dbContext.Products
        .OrderBy(sortBy, descending);

    var totalCount = await query.CountAsync();
    var items = await query.Page(page, pageSize).ToListAsync();

    return new PaginatedResponse<ProductDto>(
        items.Select(MapToDto),
        page,
        pageSize,
        totalCount);
}
```
