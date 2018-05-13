#if __ANDROID__
namespace MADE.App.Views.Navigation
{
    using System;
    using System.Collections.Generic;

    using Android.OS;
    using Android.Support.V4.App;
    using Android.Support.V7.App;

    public class NavigationFrame : AppCompatActivity, INavigationFrame
    {
        private Page currentPage;

        private readonly Dictionary<int, NavigationEventArgs> navigationEvents;

        public NavigationFrame()
        {
            this.navigationEvents = new Dictionary<int, NavigationEventArgs>();
        }

        public event NavigationEventHandler PageNavigated;

        public bool CanGoBack => this.BackStackDepth > 0;

        public Type CurrentSourcePageType => this.currentPage.GetType();

        public int BackStackDepth => this.SupportFragmentManager.BackStackEntryCount;

        public int FrameLayoutId { get; set; }

        public int FragmentFrameLayoutId { get; set; }

        public void GoBack()
        {
            if (this.CanGoBack)
            {
                if (!this.CanNavigateAway())
                {
                    return;
                }

                NavigationEventArgs currentPageEvent = this.GetNavigationEvent(this.BackStackDepth);
                currentPageEvent.NavigationMode = NavigationMode.Back;

                NavigationEventArgs previousPageEvent = this.GetNavigationEvent(this.BackStackDepth - 1);
                previousPageEvent.NavigationMode = NavigationMode.Back;

                this.HandleNavigation(
                    currentPageEvent,
                    previousPageEvent,
                    () =>
                        {
                            this.RemoveNavigationEvent(this.BackStackDepth);

                            this.SupportFragmentManager.PopBackStackImmediate();
                        });
            }
        }

        public bool Navigate(Type sourcePageType)
        {
            return this.Navigate(sourcePageType, null);
        }

        public bool Navigate(Type sourcePageType, object parameter)
        {
            if (!this.CanNavigateAway())
            {
                return false;
            }

            if (!(Activator.CreateInstance(sourcePageType) is Page page))
            {
                return false;
            }

            NavigationEventArgs navArgs = new NavigationEventArgs
                                              {
                                                  NavigationMode = NavigationMode.New,
                                                  Parameter = parameter,
                                                  SourcePageType = sourcePageType
                                              };

            this.HandleNavigation(
                navArgs,
                () =>
                    {
                        FragmentTransaction transaction = this.SupportFragmentManager.BeginTransaction();
                        transaction.Replace(this.FragmentFrameLayoutId, page, sourcePageType.FullName);
                        transaction.AddToBackStack(sourcePageType.FullName);
                        transaction.Commit();
                        this.SupportFragmentManager.ExecutePendingTransactions();

                        this.AddOrUpdateNavigationEvent(navArgs);
                    });

            return true;
        }

        public override void OnBackPressed()
        {
            this.GoBack();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(this.FrameLayoutId);
        }

        private void HandleNavigation(NavigationEventArgs navArgs, Action navigationAction)
        {
            this.HandleNavigation(navArgs, navArgs, navigationAction);
        }

        private void HandleNavigation(
            NavigationEventArgs previousNavArgs,
            NavigationEventArgs newNavArgs,
            Action navigationAction)
        {
            Page previousPage = this.currentPage;
            previousPage?.OnNavigatedFrom(previousNavArgs);

            navigationAction?.Invoke();

            this.currentPage = this.SupportFragmentManager.GetCurrentFragment() as Page;
            this.currentPage?.OnNavigatedTo(newNavArgs);

        }

        private NavigationEventArgs GetNavigationEvent(int key)
        {
            if (this.navigationEvents.ContainsKey(key))
            {
                return this.navigationEvents[key];
            }

            return null;
        }

        private void AddOrUpdateNavigationEvent(NavigationEventArgs navArgs)
        {
            this.RemoveNavigationEvent(this.BackStackDepth);

            if (navArgs != null)
            {
                this.navigationEvents.Add(this.BackStackDepth, navArgs);
            }
        }

        private void RemoveNavigationEvent(int key)
        {
            if (this.navigationEvents.ContainsKey(key))
            {
                this.navigationEvents.Remove(key);
            }
        }

        private bool CanNavigateAway()
        {
            Page previousPage = this.currentPage;

            bool shouldCancel = false;

            NavigatingCancelEventArgs navArgs =
                new NavigatingCancelEventArgs(() => shouldCancel = true)
                    {
                        SourcePageType = previousPage?.GetType(),
                        NavigationMode = NavigationMode.Back
                    };

            previousPage?.OnNavigatingFrom(navArgs);

            if (shouldCancel)
            {
                return false;
            }

            return true;
        }
    }
}
#endif