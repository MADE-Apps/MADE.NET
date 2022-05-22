---
uid: package-data-validation
title: Using the Data Validation package
---

# Using the Data Validation package

The Data Validation package is designed to provide out-of-the-box data validation to applications built with C#.

## Validating an object using the ValidatorCollection

Data validation can be implemented in so many different ways. MADE provides the capability to perform data validation through its own `IValidator` interface that can be used to create consistent data validators.

Using the `MADE.Data.Validation.ValidatorCollection` based on a `List` type, you can construct a collection of `IValidator` instances which can be used to validate values.

For example, you might want a collection of validators that ensure that a value is provided, it has a minimum length, and it contains only alphanumeric characters.

Instead of implementing your own custom validation in your application, you can take advantage of the built-in `IValidator` implementation of this package and utilize them with the `ValidatorCollection`.

This can be achieved like the example below.

```csharp
namespace App.Validations
{
    using MADE.Data.Converters;

    public class ApplicationConverters
    {
        private readonly ValidatorCollection RequiredAlphaNumericValidators = new ValidatorCollection
                {
                    new RequiredValidator { Key = "Required" },
                    new AlphaNumericValidator { Key = "AlphaNumericOnly" },
                };

        public void EnsureValid(string value)
        {
            RequiredAlphaNumericValidators.Validate(value);

            if (RequiredAlphaNumericValidators.IsInvalid)
            {
                throw new InvalidOperationException(string.Join(", ", RequiredAlphaNumericValidators.FeedbackMessages)));
            }
        }
    }
}
```

The `ValidatorCollection` will loop through all of the `IValidator` instances that have been registered with it and validate the value with each one.

When there are errors, this will result in the `FeedbackMessages` collection being populated with validation messages which can be used to display to the user.

You also have the `IsInvalid` property which exposes where there are any validators that are invalid.

There is also control through a `Validated` event handler which you can use to trigger actions based on whether the validations have run in the collection.

## Available IValidator types

`IValidator` types can be used on their own, as well as with the `ValidatorCollection`.

Here is a list of the in-box `IValidators` that you can use in your applications.

### AlphaValidator

The `AlphaValidator` checks a string value contains only letter characters, i.e. a-z, ignoring casing.

If the value contains only letters, the validator will report valid; otherwise, it will report invalid.

### AlphaNumericValidator

Similar to the `AlphaValidator`, the `AlphaNumericValidator` extends the characters in the validation to include numbers also.

### Base64Validator

The `Base64Validator` checks whether a string value is a valid Base64 encoded string.

### BetweenValidator

The `BetweenValidator` validates an `IComparable` value is between a configurable minimum and maximum range.

The range can be configured by setting the `Min` and `Max` values, as well as an `Inclusive` flag to configure whether the minimum and maximum values are included in the range (defaults to `true`).

The in-box `System` types which implement the `IComparable` interface can be [found in the Microsoft documentation](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable?view=net-5.0).

### EmailValidator

The `EmailValidator` extends the in-box `RegexValidator` type to provide validating a string is a valid e-mail address.

The current pattern used to validate e-mail addresses is:

```csharp
this.Pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-zA-Z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
```

MADE.NET has a comprehensive set of test cases which validate the implementation with a variety of different valid and invalid email addresses.

### GuidValidator

The `GuidValidator` checks whether a string value is a valid GUID. 

The underlying implementation uses the `Guid.TryParse` method to validate the string.

### IpAddressValidator

The `IpAddressValidator` is a simple data validator which ensures that a value is a valid IP address.

The implementation splits the IP address into each nibble and validates them based on the following criteria:

- Is less than 4 characters
- Is greater than 0 characters
- Is a digit
- Is a numeric value between 0 and 255

### LatitudeValidator

The `LatitudeValidator` validates a value is within the valid range for a latitude value (-90 and 90).

### LongitudeValidator

