---
uid: package-testing
title: Using the Testing package
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

## Asserting null state with ObjectAssertExtensions

The `ShouldBeNull` and `ShouldNotBeNull` extension methods for any object allow you to assert the null state of a value.

```csharp
[Test]
public void ShouldBeNullTest()
{
    object? value = null;
    value.ShouldBeNull();
}

[Test]
public void ShouldNotBeNullTest()
{
    object value = new();
    value.ShouldNotBeNull();
}
```

## Asserting boolean values with BooleanAssertExtensions

The `ShouldBeTrue` and `ShouldBeFalse` extension methods for `bool` values allow you to assert the expected state of a boolean.

```csharp
[Test]
public void ShouldBeTrueTest()
{
    bool result = true;
    result.ShouldBeTrue();
}

[Test]
public void ShouldBeFalseTest()
{
    bool result = false;
    result.ShouldBeFalse();
}
```

## Comparing values with ComparableAssertExtensions

The `ShouldBeGreaterThan`, `ShouldBeGreaterThanOrEqualTo`, `ShouldBeLessThan`, and `ShouldBeLessThanOrEqualTo` extension methods allow you to assert the comparison of `IComparable` values.

```csharp
[Test]
public void ComparisonTest()
{
    int value = 10;
    value.ShouldBeGreaterThan(5);
    value.ShouldBeLessThan(20);
    value.ShouldBeGreaterThanOrEqualTo(10);
    value.ShouldBeLessThanOrEqualTo(10);
}
```

## Asserting strings with StringAssertExtensions

The `ShouldContain`, `ShouldNotContain`, `ShouldStartWith`, and `ShouldEndWith` extension methods allow you to assert the contents of strings.

```csharp
[Test]
public void StringAssertionTest()
{
    string value = "Hello, World!";
    value.ShouldContain("World");
    value.ShouldNotContain("Goodbye");
    value.ShouldStartWith("Hello");
    value.ShouldEndWith("World!");
}
```

## Asserting exceptions with ExceptionAssertExtensions

The `ShouldThrow` and `ShouldNotThrow` extension methods allow you to assert that an action throws or does not throw an exception. Async variants `ShouldThrowAsync` and `ShouldNotThrowAsync` are also available.

```csharp
[Test]
public void ShouldThrowTest()
{
    Action action = () => throw new InvalidOperationException("Oops");
    var exception = action.ShouldThrow<InvalidOperationException>();
    // exception.Message is "Oops"
}

[Test]
public void ShouldNotThrowTest()
{
    Action action = () => { /* no error */ };
    action.ShouldNotThrow();
}
```
