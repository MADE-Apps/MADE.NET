// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHeaderedTextBlock.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for a UI element representing read-only text with a header component.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Views
{
    using XPlat.UI.Controls;

    /// <summary>
    /// Defines an interface for a UI element representing read-only text with a header component.
    /// </summary>
    public interface IHeaderedTextBlock
    {
        /// <summary>
        /// Gets or sets the string associated with the header.
        /// </summary>
        string Header { get; set; }

        /// <summary>
        /// Gets or sets the string associated with the text.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Gets or sets the orientation the header and text should layout as.
        /// </summary>
        Orientation Orientation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to hide the control if the <see cref="Text"/> is null or whitespace.
        /// </summary>
        bool HideIfNullOrWhiteSpace { get; set; }

        /// <summary>
        /// Updates the layout for the control based on the current <see cref="Orientation"/> value.
        /// </summary>
        void UpdateOrientation();

        /// <summary>
        /// Updates the visibility of the control based on the values of the <see cref="Header"/> and <see cref="Text"/> properties.
        /// </summary>
        void UpdateVisibility();
    }
}