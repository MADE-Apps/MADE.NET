// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValidationRule.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for a data validation rule.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Data.Validation.Rules
{
    /// <summary>
    /// Defines an interface for a data validation rule.
    /// </summary>
    public interface IValidationRule
    {
        /// <summary>
        /// Gets or sets the error message to display when the validation is not met.
        /// </summary>
        string ErrorMessage { get; set; }

        /// <summary>
        /// Checks whether the given value is valid according to the rule.
        /// </summary>
        /// <param name="value">
        /// The value to validate.
        /// </param>
        /// <returns>
        /// Returns true if the value is valid.
        /// </returns>
        bool IsValid(object value);
    }
}