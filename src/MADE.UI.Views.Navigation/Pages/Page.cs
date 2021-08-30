namespace MADE.UI.Views.Navigation.Pages
{
    using System;
    using MADE.UI.Extensions;
    using Windows.ApplicationModel;
    using Windows.UI.Xaml;

    /// <summary>
    /// Defines an extended page implementation.
    /// </summary>
    public partial class Page : Windows.UI.Xaml.Controls.Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Page"/> class.
        /// </summary>
        public Page()
        {
            if (DesignMode.DesignModeEnabled)
            {
                return;
            }

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
        /// Called when the page has loaded.
        /// </summary>
        public virtual void OnPageLoaded()
        {
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= this.OnLoaded;

            this.ViewLoaded?.Invoke(this, new ViewLoadedEventArgs());
            this.OnPageLoaded();
        }
    }
}