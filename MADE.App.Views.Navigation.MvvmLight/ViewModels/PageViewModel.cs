// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PageViewModel.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a view-model for a MADE page.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Views.Navigation.ViewModels
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Messaging;

    /// <summary>
    /// Defines a view-model for a MADE page.
    /// </summary>
    public class PageViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageViewModel"/> class.
        /// </summary>
        public PageViewModel()
            : this(SimpleIoc.Default.GetInstance<IMessenger>())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageViewModel"/> class.
        /// </summary>
        /// <param name="messenger">
        /// The MVVM message aggregator.
        /// </param>
        [PreferredConstructor]
        public PageViewModel(IMessenger messenger)
        {
            this.MessengerInstance = messenger;
        }

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