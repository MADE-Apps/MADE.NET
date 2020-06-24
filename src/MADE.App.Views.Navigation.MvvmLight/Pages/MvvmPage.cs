// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MvvmPage.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an MVVM friendly page that is compatible with MvvmLight and the application NavigationFrame.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if WINDOWS_UWP || __ANDROID__
namespace MADE.App.Views.Navigation.Pages
{
    using MADE.App.Views.Navigation.ViewModels;

    /// <summary>
    /// Defines an MVVM friendly page that is compatible with MvvmLight and the application NavigationFrame.
    /// </summary>
    public class MvvmPage : Page
    {
        /// <summary>
        /// Called when the page has loaded.
        /// </summary>
        public override void OnPageLoaded()
        {
            base.OnPageLoaded();

            if (this.DataContext is PageViewModel vm)
            {
                vm.OnPageLoaded();
            }
        }

        /// <summary>
        /// Called when the page has been navigated from.
        /// </summary>
        /// <param name="e">
        /// The navigation event argument for the navigation.
        /// </param>
        public override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (this.DataContext is PageViewModel vm)
            {
                vm.OnNavigatedFrom(e);
            }
        }

        /// <summary>
        /// Called when the page has been navigated to.
        /// </summary>
        /// <param name="e">
        /// The navigation event argument for the navigation.
        /// </param>
        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (this.DataContext is PageViewModel vm)
            {
                vm.OnNavigatedTo(e);
            }
        }

        /// <summary>
        /// Called when the page is being navigated from.
        /// </summary>
        /// <param name="e">
        /// The navigation event argument for the navigation supporting the cancellation of the navigation.
        /// </param>
        public override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            if (this.DataContext is PageViewModel vm)
            {
                vm.OnNavigatingFrom(e);
            }
        }
    }
}
#endif