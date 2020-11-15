// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation
{
    /// <summary>
    /// Defines an interface for a data validator.
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Gets or sets the key associated with the validator.
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the data provided is in an invalid state.
        /// </summary>
        bool IsInvalid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the data is dirty.
        /// </summary>
        bool IsDirty { get; set; }

        /// <summary>
        /// Executes data validation on the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        void Validate(object value);
    }
}