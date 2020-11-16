// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI
{
    using System;

    /// <summary>
    /// Defines an interface for a common application user interface.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Occurs when the view has loaded.
        /// </summary>
        event ViewLoadedEventHandler ViewLoaded;

        /// <summary>
        /// Occurs when the <see cref="IsVisible"/> state has changed.
        /// </summary>
        event EventHandler<bool> IsVisibleChanged;

        /// <summary>
        /// Gets or sets a value indicating whether the view is enabled and can be interacted with.
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the view is visible in the UI.
        /// </summary>
        bool IsVisible { get; set; }
    }
}