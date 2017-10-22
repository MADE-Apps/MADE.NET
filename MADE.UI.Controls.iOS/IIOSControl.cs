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
        /// Gets or sets the root view.
        /// </summary>
        UIView RootView { get; set; }

        /// <summary>
        /// Loads the relevant control template so that it's parts can be referenced.
        /// </summary>
        void OnApplyTemplate();

        /// <summary>
        /// Cleans up the designer outlets.
        /// </summary>
        void ReleaseDesignerOutlets();
    }
}