// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValidationRules.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for a container for IValidationRule objects.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Data.Validation.Rules
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines an interface for a container for <see cref="IValidationRule"/> objects.
    /// </summary>
    public interface IValidationRules : IValidationRule
    {
        /// <summary>
        /// Gets the collection of registered data validation rules.
        /// </summary>
        List<IValidationRule> Rules { get; }
    }
}