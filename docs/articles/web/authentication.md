---
uid: web-authentication
title: Authentication helpers
---

# Authentication helpers

Extracting user information from a `ClaimsPrincipal` involves knowing the correct claim type strings and handling cases where claims are missing. The `AuthenticatedUser` class provides typed access to common claims, and `IAuthenticatedUserAccessor` makes it available through dependency injection.

## AuthenticatedUser

Wraps a `ClaimsPrincipal` and exposes commonly needed identity properties:

```csharp
using MADE.Web.Identity;

var user = new AuthenticatedUser(httpContext.User);

string userId = user.Subject;  // "sub" claim
string email = user.Email;     // "email" claim
IEnumerable<string> roles = user.Roles;  // "role" claims
IReadOnlyList<Claim> allClaims = user.Claims;
```

## Using IAuthenticatedUserAccessor with DI

Register the accessor in your service collection:

```csharp
builder.Services.AddAuthenticatedUserAccessor();
```

Then inject it into your services:

```csharp
public class OrderService
{
    private readonly IAuthenticatedUserAccessor userAccessor;

    public OrderService(IAuthenticatedUserAccessor userAccessor)
    {
        this.userAccessor = userAccessor;
    }

    public async Task<Order> CreateOrderAsync(OrderRequest request)
    {
        var order = new Order
        {
            Description = request.Description,
            CreatedBy = userAccessor.AuthenticatedUser.Subject,
        };

        return await SaveAsync(order);
    }
}
```

This keeps your services clean - they don't need to accept `HttpContext` or `ClaimsPrincipal` directly, making them easier to test.

## Combining with EF Core audit tracking

`IAuthenticatedUserAccessor` pairs naturally with `IAuditableEntity` from `MADE.Data.EFCore`. Pass the user's subject claim to `SetEntityAuditInfo` in your `SaveChangesAsync` override:

```csharp
public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
{
    this.SetEntityDates();
    this.SetEntityAuditInfo(userAccessor.AuthenticatedUser.Subject);
    return await base.SaveChangesAsync(cancellationToken);
}
```

See [Audit tracking](../data-efcore/audit-tracking.md) for details.
