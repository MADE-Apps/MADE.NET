---
uid: package-testing
title: Using the Testing package | MADE.NET
---

# Using the Testing package

While the `MADE.Testing` library is designed to be complementary to your own unit testing projects, it can be used in any project you wish you make assertions.

`MADE.Testing` is unit testing framework agnostic so it can be used with your flavor of choice. Whether that's NUnit, xUnit, MSTest, or another, you can use any helpers from this library.

## Asserting collection equivalency with CollectionAssertExtensions

The `ShouldBeEquivalentTo` extension method for `IEnumerable` instances is capable of testing whether two collections contain the same elements, regardless of order.

Before getting to the point of comparing the actual items in the collections, the extension will check the nullable state, whether the two collections are the same object reference, and if they have the same number of items. 

After this point, the extension will compare items in both collections to ensure that each collection contains the same items by equality.

Below is an example of a scenario that would result in a valid and invalid test run.

```csharp
[Test]
public void ValidTest()
{
    IEnumerable<string> expected = new List<string>{"Hello", "World"};
    var actual = new List<string>{"World", "Hello"};
    actual.ShouldBeEquivalentTo(expected);
}

[Test]
public void InvalidTest()
{
    IEnumerable<string> expected = new List<string>{"Hello", "World"};
    var actual = new List<string>{"World", "Bonjour"};
    actual.ShouldBeEquivalentTo(expected); // Throws AssertFailedException
}
```

You can also perform the same check for scenarios where the collections are **not** equivalent.
