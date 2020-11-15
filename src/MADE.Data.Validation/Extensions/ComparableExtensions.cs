namespace MADE.Data.Validation.Extensions
{
    using System;

    /// <summary>
    /// Defines a collection of extensions for <see cref="IComparable"/> objects.
    /// </summary>
    public static class ComparableExtensions
    {
        /// <summary>
        /// Determines whether the value is greater than the <paramref name="other"/> value.
        /// </summary>
        /// <typeparam name="T">The <see cref="IComparable"/> type.</typeparam>
        /// <param name="value">The value to compare.</param>
        /// <param name="other">The value to compare against.</param>
        /// <returns>True if the <paramref name="value"/> is greater than the <paramref name="other"/> value.</returns>
        public static bool IsGreaterThan<T>(this T value, T other)
            where T : IComparable
        {
            return value.CompareTo(other) > 0;
        }

        /// <summary>
        /// Determines whether the value is less than the <paramref name="other"/> value.
        /// </summary>
        /// <typeparam name="T">The <see cref="IComparable"/> type.</typeparam>
        /// <param name="value">The value to compare.</param>
        /// <param name="other">The value to compare against.</param>
        /// <returns>True if the <paramref name="value"/> is less than the <paramref name="other"/> value.</returns>
        public static bool IsLessThan<T>(this T value, T other)
            where T : IComparable
        {
            return value.CompareTo(other) < 0;
        }
    }
}