The `LongitudeValidator` validates a value is within the valid range for a longitude value (-180 and 180).

### MacAddressValidator

The `MacAddressValidator` is a simple data validator which ensures that a value is a valid MAC address.

The implementation uses the .NET `PhysicalAddress` class to parse the provided value.

For more information on the `PhysicalAddress` class, see the [Microsoft documentation](https://docs.microsoft.com/en-us/dotnet/api/system.net.networkinformation.physicaladdress?view=net-6.0).

### MaxValueValidator

The `MaxValueValidator` validates an `IComparable` value is less than a configurable maximum value.

The maximum can be configured by setting the `Max` value.

The in-box `System` types which implement the `IComparable` interface can be [found in the Microsoft documentation](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable?view=net-5.0).

### MinValueValidator

The `MinValueValidator` validates an `IComparable` value is greater than a configurable minimum value.

The minimum can be configured by setting the `Min` value.

The in-box `System` types which implement the `IComparable` interface can be [found in the Microsoft documentation](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable?view=net-5.0).

### PredicateValidator

The `PredicateValidator` validates a value using a custom predicate to ensure that a condition is met.

### RegexValidator

The `RegexValidator` is a generic data validator which validates a value based on a configurable regular expression pattern.

The pattern can be configured by setting the `Pattern` value.

### RequiredValidator

The `RequiredValidator` is a data validator that ensures that the value provided exists.

This is determined based on the following criteria:

- The value is not null
- The value is a collection and contains items
- The value is a boolean and is true
- The value is a string and is not null or whitespace

## Creating your own custom data validators

There are likely to be more advanced, custom scenarios for your own applications that need to extend the capabilities past the in-box `IValidator` types.

If you want to take advantage of what goes into a data validator, you can build your own using the `MADE.Data.Validation.IValidator` interface.

You can then build out your own, similar to our validators such as the `RequiredValidator`.

```csharp
namespace MADE.Data.Validation.Validators
{
    using System.Collections;
    using MADE.Data.Validation.Extensions;
    using MADE.Data.Validation.Strings;

    public class RequiredValidator : IValidator
    {
        private string feedbackMessage = Resources.ResourceManager.GetString("RequiredValidator_FeedbackMessage");

        public string Key { get; set; } = nameof(RequiredValidator);

        public bool IsInvalid { get; set; }

        public bool IsDirty { get; set; }

        public string FeedbackMessage
        {
            get => this.feedbackMessage.IsNullOrWhiteSpace() ? Resources.RequiredValidator_FeedbackMessage : this.feedbackMessage;
            set => this.feedbackMessage = value;
        }

        public void Validate(object value)
        {
            this.IsInvalid = DetermineIsInvalid(value);
            this.IsDirty = true;
        }

        private static bool DetermineIsInvalid(object value)
        {
            switch (value)
            {
                case null:
                    return true;
                case ICollection collection:
                    return collection.Count <= 0;
                case bool isTrue:
                    return !isTrue;
                case string str:
                    return str.IsNullOrWhiteSpace();
                default:
                    return false;
            }
        }
    }
}
```

If there is a common data validator you think is missing from MADE.NET, [raise a tracking item on GitHub](https://github.com/MADE-Apps/MADE.NET/issues/new/choose) and we'll get it implemented.

## Using FluentValidation with MADE.NET

The `MADE.Data.Validation.FluentValidation` package provides an easy way to take advantage of validation with the [FluentValidation](https://fluentvalidation.net/) library validator framework.

### Validating an object using the FluentValidatorCollection

Using the `MADE.Data.Validation.FluentValidatorCollection<T>` based on a `List` type, you can construct a collection of `AbstractValidator` instances which can be used to validate values.

This way, you can bring FluentValidation's out-of-the-box validators or your own custom validators based on the `AbstractValidator` type and get all the benefits of using the existing MADE.NET validation framework. This is great for example with input validator controls that currently support the MADE.NET validation framework!
