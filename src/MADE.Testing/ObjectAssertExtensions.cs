// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Testing;

/// <summary>
/// Defines a code assertion helper for object-based scenarios.
/// </summary>
public static class ObjectAssertExtensions
{
    /// <summary>
    /// Tests whether the specified value is null and throws an exception if it is not null.
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <exception cref="AssertFailedException">Thrown if the <paramref name="value"/> is not null.</exception>
    public static void ShouldBeNull(this object? value)
    {
        if (value is not null)
        {
            throw new AssertFailedException($"{nameof(ShouldBeNull)} failed. Expected null but was '{value}'.");
        }
    }

    /// <summary>
    /// Tests whether the specified value is not null and throws an exception if it is null.
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <exception cref="AssertFailedException">Thrown if the <paramref name="value"/> is null.</exception>
    public static void ShouldNotBeNull(this object? value)
    {
        if (value is null)
        {
            throw new AssertFailedException($"{nameof(ShouldNotBeNull)} failed. Expected a non-null value.");
        }
    }
}
