// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Converters.Extensions
{
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
    }
}