// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INumericValidationRule.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for a data validation rule which accepts a numeric value.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Data.Validation.Rules
{
    /// <summary>
    /// Defines an interface for a data validation rule which accepts a numeric value.
    /// </summary>
    public interface INumericValidationRule : IValidationRule
    {
        /// <summary>
        /// Gets or sets the numeric validation rule setting for supporting all, only positive or only negative values as valid.
        /// </summary>
        NumericValidationRuleSetting NumericValidationSetting { get; set; }
    }
}