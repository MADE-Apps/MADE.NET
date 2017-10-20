// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAndroidControl.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for Android UI elements that use a template to define their appearance when rendered.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Controls
{
    using Android.Views;

    /// <summary>
    /// Defines an interface for Android UI elements that use a template to define their appearance when rendered.
    /// </summary>
    public interface IAndroidControl : IControl
    {
        /// <summary>
        /// Gets the reference identifier for the control's layout.
        /// </summary>
        int LayoutReference { get; }

        /// <summary>
        /// Gets the view associated with the inflated layout.
        /// </summary>
        View View { get; }

        /// <summary>
        /// Retrieves the element from the instantiated control template by the given resource identifier.
        /// </summary>
        /// <param name="resourceId">
        /// The element resource identifier.
        /// </param>
        /// <typeparam name="TElement">
        /// The type of element to retrieve.
        /// </typeparam>
        /// <returns>
        /// Returns the element from the template, if the element is found.
        /// </returns>
        TElement GetTemplateChild<TElement>(int resourceId)
            where TElement : View;
    }
}