// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IControl.iOS.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for iOS components of a common application control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if __IOS__
namespace MADE.App.Views.Controls
{
    using System.ComponentModel;

    using UIKit;

    /// <summary>
    /// Defines an interface for iOS components of a common application control.
    /// </summary>
    public partial interface IControl : IView, INotifyPropertyChanged
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
#endif