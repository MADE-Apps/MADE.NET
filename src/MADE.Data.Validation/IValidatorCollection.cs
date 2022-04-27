// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Defines an interface for a collection of validators.
    /// </summary>
    public interface IValidatorCollection : IList
    {
        /// <summary>
        /// Gets or sets a value indicating whether the data provided is in an invalid state.
        /// </summary>
        bool IsInvalid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the data is dirty.
        /// </summary>
        bool IsDirty { get; set; }

        /// <summary>
        /// Gets the validator feedback messages for ones which are invalid.
        /// </summary>
        IEnumerable<string> FeedbackMessages { get; }

        /// <summary>
        /// Executes data validation on the provided <paramref name="value"/> against the validators provided.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        void Validate(object value);
    }
}