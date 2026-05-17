---
uid: data-validation-custom-validators
title: Custom validators
---

# Custom validators

The built-in validators cover common scenarios, but your application will likely need validation logic specific to your domain. Implementing `IValidator` gives you a consistent pattern that integrates seamlessly with `ValidatorCollection`.

## Implementing IValidator

Here's a validator that checks whether a username meets your application's naming rules:

```csharp
using MADE.Data.Validation;

public class UsernameValidator : IValidator
{
    public string Key { get; set; } = nameof(UsernameValidator);
    public bool IsInvalid { get; set; }
    public bool IsDirty { get; set; }
    public string FeedbackMessage { get; set; } = "Username must be 3-20 characters, letters and numbers only, starting with a letter.";

    public void Validate(object value)
    {
        var username = value?.ToString();

        IsInvalid = string.IsNullOrWhiteSpace(username)
            || username.Length < 3
            || username.Length > 20
            || !char.IsLetter(username[0])
            || !username.All(c => char.IsLetterOrDigit(c));

        IsDirty = true;
    }
}
```

Use it standalone or as part of a collection:

```csharp
var validators = new ValidatorCollection
{
    new RequiredValidator(),
    new UsernameValidator(),
};

validators.Validate(input);
```

## The IValidator contract

When implementing `IValidator`, follow these conventions:

- **Set `IsInvalid`** to `true` when validation fails, `false` when it passes.
- **Set `IsDirty`** to `true` after `Validate` is called, regardless of the result. This lets consumers distinguish between "not yet validated" and "validated and passed".
- **Provide a meaningful `FeedbackMessage`** that tells the user what went wrong and how to fix it.
- **Use `Key`** to identify the validator in collections. This defaults to the class name but can be customized for scenarios where you use multiple instances of the same validator type.

## When to use custom validators vs PredicateValidator

**Use `PredicateValidator<T>`** for simple, one-off checks where creating a full class would be overkill:

```csharp
var ageCheck = new PredicateValidator<int>
{
    Predicate = age => age >= 18,
    FeedbackMessage = "Must be 18 or older.",
};
```

**Use a custom `IValidator` class** when:
- The validation logic is complex enough to warrant its own tests.
- You'll reuse the validator across multiple forms or endpoints.
- You need to configure the validator with properties (like `MinLength` on `MinLengthValidator`).

If you build a validator that would be broadly useful, consider [contributing it back to MADE.NET](https://github.com/MADE-Apps/MADE.NET/issues/new/choose).
