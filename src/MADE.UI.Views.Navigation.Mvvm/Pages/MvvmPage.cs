// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.Views.Navigation.Pages
{
    using MADE.UI.Views.Navigation.ViewModels;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// Defines an MVVM friendly page that is compatible with the Windows Community Toolkit MVVM library.
    /// </summary>
    public partial class MvvmPage : Page
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
        protected override void OnNavigatedFrom(NavigationEventArgs e)
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
        protected override void OnNavigatedTo(NavigationEventArgs e)
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
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            if (this.DataContext is PageViewModel vm)
            {
                vm.OnNavigatingFrom(e);
            }
        }
    }
}