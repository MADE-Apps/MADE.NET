---
uid: web-pagination
title: Pagination
---

# Pagination

Returning unbounded result sets from an API is a performance problem waiting to happen. Pagination is the standard solution, but implementing it consistently across all endpoints requires a shared request/response pattern. `PaginatedRequest` and `PaginatedResponse` provide that pattern.

## PaginatedRequest

`PaginatedRequest` takes a page number and page size, and automatically calculates the `Skip` and `Take` values you need for your data query:

```csharp
using MADE.Web;

var request = new PaginatedRequest(page: 2, pageSize: 25);
// request.Skip = 25
// request.Take = 25
```

## PaginatedResponse

`PaginatedResponse<T>` wraps the result set with metadata about the current page and total available data:

```csharp
using MADE.Web;

var response = new PaginatedResponse<Product>(
    items: products,
    page: 2,
    pageSize: 25,
    availableCount: 150);

// response.Page = 2
// response.PageSize = 25
// response.AvailableCount = 150
// response.TotalPages = 6
```

The `TotalPages` property is calculated automatically from `AvailableCount` and `PageSize`.

## Using with EF Core

Pair with the `Page` extension from `MADE.Data.EFCore` for a complete pagination flow:

```csharp
using MADE.Data.EFCore.Extensions;
using MADE.Web;

[HttpGet("products")]
public async Task<PaginatedResponse<ProductDto>> GetProducts(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 25)
{
    var totalCount = await dbContext.Products.CountAsync();

    var items = await dbContext.Products
        .OrderBy(p => p.Name)
        .Page(page, pageSize)
        .Select(p => new ProductDto(p.Id, p.Name, p.Price))
        .ToListAsync();

    return new PaginatedResponse<ProductDto>(items, page, pageSize, totalCount);
}
```

The response gives the client everything it needs to build pagination UI: the current items, which page they're on, and how many pages are available.

## Query collection helpers

The `MADE.Web.Extensions.QueryCollectionExtensions` class provides helpers for reading typed values from query strings:

```csharp
using MADE.Web.Extensions;

int page = context.Request.Query.GetIntValueOrDefault("page", 1);
string search = context.Request.Query.GetStringValueOrDefault("q", "");
DateTime? from = context.Request.Query.GetDateTimeValueOrDefault("from", null);
```

These return the specified default value when the query parameter is missing or can't be parsed, avoiding the boilerplate of manual parsing and null checking.
