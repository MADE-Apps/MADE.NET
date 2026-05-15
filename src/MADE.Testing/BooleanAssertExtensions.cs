// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Testing;

/// <summary>
/// Defines a code assertion helper for boolean-based scenarios.
/// </summary>
public static class BooleanAssertExtensions
{
    /// <summary>
    /// Tests whether the specified value is true and throws an exception if it is false.
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <exception cref="AssertFailedException">Thrown if the <paramref name="value"/> is false.</exception>
    public static void ShouldBeTrue(this bool value)
    {
        if (!value)
        {
            throw new AssertFailedException($"{nameof(ShouldBeTrue)} failed. Expected true but was false.");
        }
    }

    /// <summary>
    /// Tests whether the specified value is false and throws an exception if it is true.
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <exception cref="AssertFailedException">Thrown if the <paramref name="value"/> is true.</exception>
    public static void ShouldBeFalse(this bool value)
    {
        if (value)
        {
            throw new AssertFailedException($"{nameof(ShouldBeFalse)} failed. Expected false but was true.");
        }
    }
}
