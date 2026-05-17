---
uid: data-efcore-dbcontext-extensions
title: DbContext extensions
---

# DbContext extensions

The `MADE.Data.EFCore.Extensions.DbContextExtensions` class provides helper methods for common EF Core operations: updating and saving in one call, removing entities by predicate, automatic date management, and safe save with concurrency handling.

## UpdateAsync

Updates an entity and saves changes in a single call, reducing the boilerplate of marking an entity as modified and then calling `SaveChangesAsync` separately:

```csharp
await dbContext.UpdateAsync(user);
```

## RemoveWhere

Removes all entities from a `DbSet` that match a predicate:

```csharp
await dbContext.RemoveWhere<Session>(s => s.ExpiresAt < DateTime.UtcNow);
```

## Automatic date management with SetEntityDates

`SetEntityDates` scans the change tracker for entities implementing `IDatedEntity` and automatically sets their `CreatedDate` (on insert) and `UpdatedDate` (on insert and update). Call it from your `SaveChangesAsync` override:

```csharp
public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
{
    this.SetEntityDates();
    return await base.SaveChangesAsync(cancellationToken);
}
```

This ensures timestamps are set consistently regardless of which code path triggers the save. You never need to manually set `UpdatedDate` at your call sites.

## TrySaveChangesAsync

A wrapper around `SaveChangesAsync` that catches `DbUpdateConcurrencyException` and returns a boolean indicating success:

```csharp
bool saved = await dbContext.TrySaveChangesAsync();
if (!saved)
{
    // Handle concurrency conflict
}
```

## Combining extensions in SaveChangesAsync

A typical `SaveChangesAsync` override uses multiple extensions together:

```csharp
public class AppDbContext : DbContext
{
    private readonly IAuthenticatedUserAccessor userAccessor;

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        this.InterceptSoftDeletions();       // Convert hard deletes to soft deletes
        this.SetEntityDates();               // Set CreatedDate and UpdatedDate
        this.SetEntityAuditInfo(             // Set CreatedBy and UpdatedBy
            userAccessor.AuthenticatedUser.Subject);
        return await base.SaveChangesAsync(cancellationToken);
    }
}
```

See [Soft delete](soft-delete.md) and [Audit tracking](audit-tracking.md) for details on `InterceptSoftDeletions` and `SetEntityAuditInfo`.
