// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWindowsControl.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for Windows UI elements that use a template to define their appearance when rendered.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Controls
{
    using MADE.Controls.Design;

    /// <summary>
    /// Defines an interface for Windows UI elements that use a template to define their appearance when rendered.
    /// </summary>
    public interface IWindowsControl : IControl
    {
        /// <summary>
        /// Gets or sets a color that provides the background of the control.
        /// </summary>
        Color BackgroundColor { get; set; }

        /// <summary>
        /// Retrieves the given named element from the instantiated control template.
        /// </summary>
        /// <param name="name">
        /// The name of the element to find.
        /// </param>
        /// <typeparam name="TElement">
        /// The type of element to retrieve.
        /// </typeparam>
        /// <returns>
        /// Returns the element from the template, if the element is found.
        /// </returns>
        TElement GetTemplateChild<TElement>(string name)
            where TElement : class;
    }
}