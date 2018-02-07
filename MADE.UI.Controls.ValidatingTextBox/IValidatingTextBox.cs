// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValidatingTextBox.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for a UI element allowing the user to input a value and have real-time validation run against it.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.UI.Controls
{
    using MADE.Data.Validation.Rules;

    /// <summary>
    /// Defines an interface for a UI element allowing the user to input a value and have real-time validation run against it.
    /// </summary>
    public interface IValidatingTextBox : IValidatingControl
    {
        /// <summary>
        /// Gets or sets the validation rules to run against the value of the text box.
        /// </summary>
        ValidationRules ValidationRules { get; set; }
    }
}