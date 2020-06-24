// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IView.Windows.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for Windows components of a common application user interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if WINDOWS_UWP
namespace MADE.App.Views
{
    using XPlat.UI;

    /// <summary>
    /// Defines an interface for Windows components of a common application user interface.
    /// </summary>
    public partial interface IView
    {
        /// <summary>
        /// Gets or sets a color that provides the background of the view.
        /// </summary>
        Color BackgroundColor { get; set; }
    }
}
#endif