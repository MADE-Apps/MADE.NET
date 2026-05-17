---
uid: data-efcore-overview
title: Entity Framework Core
---

# Entity Framework Core

Every EF Core project ends up with the same patterns: a base entity with an ID and timestamps, UTC date handling, pagination helpers, soft-delete logic, and audit tracking. These aren't complex to implement individually, but they're tedious to set up correctly every time, and inconsistencies across projects create maintenance overhead.

`MADE.Data.EFCore` packages these patterns into a set of base classes, interfaces, and extensions that you configure once and then forget about.

```bash
dotnet add package MADE.Data.EFCore
```

## What's included

| Guide | What it covers |
| --- | --- |
| [Entity base classes](entity-base.md) | `EntityBase`, `EntityBase<TKey>`, `IDatedEntity`, and `IEntityBase` for standardizing your entity definitions. |
| [DbContext extensions](dbcontext-extensions.md) | `UpdateAsync`, `RemoveWhere`, `SetEntityDates`, and `TrySaveChangesAsync`. |
| [Soft delete](soft-delete.md) | `ISoftDeletable` with automatic query filtering and deletion interception. |
| [Audit tracking](audit-tracking.md) | `IAuditableEntity` for recording who created and updated each record. |
| [Query extensions](query-extensions.md) | Pagination with `Page`, dynamic ordering with `OrderBy`, and UTC date conversion. |

## When to use this package

- You want a consistent entity structure across your EF Core models with ID, created date, and updated date.
- You need soft-delete behavior without writing custom query filters and save interceptors.
- You want to automatically track which user created or last modified a record.
- You need pagination and dynamic sorting for API endpoints.

## Quick example

```csharp
using MADE.Data.EFCore;

// Define your entity
public class Article : EntityBase, ISoftDeletable, IAuditableEntity
{
    public string Title { get; set; }
    public string Content { get; set; }

    // ISoftDeletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedDate { get; set; }

    // IAuditableEntity
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}

// Configure in your DbContext
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Article>().Configure();
    modelBuilder.ApplySoftDeleteFilter();
    modelBuilder.ApplyUtcDateTimeConverter();
}

// Automatic date and audit tracking on save
public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
{
    this.InterceptSoftDeletions();
    this.SetEntityDates();
    this.SetEntityAuditInfo(currentUserId);
    return await base.SaveChangesAsync(cancellationToken);
}
```
