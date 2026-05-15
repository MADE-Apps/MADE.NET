// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Testing;

/// <summary>
/// Defines a code assertion helper for <see cref="IComparable"/> based scenarios.
/// </summary>
public static class ComparableAssertExtensions
{
    /// <summary>
    /// Tests whether the specified value is greater than the given threshold and throws an exception if it is not.
    /// </summary>
    /// <typeparam name="T">The type of value to compare.</typeparam>
    /// <param name="value">The value to test.</param>
    /// <param name="threshold">The threshold value that the <paramref name="value"/> should be greater than.</param>
    /// <exception cref="AssertFailedException">Thrown if the <paramref name="value"/> is not greater than the <paramref name="threshold"/>.</exception>
    public static void ShouldBeGreaterThan<T>(this T value, T threshold)
        where T : IComparable<T>
    {
        if (value.CompareTo(threshold) <= 0)
        {
            throw new AssertFailedException($"{nameof(ShouldBeGreaterThan)} failed. Expected '{value}' to be greater than '{threshold}'.");
        }
    }

    /// <summary>
    /// Tests whether the specified value is greater than or equal to the given threshold and throws an exception if it is not.
    /// </summary>
    /// <typeparam name="T">The type of value to compare.</typeparam>
    /// <param name="value">The value to test.</param>
    /// <param name="threshold">The threshold value that the <paramref name="value"/> should be greater than or equal to.</param>
    /// <exception cref="AssertFailedException">Thrown if the <paramref name="value"/> is less than the <paramref name="threshold"/>.</exception>
    public static void ShouldBeGreaterThanOrEqualTo<T>(this T value, T threshold)
        where T : IComparable<T>
    {
        if (value.CompareTo(threshold) < 0)
        {
            throw new AssertFailedException($"{nameof(ShouldBeGreaterThanOrEqualTo)} failed. Expected '{value}' to be greater than or equal to '{threshold}'.");
        }
    }

    /// <summary>
    /// Tests whether the specified value is less than the given threshold and throws an exception if it is not.
    /// </summary>
    /// <typeparam name="T">The type of value to compare.</typeparam>
    /// <param name="value">The value to test.</param>
    /// <param name="threshold">The threshold value that the <paramref name="value"/> should be less than.</param>
    /// <exception cref="AssertFailedException">Thrown if the <paramref name="value"/> is not less than the <paramref name="threshold"/>.</exception>
    public static void ShouldBeLessThan<T>(this T value, T threshold)
        where T : IComparable<T>
    {
        if (value.CompareTo(threshold) >= 0)
        {
            throw new AssertFailedException($"{nameof(ShouldBeLessThan)} failed. Expected '{value}' to be less than '{threshold}'.");
        }
    }

    /// <summary>
    /// Tests whether the specified value is less than or equal to the given threshold and throws an exception if it is not.
    /// </summary>
    /// <typeparam name="T">The type of value to compare.</typeparam>
    /// <param name="value">The value to test.</param>
    /// <param name="threshold">The threshold value that the <paramref name="value"/> should be less than or equal to.</param>
    /// <exception cref="AssertFailedException">Thrown if the <paramref name="value"/> is greater than the <paramref name="threshold"/>.</exception>
    public static void ShouldBeLessThanOrEqualTo<T>(this T value, T threshold)
        where T : IComparable<T>
    {
        if (value.CompareTo(threshold) > 0)
        {
            throw new AssertFailedException($"{nameof(ShouldBeLessThanOrEqualTo)} failed. Expected '{value}' to be less than or equal to '{threshold}'.");
        }
    }
}
