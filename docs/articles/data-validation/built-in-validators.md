---
uid: data-validation-built-in-validators
title: Built-in validators
---

# Built-in validators

`MADE.Data.Validation` ships with over 30 validators covering the most common validation scenarios. Each validator implements `IValidator` and can be used independently or composed into a `ValidatorCollection`.

Every validator exposes the same properties:

| Property | Description |
| --- | --- |
| `Key` | A string identifier for the validator (defaults to the class name). |
| `IsInvalid` | `true` if the last validation failed. |
| `IsDirty` | `true` if `Validate` has been called at least once. |
| `FeedbackMessage` | A human-readable error message. Customizable via the property setter. |

## String validators

### RequiredValidator

Ensures a value is not null, not an empty string, not whitespace, not an empty collection, and not `false`:

```csharp
var validator = new RequiredValidator();
validator.Validate(null);       // IsInvalid = true
validator.Validate("");         // IsInvalid = true
validator.Validate("  ");      // IsInvalid = true
validator.Validate("hello");   // IsInvalid = false
```

### AlphaValidator

Checks that a string contains only letter characters (a-z, case-insensitive):

```csharp
var validator = new AlphaValidator();
validator.Validate("Hello");    // IsInvalid = false
validator.Validate("Hello123"); // IsInvalid = true
```

### AlphaNumericValidator

Checks that a string contains only letters and numbers:

```csharp
var validator = new AlphaNumericValidator();
validator.Validate("Hello123"); // IsInvalid = false
validator.Validate("Hello!");   // IsInvalid = true
```

### MinLengthValidator

Validates that a string meets a minimum length:

```csharp
var validator = new MinLengthValidator { MinLength = 3 };
validator.Validate("Hi");    // IsInvalid = true
validator.Validate("Hello"); // IsInvalid = false
```

### MaxLengthValidator

Validates that a string doesn't exceed a maximum length:

```csharp
var validator = new MaxLengthValidator { MaxLength = 10 };
validator.Validate("Hello");          // IsInvalid = false
validator.Validate("Hello, World!"); // IsInvalid = true
```

### RegexValidator

Validates a value against a configurable regular expression:

```csharp
var validator = new RegexValidator { Pattern = @"^\d{3}-\d{4}$" };
validator.Validate("123-4567"); // IsInvalid = false
validator.Validate("abc");      // IsInvalid = true
```

### Base64Validator

Checks whether a string is valid Base64:

```csharp
var validator = new Base64Validator();
validator.Validate("SGVsbG8="); // IsInvalid = false
validator.Validate("not base64!"); // IsInvalid = true
```

## Format validators

### EmailValidator

Validates that a string is a valid email address. Extends `RegexValidator` with a comprehensive email pattern:

```csharp
var validator = new EmailValidator();
validator.Validate("user@example.com"); // IsInvalid = false
validator.Validate("not-an-email");     // IsInvalid = true
```

### IpAddressValidator

Validates that a value is a valid IPv4 address:

```csharp
var validator = new IpAddressValidator();
validator.Validate("192.168.1.1");   // IsInvalid = false
validator.Validate("999.999.999.999"); // IsInvalid = true
```

### MacAddressValidator

Validates that a value is a valid MAC address using .NET's `PhysicalAddress.Parse`:

```csharp
var validator = new MacAddressValidator();
validator.Validate("00:1A:2B:3C:4D:5E"); // IsInvalid = false
```

### GuidValidator

Checks whether a string is a valid GUID:

```csharp
var validator = new GuidValidator();
validator.Validate("550e8400-e29b-41d4-a716-446655440000"); // IsInvalid = false
validator.Validate("not-a-guid"); // IsInvalid = true
```

### WellFormedUrlValidator

Validates that a string is a well-formed URL:

```csharp
var validator = new WellFormedUrlValidator();
validator.Validate("https://example.com"); // IsInvalid = false
validator.Validate("not a url");           // IsInvalid = true
```

## Range validators

### BetweenValidator

Validates that an `IComparable` value falls within a range:

```csharp
var validator = new BetweenValidator { Min = 1, Max = 100, Inclusive = true };
validator.Validate(50);  // IsInvalid = false
validator.Validate(0);   // IsInvalid = true
validator.Validate(100); // IsInvalid = false (inclusive)
```

### MinValueValidator

Validates that a value is greater than a minimum:

```csharp
var validator = new MinValueValidator { Min = 0 };
validator.Validate(5);  // IsInvalid = false
validator.Validate(-1); // IsInvalid = true
```

### MaxValueValidator

Validates that a value is less than a maximum:

```csharp
var validator = new MaxValueValidator { Max = 100 };
validator.Validate(50);  // IsInvalid = false
validator.Validate(150); // IsInvalid = true
```

### LatitudeValidator

Validates that a value is within the valid latitude range (-90 to 90):

```csharp
var validator = new LatitudeValidator();
validator.Validate(51.5074); // IsInvalid = false (London)
validator.Validate(95.0);    // IsInvalid = true
```

### LongitudeValidator

Validates that a value is within the valid longitude range (-180 to 180):

```csharp
var validator = new LongitudeValidator();
validator.Validate(-0.1278); // IsInvalid = false (London)
validator.Validate(200.0);   // IsInvalid = true
```

## Custom predicate validation

### PredicateValidator

Validates using a custom function, useful for one-off validation logic:

```csharp
var validator = new PredicateValidator<string>
{
    Predicate = value => value?.Contains("@") == true,
    FeedbackMessage = "Value must contain @",
};
validator.Validate("test@example"); // IsInvalid = false
validator.Validate("test");         // IsInvalid = true
```
