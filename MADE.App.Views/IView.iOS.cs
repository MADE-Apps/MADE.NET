// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IView.iOS.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for iOS components of a common application user interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if __IOS__
namespace MADE.App.Views
{
    using System;

    /// <summary>
    /// Defines an interface for iOS components of a common application user interface.
    /// </summary>
    public partial interface IView
    {
        /// <summary>
        /// Occurs when the <see cref="IsEnabled"/> state has changed.
        /// </summary>
        event EventHandler<bool> IsEnabledChanged;
    }
}
#endif