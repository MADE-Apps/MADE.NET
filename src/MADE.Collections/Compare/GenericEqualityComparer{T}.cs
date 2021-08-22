// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Collections.Compare
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines an equality comparer for comparing two objects using a simple comparison function.
    /// </summary>
    /// <typeparam name="T">
    /// The type of object to comparison.
    /// </typeparam>
    public class GenericEqualityComparer<T> : IEqualityComparer<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericEqualityComparer{T}"/> class.
        /// </summary>
        /// <param name="comparison">
        /// The comparison expression.
        /// </param>
        public GenericEqualityComparer(Func<T, object> comparison)
        {
            this.Comparison = comparison;
        }

        private Func<T, object> Comparison { get; }

        /// <summary>
        /// Compares two objects of the same type for equality.
        /// </summary>
        /// <param name="x">
        /// The first item.
        /// </param>
        /// <param name="y">
        /// The second item.
        /// </param>
        /// <returns>
        /// True if the two items are equal based on the comparison expression; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.Exception">The <see cref="Comparison"/> callback throws an exception.</exception>
        public bool Equals(T x, T y)
        {
            object first = this.Comparison.Invoke(x);
            object second = this.Comparison.Invoke(y);

            return first != null && first.Equals(second);
        }

        /// <summary>
        /// Gets the hash code for the expected comparison object.
        /// </summary>
        /// <param name="obj">
        /// The object to get the comparison object hash code for.
        /// </param>
        /// <returns>
        /// A hash code for the comparison object.
        /// </returns>
        /// <exception cref="T:System.Exception">The <see cref="Comparison"/> callback throws an exception.</exception>
        public int GetHashCode(T obj)
        {
            return this.Comparison.Invoke(obj).GetHashCode();
        }
    }
}