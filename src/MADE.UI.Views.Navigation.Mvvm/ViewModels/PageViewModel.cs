// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.Views.Navigation.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Messaging;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// Defines a view-model for a MADE page.
    /// </summary>
    public class PageViewModel : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageViewModel"/> class.
        /// </summary>
        /// <param name="messenger">
        /// The <see cref="IMessenger"/> for exchanging messages between objects.
        /// </param>
        public PageViewModel(IMessenger messenger)
        {
            this.Messenger = messenger;
        }

        /// <summary>
        /// Gets the <see cref="IMessenger"/> for exchanging messages between objects.
        /// </summary>
        public IMessenger Messenger { get; }

        /// <summary>
        /// Called when the page has loaded.
        /// </summary>
        public virtual void OnPageLoaded()
        {
        }

        /// <summary>
        /// Called when the page has been navigated from.
        /// </summary>
        /// <param name="e">
        /// The navigation event argument for the navigation.
        /// </param>
        public virtual void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        /// <summary>
        /// Called when the page has been navigated to.
        /// </summary>
        /// <param name="e">
        /// The navigation event argument for the navigation.
        /// </param>
        public virtual void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        /// <summary>
        /// Called when the page is being navigated from.
        /// </summary>
        /// <param name="e">
        /// The navigation event argument for the navigation supporting the cancellation of the navigation.
        /// </param>
        public virtual void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }
    }
}