// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation.Extensions
{
    /// <summary>
    /// Defines a collection of extensions for common mathematics expressions.
    /// </summary>
    public static class MathExtensions
    {
        /// <summary>
        /// Gets a value for Epsilon.
        /// </summary>
        public static readonly double Epsilon = 2.2204460492503131E-16;

        /// <summary>
        /// Checks whether a double value is zero.
        /// </summary>
        /// <param name="value">
        /// The value to check.
        /// </param>
        /// <returns>
        /// True if zero; otherwise, false.
        /// </returns>
        public static bool IsZero(this double value)
        {
            return System.Math.Abs(value) < Epsilon;
        }

        /// <summary>
        /// Checks whether a float value is zero.
        /// </summary>
        /// <param name="value">
        /// The value to check.
        /// </param>
        /// <returns>
        /// True if zero; otherwise, false.
        /// </returns>
        public static bool IsZero(this float value)
        {
            return System.Math.Abs(value) < Epsilon;
        }

        /// <summary>
        /// Checks whether two values are close in value.
        /// </summary>
        /// <param name="value">
        /// The first value.
        /// </param>
        /// <param name="compare">
        /// The second value.
        /// </param>
        /// <returns>
        /// True if the values are close; otherwise, false.
        /// </returns>
        public static bool IsCloseTo(this int value, int compare)
        {
            if (System.Math.Abs(value - compare) < 1)
            {
                return true;
            }

            double a = (System.Math.Abs(value) + System.Math.Abs(compare) + 10.0) * Epsilon;
            int b = value - compare;
            return -a < b && a > b;
        }

        /// <summary>
        /// Checks whether two values are close in value which have a precision point.
        /// </summary>
        /// <param name="value">
        /// The first value.
        /// </param>
        /// <param name="compare">
        /// The second value.
        /// </param>
        /// <returns>
        /// True if the values are close; otherwise, false.
        /// </returns>
        public static bool IsCloseTo(this double value, double compare)
        {
            if (System.Math.Abs(value - compare) < Epsilon)
            {
                return true;
            }

            double a = (System.Math.Abs(value) + System.Math.Abs(compare) + 10.0) * Epsilon;
            double b = value - compare;
            return -a < b && a > b;
        }

        /// <summary>
        /// Checks whether two values are close in value which have a precision point.
        /// </summary>
        /// <param name="value">
        /// The first value.
        /// </param>
        /// <param name="compare">
        /// The second value.
        /// </param>
        /// <returns>
        /// True if the values are close; otherwise, false.
        /// </returns>
        public static bool IsCloseTo(this double? value, double? compare)
        {
            if (!value.HasValue || !compare.HasValue)
            {
                return false;
            }

            if (System.Math.Abs(value.Value - compare.Value) < Epsilon)
            {
                return true;
            }

            double a = (System.Math.Abs(value.Value) + System.Math.Abs(compare.Value) + 10.0) * Epsilon;
            double? b = value - compare;
            return -a < b && a > b;
        }

        /// <summary>
        /// Checks whether two values are close in value which have a precision point.
        /// </summary>
        /// <param name="value">
        /// The first value.
        /// </param>
        /// <param name="compare">
        /// The second value.
        /// </param>
        /// <returns>
        /// True if the values are close; otherwise, false.
        /// </returns>
        public static bool IsCloseTo(this float value, float compare)
        {
            if (System.Math.Abs(value - compare) < Epsilon)
            {
                return true;
            }

            double a = (System.Math.Abs(value) + System.Math.Abs(compare) + 10.0) * Epsilon;
            float b = value - compare;
            return -a < b && a > b;
        }

        /// <summary>
        /// Checks whether two values are close in value which have a precision point.
        /// </summary>
        /// <param name="value">
        /// The first value.
        /// </param>
        /// <param name="compare">
        /// The second value.
        /// </param>
        /// <returns>
        /// True if the values are close; otherwise, false.
        /// </returns>
        public static bool IsCloseTo(this float? value, float? compare)
        {
            if (!value.HasValue || !compare.HasValue)
            {
                return false;
            }

            if (System.Math.Abs(value.Value - compare.Value) < Epsilon)
            {
                return true;
            }

            double a = (System.Math.Abs(value.Value) + System.Math.Abs(compare.Value) + 10.0) * Epsilon;
            float? b = value - compare;
            return -a < b && a > b;
        }

        /// <summary>
        /// Checks whether a value is significantly greater than another.
        /// </summary>
        /// <param name="value">
        /// The first value.
        /// </param>
        /// <param name="compare">
        /// The second value.
        /// </param>
        /// <returns>
        /// True if the first value is greater than the second; otherwise, false.
        /// </returns>
        public static bool IsGreaterThan(this double value, double compare)
        {
            return value > compare && !value.IsCloseTo(compare);
        }

        /// <summary>
        /// Checks whether a value is significantly less than another.
        /// </summary>
        /// <param name="value">
        /// The first value.
        /// </param>
        /// <param name="compare">
        /// The second value.
        /// </param>
        /// <returns>
        /// True if the first value is less than the second; otherwise, false.
        /// </returns>
        public static bool IsLessThan(this double value, double compare)
        {
            return value < compare && !value.IsCloseTo(compare);
        }
    }
}