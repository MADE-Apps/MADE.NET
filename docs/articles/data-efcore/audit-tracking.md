---
uid: data-efcore-audit-tracking
title: Audit tracking
---

# Audit tracking

For many applications, knowing *when* a record changed isn't enough - you also need to know *who* changed it. Compliance, debugging, and accountability all require tracking the user behind each create and update operation.

`IAuditableEntity` adds `CreatedBy` and `UpdatedBy` fields to your entities, and the `SetEntityAuditInfo` extension automatically populates them from your authentication context.

## Setting up audit tracking

Implement `IAuditableEntity` on entities that need user tracking:

```csharp
using MADE.Data.EFCore;

public class Order : EntityBase, IAuditableEntity
{
    public string Description { get; set; }
    public decimal Amount { get; set; }

    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
```

## Automatic audit info on save

Call `SetEntityAuditInfo` in your `SaveChangesAsync` override, passing the current user's identifier:

```csharp
public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
{
    this.SetEntityDates();
    this.SetEntityAuditInfo(currentUserId);
    return await base.SaveChangesAsync(cancellationToken);
}
```

The extension scans the change tracker for entities implementing `IAuditableEntity`:
- **On insert**: sets both `CreatedBy` and `UpdatedBy` to the current user.
- **On update**: sets `UpdatedBy` to the current user, leaving `CreatedBy` unchanged.

## Getting the current user ID

The user identifier you pass to `SetEntityAuditInfo` depends on your authentication setup. If you're using the `MADE.Web` package, you can use `IAuthenticatedUserAccessor`:

```csharp
public class AppDbContext : DbContext
{
    private readonly IAuthenticatedUserAccessor userAccessor;

    public AppDbContext(DbContextOptions options, IAuthenticatedUserAccessor userAccessor)
        : base(options)
    {
        this.userAccessor = userAccessor;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        this.SetEntityDates();
        this.SetEntityAuditInfo(userAccessor.AuthenticatedUser.Subject);
        return await base.SaveChangesAsync(cancellationToken);
    }
}
```

See [Authentication helpers](../web/authentication.md) for setting up `IAuthenticatedUserAccessor`.

## Combining with soft delete

Audit tracking and soft delete work well together. When a user "deletes" a record, you can track who did it:

```csharp
public class Document : EntityBase, ISoftDeletable, IAuditableEntity
{
    public string Title { get; set; }

    // ISoftDeletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedDate { get; set; }

    // IAuditableEntity
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
```

With `InterceptSoftDeletions` and `SetEntityAuditInfo` both running in `SaveChangesAsync`, the soft delete is recorded with the `UpdatedBy` field showing who performed the deletion.
