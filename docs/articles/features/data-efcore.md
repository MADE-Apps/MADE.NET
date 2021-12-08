---
uid: package-data-converters
title: Using the Data Converters package
---

# Using the Data Entity Framework Core package

The Data Entity Framework Core package provides a collection of helpers, extensions, and converters for applications taking advantage of the `Microsoft.EntityFrameworkCore` library.

## Standardizing your entities with EntityBase

When setting up your entities, there are some common standard properties you'll usually want to include in most circumstances.

These are:

- An identifier
- A date the entity was created
- A date the entity was last updated

This is what the `MADE.Data.EFCore.EntityBase` type provides for you. It even goes as far as to initialize your created and last updated date values for you when you create your object.

To use it for your own entities, it's as simple as inheriting from the `EntityBase` type.

```csharp
namespace MyApp.Data
{
    using MADE.Data.EFCore;

    public class User : EntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }
    }
}
```
