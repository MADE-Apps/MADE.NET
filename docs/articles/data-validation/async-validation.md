---
uid: data-validation-async-validation
title: Async validation
---

# Async validation

Some validation can't happen in-memory. Checking whether an email is already registered, verifying an invite code against a database, or calling an external API for address validation all require I/O. The standard `IValidator` interface is synchronous, so `MADE.Data.Validation` provides `IAsyncValidator` and `AsyncValidatorCollection` for these scenarios.

## Implementing IAsyncValidator

`IAsyncValidator` has the same properties as `IValidator` but replaces `Validate(object)` with `ValidateAsync(object, CancellationToken)`:

```csharp
using MADE.Data.Validation;

public class UniqueEmailValidator : IAsyncValidator
{
    private readonly IUserRepository repository;

    public UniqueEmailValidator(IUserRepository repository)
    {
        this.repository = repository;
    }

    public string Key { get; set; } = nameof(UniqueEmailValidator);
    public bool IsInvalid { get; set; }
    public bool IsDirty { get; set; }
    public string FeedbackMessage { get; set; } = "This email address is already registered.";

    public async Task ValidateAsync(object value, CancellationToken cancellationToken = default)
    {
        var email = value?.ToString();
        IsInvalid = !string.IsNullOrWhiteSpace(email)
            && await repository.EmailExistsAsync(email, cancellationToken);
        IsDirty = true;
    }
}
```

## Using AsyncValidatorCollection

`AsyncValidatorCollection` works the same way as `ValidatorCollection` but executes each validator asynchronously:

```csharp
var validators = new AsyncValidatorCollection
{
    new UniqueEmailValidator(userRepository),
    new ActiveDomainValidator(domainService),
};

await validators.ValidateAsync(emailAddress);

if (validators.IsInvalid)
{
    foreach (var message in validators.FeedbackMessages)
    {
        Console.WriteLine(message);
    }
}
```

## Combining sync and async validation

A common pattern is to run cheap synchronous validators first (format checks, length constraints) and only run expensive async validators if the basic checks pass:

```csharp
// Step 1: synchronous validation
var formatValidators = new ValidatorCollection
{
    new RequiredValidator(),
    new EmailValidator(),
};

formatValidators.Validate(email);
if (formatValidators.IsInvalid)
{
    return formatValidators.FeedbackMessages;
}

// Step 2: async validation (only if format is valid)
var asyncValidators = new AsyncValidatorCollection
{
    new UniqueEmailValidator(userRepository),
};

await asyncValidators.ValidateAsync(email);
if (asyncValidators.IsInvalid)
{
    return asyncValidators.FeedbackMessages;
}
```

This avoids unnecessary database calls for obviously invalid input.
