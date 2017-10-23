// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IIOSControl.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for iOS UI elements that use a template to define their appearance when rendered.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.UI.Controls
{
    using System.ComponentModel;

    using UIKit;

    /// <summary>
    /// Defines an interface for iOS UI elements that use a template to define their appearance when rendered.
    /// </summary>
    public interface IIOSControl : IControl, INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the name of the nib to load.
        /// </summary>
        string NibName { get; }

        /// <summary>
        /// Gets the view associated with the root of the control.
        /// </summary>
        UIView Root { get; }

        /// <summary>
        /// Loads the relevant control template so that it's parts can be referenced.
        /// </summary>
        void OnApplyTemplate();
    }
}