namespace MADE.Samples.Infrastructure.Styling
{
    using Windows.UI.Xaml;

    public static class ThemeHelper
    {
        public static ElementTheme CurrentTheme
        {
            get
            {
                if (Window.Current.Content is FrameworkElement rootElement)
                {
                    if (rootElement.RequestedTheme != ElementTheme.Default)
                    {
                        return rootElement.RequestedTheme;
                    }
                }

                return Application.Current.RequestedTheme switch
                {
                    ApplicationTheme.Light => ElementTheme.Light,
                    ApplicationTheme.Dark => ElementTheme.Dark,
                    _ => ElementTheme.Default
                };
            }
        }
    }
}
