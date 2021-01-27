// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.Controls
{
    using System;

    using MADE.UI;
    using MADE.UI.Extensions;

    using Windows.UI.Xaml;

    /// <summary>
    /// Defines a UI element for creating custom controls in Windows applications.
    /// </summary>
    public partial class Control : Windows.UI.Xaml.Controls.Control, IControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Control"/> class.
        /// </summary>
        public Control()
        {
            this.Loaded += this.OnLoaded;
        }

        /// <summary>
        /// Occurs when the view has loaded.
        /// </summary>
        public event ViewLoadedEventHandler ViewLoaded;

        /// <summary>
        /// Occurs when the <see cref="IsVisible"/> state has changed.
        /// </summary>
        public event EventHandler<bool> IsVisibleChanged;

        /// <summary>
        /// Gets or sets a value indicating whether the view is visible in the UI.
        /// </summary>
        /// <exception cref="T:System.Exception" accessor="set">A delegate callback throws an exception.</exception>
        public bool IsVisible
        {
            get => this.Visibility == Visibility.Visible;
            set
            {
                this.SetVisible(value);
                this.IsVisibleChanged?.Invoke(this, value);
            }
        }

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
        public TView GetChildView<TView>(string resourceName)
            where TView : class
        {
            return this.GetTemplateChild(resourceName) as TView;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= this.OnLoaded;

            ViewLoadedEventHandler handler = this.ViewLoaded;
            handler?.Invoke(this, new ViewLoadedEventArgs());
        }
    }
}