// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Converters.Extensions;

/// <summary>
/// Defines a collection of extensions for common mathematics expressions.
/// </summary>
public static class MathExtensions
{
    /// <summary>
    /// Converts a degrees value to a radians value.
    /// </summary>
    /// <param name="degrees">
    /// The degrees value to convert.
    /// </param>
    /// <returns>
    /// The converted value as radians.
    /// </returns>
    public static double ToRadians(this double degrees)
    {
        return degrees * (System.Math.PI / 180);
    }

    /// <summary>
    /// Converts a radians value to a degrees value.
    /// </summary>
    /// <param name="radians">
    /// The radians value to convert.
    /// </param>
    /// <returns>
    /// The converted value as degrees.
    /// </returns>
    public static double ToDegrees(this double radians)
    {
        return radians * (180 / System.Math.PI);
    }
}
