---
uid: testing-overview
title: Testing
---

# Testing

Most .NET test assertion libraries are tightly coupled to a specific test framework. If you're using xUnit, you get `Assert.Equal`. MSTest gives you `Assert.AreEqual`. NUnit has `Assert.That`. They all do the same thing with slightly different syntax, and if you switch frameworks, you rewrite your assertions.

`MADE.Testing` provides fluent `Should*` assertion extensions that work with any test framework. The assertions throw `AssertFailedException` on failure, which is recognized by all major test runners.

```bash
dotnet add package MADE.Testing
```

## What's included

| Assertions | What they cover |
| --- | --- |
| `ShouldBeNull` / `ShouldNotBeNull` | Null state checking for any object. |
| `ShouldBeTrue` / `ShouldBeFalse` | Boolean value assertions. |
| `ShouldBeGreaterThan` / `ShouldBeLessThan` | Comparison assertions for `IComparable` values (plus `OrEqualTo` variants). |
| `ShouldContain` / `ShouldNotContain` | String content assertions. |
| `ShouldStartWith` / `ShouldEndWith` | String prefix and suffix assertions. |
| `ShouldBeEquivalentTo` / `ShouldNotBeEquivalentTo` | Collection equivalence (same items, any order). |
| `ShouldThrow` / `ShouldNotThrow` | Exception assertions with async variants. |

## Quick example

```csharp
using MADE.Testing;

[Test]
public async Task CreateUser_ReturnsValidUser()
{
    var user = await userService.CreateAsync("James", "james@example.com");

    user.ShouldNotBeNull();
    user.Name.ShouldContain("James");
    user.Email.ShouldEndWith("@example.com");
    user.Age.ShouldBeGreaterThan(0);
}

[Test]
public void InvalidInput_ThrowsValidationException()
{
    Action act = () => userService.Validate(null);
    act.ShouldThrow<ArgumentNullException>();
}

[Test]
public void GetPermissions_ReturnsExpectedSet()
{
    var expected = new[] { "read", "write", "delete" };
    var actual = permissionService.GetPermissions("admin");
    actual.ShouldBeEquivalentTo(expected); // Order doesn't matter
}
```

## When to use this package

- You want framework-agnostic assertions that read naturally in English.
- You're working on a project that uses multiple test frameworks (e.g., NUnit for unit tests, xUnit for integration tests) and want consistent assertion syntax.
- You want collection equivalence checking that ignores order, which most built-in assertion libraries don't provide.

## Assertion details

### Collection equivalence

`ShouldBeEquivalentTo` checks that two collections contain the same elements regardless of order. It performs several checks in sequence:

1. Null state comparison
2. Reference equality (are they the same object?)
3. Count comparison
4. Element-by-element equality

This catches common bugs where a method returns the right items but in a different order than the test expected.

### Exception assertions

`ShouldThrow<T>` returns the caught exception, letting you assert on its properties:

```csharp
var ex = action.ShouldThrow<InvalidOperationException>();
ex.Message.ShouldContain("order not found");
```

Async variants are available for testing async methods:

```csharp
Func<Task> act = () => service.ProcessAsync(invalidInput);
await act.ShouldThrowAsync<ValidationException>();
```
