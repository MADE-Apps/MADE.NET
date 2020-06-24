// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IControl.Windows.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for Windows components of a common application control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if WINDOWS_UWP
namespace MADE.App.Views.Controls
{
    /// <summary>
    /// Defines an interface for Windows components of a common application control.
    /// </summary>
    public partial interface IControl
    {
        /// <summary>
        /// Retrieves the element from the instantiated view by the given resource identifier.
        /// </summary>
        /// <param name="resourceName">
        /// The name of the resource to find.
        /// </param>
        /// <typeparam name="TView">
        /// The type of view to retrieve.
        /// </typeparam>
        /// <returns>
        /// Returns the view from the layout, if the view is found.
        /// </returns>
        TView GetChildView<TView>(string resourceName)
            where TView : class;
    }
}
#endif