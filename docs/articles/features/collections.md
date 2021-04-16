---
uid: package-collections
title: Using the Collections package | MADE.NET
---

# Using the Collections package

The Collections package is designed to provide helpful extensions and additional types for working with enumerable objects in your applications.

## Comparing objects using the GenericEqualityComparer

The `MADE.Collections.Compare.GenericEqualityComparer` is a simple to use `IEqualityComparer` that takes a function as a parameter when constructed.

This function is used when comparing for equality to validate that the two objects under test are equal.

It allows you to go over equality based on the `Equals` method and provide your own equality comparison.

Here's an example of it in use, combining multiple collections together with distinct objects by comparing based on the ID of the items.

```csharp
namespace App.Permissions
{
    using System.Collections.Generic;
    using System.Linq;
    using MADE.Collections.Compare;

    public static class ApplicationPermissions
    {
        private static readonly GenericEqualityComparer<Permission> PermissionComparer = new GenericEqualityComparer<Permission>( permission => permission.Id );

        private static readonly IEnumerable<Permission> AdminPermissions = new List<Permission>();

        private static readonly IEnumerable<Permission> UserPermissions = new List<Permission>();

        public static IEnumerable<Permission> GetAllPermissions()
        {
            return AdminPermissions.Union(UserPermissions, PermissionComparer);
        }
    }
}
```

## Make a collection of items equal to another using the MakeEqualTo extension

The `MADE.Collections.CollectionExtensions` static class contains a `MakeEqualTo<T>` extension method for `ICollection<T>` objects that will add or remove items from the destination collection to match the source.

You can use this in your projects similar to our example below.

```csharp
public void UpdateAdminPermissions(IEnumerable<Permission> permissions)
{
    AdminPermissions.MakeEqualTo<Permission>(permissions);
}
```

## Ensuring a collection of items is equivalent to another using the AreEquivalent extension

The `MADE.Collections.CollectionExtensions` static class contains an `AreEquivalent<T>` extension method for `ICollection<T>` objects that will ensure the expected and actual collections contain the same items with no regard to order.

Here's an example of this in use.

```csharp
public void UpdateAdminPermissions(IEnumerable<Permission> permissions)
{
    AdminPermissions.MakeEqualTo<Permission>(permissions);

    if (!AdminPermission.AreEquivalent<Permission>(permissions))
    {
        throw new InvalidOperationException("Permissions were not updated successfully.");
    }
}
```

## Adding a collection of items to another using the AddRange extension

`ICollection<T>` objects don't contain a method for adding a collection of items to it.

This extension method takes a collection of items with the same item type and adds them all in order to the end of the destination collection.

You can use this extension in your own code like this example.

```csharp
public void AddAdminPermissions(IEnumerable<Permission> permissions)
{
    AdminPermissions.AddRange<Permission>(permissions);
}
```

## Updating an item in a collection using the Update extension

The `MADE.Collections.CollectionExtensions` static class contains an `Update<T>` extension method for `IList<T>` objects that allows an item to be updated in the collection based on a predicate.

The predicate will find the item in the collection, and ensure that it is updated, returning true once executed.

If the item can't be found, it will return false.

You can use this extension in your application like the example below.

```csharp
public void UpdatePermission(Permission permissionToUpdate)
{
    var updated = AdminPermissions.Update<Permission>(permissionToUpdate, (p1, p2) => p1.Id == p2.Id);
    if (updated)
    {
        // Update UI
    }
}
```

## Removing a collection of items from another using the RemoveRange extension

`ICollection<T>` objects don't contain a method for removing a collection of items from it.

This extension method takes a collection of items with the same item type and removes them from the destination collection.

Here's an example of this in use.

```csharp
public void RemoveAdminPermissions(IEnumerable<Permission> permissions)
{
    AdminPermissions.RemoveRange<Permission>(permissions);
}
```

## Performing actions over collections using the ForEach extension

Just a quick and easy extension to have at your disposal, `ForEach` is an `IEnumerable` extension that allows you to perform an action on each element in the collection. 

Simply call `ForEach` on your collection passing an action to perform on the items, for example:

```csharp
private void Update()
{
    myStrings.ForEach(s => VerifyString(s));
}

private void VerifyString(string val)
{
   // Do verification
}
```
