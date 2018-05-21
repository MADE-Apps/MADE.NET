#if __ANDROID__
namespace MADE.App.Views.Navigation
{
    using System;
    using System.Collections.Generic;

    using Android.OS;
    using Android.Support.V4.App;
    using Android.Support.V7.App;

    /// <summary>
    /// Defines a frame for navigating and displaying page content.
    /// </summary>
    public class NavigationFrame : AppCompatActivity, INavigationFrame
    {
        private IPage currentPage;

        private readonly Dictionary<int, NavigationEventArgs> navigationEvents;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationFrame"/> class.
        /// </summary>
        public NavigationFrame() : this(0,0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationFrame"/> class.
        /// </summary>
        /// <param name="frameLayoutId">The ID of the layout for the frame.</param>
        /// <param name="frameFragmentContentId">The ID of the frame layout to update when the frame navigates to a fragment.</param>
        public NavigationFrame(int frameLayoutId, int frameFragmentContentId)
        {
            this.navigationEvents = new Dictionary<int, NavigationEventArgs>();

            this.FrameLayoutId = frameLayoutId;
            this.FrameFragmentContentId = frameFragmentContentId;
        }

        /// <summary>
        /// Occurs when the content that is being navigated to has been found and is available from the Content property, although it may not have completed loading.
        /// </summary>
        public event NavigationEventHandler PageNavigated;

        /// <summary>
        /// Gets a value that indicates whether there is at least one entry in back navigation history.
        /// </summary>
        public bool CanGoBack => this.BackStackDepth > 0;

        /// <summary>
        /// Gets a type reference for the content that is currently displayed.
        /// </summary>
        public Type CurrentSourcePageType => this.currentPage.GetType();

        /// <summary>
        /// Gets the parameter passed to the current source page on navigated to.
        /// </summary>
        public object CurrentSourcePageParameter { get; private set; }

        /// <summary>
        /// Gets the number of entries in the navigation back stack.
        /// </summary>
        public int BackStackDepth => this.SupportFragmentManager.BackStackEntryCount;

        /// <summary>
        /// Gets or sets the ID of the layout for the frame.
        /// </summary>
        public int FrameLayoutId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the frame layout to update when the frame navigates to a fragment.
        /// </summary>
        public int FrameFragmentContentId { get; set; }

        /// <summary>
        /// Navigates to the most recent item in the back navigation history.
        /// </summary>
        public void GoBack()
        {
            if (!this.CanGoBack)
            {
                return;
            }

            if (!this.CanNavigateAway())
            {
                return;
            }

            NavigationEventArgs currentPageEvent = this.GetNavigationEvent(this.BackStackDepth);
            if (currentPageEvent != null)
            {
                currentPageEvent.NavigationMode = NavigationMode.Back;
            }

            NavigationEventArgs previousPageEvent = this.GetNavigationEvent(this.BackStackDepth - 1);
            if (previousPageEvent != null)
            {
                previousPageEvent.NavigationMode = NavigationMode.Back;
            }

            this.HandleNavigation(
                currentPageEvent,
                previousPageEvent,
                () =>
                    {
                        this.RemoveNavigationEvent(this.BackStackDepth);
                        this.SupportFragmentManager.PopBackStackImmediate();
                    });
        }

        /// <summary>
        /// Performs a new navigation within the Frame to the Page associated by the given source page type.
        /// </summary>
        /// <param name="sourcePageType">
        /// The type associated with the source page to navigate to.
        /// </param>
        /// <returns>
        /// Returns true if the navigation is successfully; otherwise, false.
        /// </returns>
        public bool Navigate(Type sourcePageType)
        {
            return this.Navigate(sourcePageType, null);
        }

        /// <summary>
        /// Performs a new navigation within the Frame to the Page associated by the given source page type, passing a parameter on navigation.
        /// </summary>
        /// <param name="sourcePageType">
        /// The type associated with the source page to navigate to.
        /// </param>
        /// <param name="parameter">
        /// The parameter to pass to the page on navigated to.
        /// </param>
        /// <returns>
        /// Returns true if the navigation is successfully; otherwise, false.
        /// </returns>
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
                        transaction.Replace(this.FrameFragmentContentId, page, sourcePageType.FullName);
                        transaction.AddToBackStack(sourcePageType.FullName);
                        transaction.Commit();
                        this.SupportFragmentManager.ExecutePendingTransactions();

                        this.AddOrUpdateNavigationEvent(navArgs);

                        this.CurrentSourcePageParameter = navArgs.Parameter;
                    });

            return true;
        }

        /// <summary>
        /// Called when the activity has detected the user's press of the back key.
        /// </summary>
        public override void OnBackPressed()
        {
            this.GoBack();
        }

        /// <summary>
        /// Called when the activity is starting.
        /// </summary>
        /// <param name="savedInstanceState">
        /// If the activity is being re-initialized after previously being shut down then this Bundle contains the data it most recently supplied in OnSaveInstanceState(Android.OS.Bundle).
        /// </param>
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
            IPage previousPage = this.currentPage;
            previousPage?.OnNavigatedFrom(previousNavArgs);

            navigationAction?.Invoke();

            this.currentPage = this.SupportFragmentManager.GetCurrentFragment() as Page;
            if (this.currentPage != null)
            {
                this.currentPage.OnNavigatedTo(newNavArgs);
                this.CurrentSourcePageParameter = newNavArgs.Parameter;
                this.PageNavigated?.Invoke(this, newNavArgs);
            }
        }

        private NavigationEventArgs GetNavigationEvent(int key)
        {
            return this.navigationEvents.ContainsKey(key) ? this.navigationEvents[key] : null;
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
            IPage previousPage = this.currentPage;

            bool shouldCancel = false;

            NavigatingCancelEventArgs navArgs =
                new NavigatingCancelEventArgs(() => shouldCancel = true)
                    {
                        SourcePageType = previousPage?.GetType(),
                        NavigationMode = NavigationMode.Back
                    };

            previousPage?.OnNavigatingFrom(navArgs);

            return !shouldCancel;
        }
    }
}
#endif