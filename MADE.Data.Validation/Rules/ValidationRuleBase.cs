// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationRuleBase.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a base class for a data validation rule.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Data.Validation.Rules
{
    using MADE.Data.Validation.Strings;

    /// <summary>
    /// Defines a base class for a data validation rule.
    /// </summary>
    public abstract class ValidationRuleBase : IValidationRule
    {
        private string errorMessage;

        /// <summary>
        /// Gets or sets the error message to display when the validation is not met.
        /// </summary>
        public string ErrorMessage
        {
            get => string.IsNullOrWhiteSpace(this.errorMessage)
                           ? Resources.ValidationRule_DefaultErrorMessage
                           : this.errorMessage;
            set => this.errorMessage = value;
        }

        /// <summary>
        /// Checks whether the given value is valid according to the rule.
        /// </summary>
        /// <param name="value">
        /// The value to validate.
        /// </param>
        /// <returns>
        /// Returns true if the value is valid.
        /// </returns>
        public abstract bool IsValid(object value);
    }
}