---
uid: data-converters-string-extensions
title: String extensions
---

# String extensions

The `MADE.Data.Converters.Extensions.StringExtensions` class provides extension methods for common string manipulation and parsing operations. These cover the transformations you'd otherwise implement as utility methods in every project.

## Text formatting

### ToTitleCase

Converts a string to title case (capitalizing the first letter of each word):

```csharp
string title = "hello world".ToTitleCase(); // "Hello World"
```

### ToDefaultCase

Converts a string to lowercase:

```csharp
string lower = "HELLO".ToDefaultCase(); // "hello"
```

### Truncate

Truncates a string to a specified maximum length:

```csharp
string preview = "This is a long description of a product".Truncate(20);
// "This is a long descr"
```

## URL slug generation with ToSlug

Converts a string to a URL-friendly slug by removing diacritics, replacing non-alphanumeric characters with hyphens, and lowercasing. This is essential for generating clean URLs from user input or content titles.

```csharp
string slug = "How To Build & Publish NuGet Packages!".ToSlug();
// "how-to-build-publish-nuget-packages"

string international = "Cafe\u0301 Résumé".ToSlug();
// "cafe-resume"
```

The slug generator handles Unicode diacritics correctly, collapsing accented characters to their base forms.

## Base64 encoding

Convert strings to and from Base64:

```csharp
string encoded = "Hello, World!".ToBase64();     // "SGVsbG8sIFdvcmxkIQ=="
string decoded = encoded.FromBase64();             // "Hello, World!"
```

## Safe numeric parsing

The standard `int.Parse` and `double.Parse` methods throw exceptions on invalid input. These extensions return `null` instead, which is cleaner when you're dealing with user input or configuration values that might not be valid:

```csharp
// Returns null instead of throwing on invalid input
int? count = "42".ToNullableInt();        // 42
int? invalid = "not-a-number".ToNullableInt(); // null

float? price = "19.99".ToNullableFloat();  // 19.99f
double? rate = "0.05".ToNullableDouble();  // 0.05

// Non-nullable variants (return 0 on failure)
int count = "42".ToInt();           // 42
float price = "19.99".ToFloat();    // 19.99f
double rate = "0.05".ToDouble();    // 0.05
```

### Boolean parsing with ToBoolean

Parses a string to a boolean value:

```csharp
bool isEnabled = "true".ToBoolean();  // true
bool isActive = "false".ToBoolean();  // false
```

## Stream conversion with ToMemoryStreamAsync

Converts a string to a `MemoryStream`, useful when an API expects a stream but you have string content:

```csharp
using var stream = await jsonContent.ToMemoryStreamAsync();
await blobClient.UploadAsync(stream);
```
