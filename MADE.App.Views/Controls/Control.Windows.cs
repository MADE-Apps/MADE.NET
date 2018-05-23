#if WINDOWS_UWP
namespace MADE.App.Views.Controls
{
    using MADE.App.Design.Color;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Media;

    using MADE.App.Views.Extensions;

    public class Control : Windows.UI.Xaml.Controls.Control, IControl
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
        /// Gets or sets a color that provides the background of the view.
        /// </summary>
        public Color BackgroundColor
        {
            get => this.Background as SolidColorBrush;
            set => this.Background = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the view is visible in the UI.
        /// </summary>
        public bool IsVisible
        {
            get => this.Visibility == Visibility.Visible;
            set => this.SetVisible(value);
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
#endif