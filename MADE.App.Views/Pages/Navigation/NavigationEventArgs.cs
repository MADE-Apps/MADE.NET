namespace MADE.App.Views.Pages.Navigation
{
    using System;

    public class NavigationEventArgs : EventArgs
    {
        public object Parameter { get; set; }

        public Type SourcePageType { get; set; }

        public NavigationMode NavigationMode { get; set; }
    }
}