// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Testing;

/// <summary>
/// Defines a code assertion helper for string-based scenarios.
/// </summary>
public static class StringAssertExtensions
{
    /// <summary>
    /// Tests whether the specified string contains the given substring and throws an exception if it does not.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <param name="substring">The substring to search for.</param>
    /// <param name="comparisonType">The string comparison type to use. Default is <see cref="StringComparison.Ordinal"/>.</param>
    /// <exception cref="AssertFailedException">Thrown if the <paramref name="value"/> does not contain the <paramref name="substring"/>.</exception>
    public static void ShouldContain(this string? value, string substring, StringComparison comparisonType = StringComparison.Ordinal)
    {
        if (value is null || !value.Contains(substring, comparisonType))
        {
            throw new AssertFailedException($"{nameof(ShouldContain)} failed. Expected '{value ?? "null"}' to contain '{substring}'.");
        }
    }

    /// <summary>
    /// Tests whether the specified string does not contain the given substring and throws an exception if it does.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <param name="substring">The substring to search for.</param>
    /// <param name="comparisonType">The string comparison type to use. Default is <see cref="StringComparison.Ordinal"/>.</param>
    /// <exception cref="AssertFailedException">Thrown if the <paramref name="value"/> contains the <paramref name="substring"/>.</exception>
    public static void ShouldNotContain(this string? value, string substring, StringComparison comparisonType = StringComparison.Ordinal)
    {
        if (value is not null && value.Contains(substring, comparisonType))
        {
            throw new AssertFailedException($"{nameof(ShouldNotContain)} failed. Expected '{value}' to not contain '{substring}'.");
        }
    }

    /// <summary>
    /// Tests whether the specified string starts with the given prefix and throws an exception if it does not.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <param name="prefix">The prefix to search for.</param>
    /// <param name="comparisonType">The string comparison type to use. Default is <see cref="StringComparison.Ordinal"/>.</param>
    /// <exception cref="AssertFailedException">Thrown if the <paramref name="value"/> does not start with the <paramref name="prefix"/>.</exception>
    public static void ShouldStartWith(this string? value, string prefix, StringComparison comparisonType = StringComparison.Ordinal)
    {
        if (value is null || !value.StartsWith(prefix, comparisonType))
        {
            throw new AssertFailedException($"{nameof(ShouldStartWith)} failed. Expected '{value ?? "null"}' to start with '{prefix}'.");
        }
    }

    /// <summary>
    /// Tests whether the specified string ends with the given suffix and throws an exception if it does not.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <param name="suffix">The suffix to search for.</param>
    /// <param name="comparisonType">The string comparison type to use. Default is <see cref="StringComparison.Ordinal"/>.</param>
    /// <exception cref="AssertFailedException">Thrown if the <paramref name="value"/> does not end with the <paramref name="suffix"/>.</exception>
    public static void ShouldEndWith(this string? value, string suffix, StringComparison comparisonType = StringComparison.Ordinal)
    {
        if (value is null || !value.EndsWith(suffix, comparisonType))
        {
            throw new AssertFailedException($"{nameof(ShouldEndWith)} failed. Expected '{value ?? "null"}' to end with '{suffix}'.");
        }
    }
}
