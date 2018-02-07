// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValidatingControl.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for UI elements that use validation on an input value.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.UI.Controls
{
    using System.Windows.Input;

    /// <summary>
    /// Defines an interface for UI elements that use validation on an input value.
    /// </summary>
    public interface IValidatingControl : IControl
    {
        /// <summary>
        /// The event associated with the control's validation being updated.
        /// </summary>
        event ValidationUpdatedEventHandler ValidationUpdated;

        /// <summary>
        /// Gets or sets the command called when the control's validation has updated.
        /// </summary>
        ICommand ValidationUpdatedCommand { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the control is mandatory, requiring a value.
        /// </summary>
        bool IsMandatory { get; set; }

        /// <summary>
        /// Gets or sets the message displayed when the mandatory validation is not met.
        /// </summary>
        string MandatoryValidationMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the control is currently in a valid state.
        /// </summary>
        bool IsControlValid { get; set; }

        /// <summary>
        /// Re-checks and updates the current valid state of the control.
        /// </summary>
        void Update();
    }
}