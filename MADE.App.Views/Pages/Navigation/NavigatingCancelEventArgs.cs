namespace MADE.App.Views.Pages.Navigation
{
    using System;

    public class NavigatingCancelEventArgs : NavigationEventArgs
    {
        private readonly Action cancelAction;

        public NavigatingCancelEventArgs(Action cancelAction)
        {
            this.cancelAction = cancelAction;
            this.Cancelled = false;
        }

        public bool Cancelled { get; private set; }

        public void Cancel()
        {
            this.cancelAction?.Invoke();
            this.Cancelled = true;
        }
    }
}