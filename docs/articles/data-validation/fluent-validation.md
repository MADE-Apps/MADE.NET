---
uid: data-validation-fluent-validation
title: FluentValidation integration
---

# FluentValidation integration

If your project already uses [FluentValidation](https://fluentvalidation.net/), you don't need to choose between it and MADE's validation pipeline. The `MADE.Data.Validation.FluentValidation` package lets you use FluentValidation's `AbstractValidator<T>` types within MADE's `ValidatorCollection` pattern.

```bash
dotnet add package MADE.Data.Validation.FluentValidation
```

## Why integrate?

This is useful when you have UI controls or framework components that are built around MADE's `IValidator`/`ValidatorCollection` interface, but you want to use FluentValidation's rule-building API for the actual validation logic. You get the best of both worlds: FluentValidation's expressive rule syntax and MADE's consistent validation pipeline.

## Using FluentValidatorCollection

`FluentValidatorCollection<T>` accepts `AbstractValidator<T>` instances and exposes the same `IsInvalid`, `FeedbackMessages`, and `Validated` event as the standard `ValidatorCollection`:

```csharp
using FluentValidation;
using MADE.Data.Validation.FluentValidation;

// Define a FluentValidation validator
public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.Email).NotEmpty().EmailAddress();
        RuleFor(u => u.Name).NotEmpty().MinimumLength(2);
    }
}

// Use it with MADE's validation pipeline
var validators = new FluentValidatorCollection<User>
{
    new UserValidator(),
};

validators.Validate(user);

if (validators.IsInvalid)
{
    foreach (var message in validators.FeedbackMessages)
    {
        Console.WriteLine(message);
    }
}
```

## When to use this

- You have existing FluentValidation validators and want to use them in a MADE-compatible validation pipeline.
- Your UI framework or input controls expect MADE's `IValidator` interface but you prefer FluentValidation's rule syntax.
- You want to mix FluentValidation validators with MADE's built-in validators in the same validation flow.
