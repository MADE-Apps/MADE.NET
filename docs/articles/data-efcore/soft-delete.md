---
uid: data-efcore-soft-delete
title: Soft delete
---

# Soft delete

Deleting records permanently is rarely what you want in a production application. Compliance requirements, audit trails, undo functionality, and data recovery all benefit from keeping records around but marking them as deleted. EF Core doesn't provide this out of the box, so you typically end up implementing query filters and save interceptors manually in each project.

`ISoftDeletable` standardizes this pattern with automatic query filtering and deletion interception.

## Setting up soft delete on your entities

Implement `ISoftDeletable` on any entity that should support soft deletion:

```csharp
using MADE.Data.EFCore;

public class User : EntityBase, ISoftDeletable
{
    public string Name { get; set; }
    public string Email { get; set; }

    public bool IsDeleted { get; set; }
    public DateTime? DeletedDate { get; set; }
}
```

## Applying the global query filter

Register the soft-delete filter in your `OnModelCreating` to automatically exclude deleted entities from all queries:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>().Configure();
    modelBuilder.ApplySoftDeleteFilter();
}
```

With this in place, `dbContext.Users.ToListAsync()` only returns non-deleted users. You never need to add `.Where(u => !u.IsDeleted)` to your queries.

### Querying deleted records

When you do need to see deleted records (admin panels, audit views, data recovery), use `IgnoreQueryFilters()`:

```csharp
var allUsers = await dbContext.Users
    .IgnoreQueryFilters()
    .ToListAsync();

var deletedUsers = await dbContext.Users
    .IgnoreQueryFilters()
    .Where(u => u.IsDeleted)
    .ToListAsync();
```

## Automatic deletion interception

Without interception, calling `dbContext.Users.Remove(user)` would still perform a hard delete. `InterceptSoftDeletions` converts these to soft deletes automatically:

```csharp
public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
{
    this.InterceptSoftDeletions();
    this.SetEntityDates();
    return await base.SaveChangesAsync(cancellationToken);
}
```

Now when you call `Remove()` on any entity that implements `ISoftDeletable`, the interceptor sets `IsDeleted = true` and `DeletedDate` to the current UTC time instead of issuing a SQL `DELETE`.

## Manual soft delete and restore

For explicit control, use the `SoftDelete` and `Restore` extension methods:

```csharp
// Soft delete
user.SoftDelete();
await dbContext.SaveChangesAsync();

// Restore a soft-deleted record
user.Restore();
await dbContext.SaveChangesAsync();
```

`SoftDelete()` sets `IsDeleted = true` and `DeletedDate` to `DateTime.UtcNow`. `Restore()` sets `IsDeleted = false` and clears `DeletedDate`.

## Best practices

- **Always call `InterceptSoftDeletions()` in your `SaveChangesAsync` override.** Without it, direct `Remove()` calls bypass soft-delete and permanently delete the record.
- **Apply `ApplySoftDeleteFilter()` globally** rather than per-entity. This ensures no soft-deletable entity is accidentally queried without the filter.
- **Use `IgnoreQueryFilters()` sparingly** and only in contexts where seeing deleted records is intentional (admin tools, audit logs, compliance reports).
- **Consider combining with `IAuditableEntity`** to also track who deleted the record. See [Audit tracking](audit-tracking.md).
