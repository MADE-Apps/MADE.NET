// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IControl.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for a control built with custom unified logic.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.UI.Controls
{
    /// <summary>
    /// Defines an interface for UI elements that use a template to define their appearance when rendered.
    /// </summary>
    public interface IControl
    {
        /// <summary>
        /// The event associated with the control being loaded.
        /// </summary>
        event ControlLoadedEventHandler ControlLoaded;

        /// <summary>
        /// Gets or sets a value indicating whether the user can interact with the control.
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the control is visible to the user.
        /// </summary>
        bool IsVisible { get; set; }
    }
}