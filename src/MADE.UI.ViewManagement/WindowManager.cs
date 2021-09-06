// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.ViewManagement
{
    using System;
    using System.Threading.Tasks;
    using Windows.ApplicationModel.Core;
    using Windows.Foundation;
    using Windows.UI.Core;
    using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Defines helper methods for handling multiple windows for an application.
    /// </summary>
#if __ANDROID__ || __WASM__ || __IOS__ || __MACOS__ || NETSTANDARD
    [Foundation.Platform.PlatformNotSupported]
#endif
    public static class WindowManager
    {
        /// <summary>
        /// Creates a new application <see cref="Window"/> with the specified page type and navigation parameter.
        /// </summary>
        /// <param name="sourcePageType">
        /// The page type to load in the new Window.
        /// </param>
        /// <returns>
        /// True if loaded; otherwise, false.
        /// </returns>
        public static async Task<bool> CreateNewWindowForPageAsync(Type sourcePageType)
        {
            return await CreateNewWindowForPageAsync(sourcePageType, Size.Empty);
        }

        /// <summary>
        /// Creates a new application <see cref="Window"/> with the specified page type and navigation parameter.
        /// </summary>
        /// <param name="sourcePageType">
        /// The page type to load in the new Window.
        /// </param>
        /// <param name="desiredSize">
        /// The desired size of the new Window.
        /// </param>
        /// <returns>
        /// True if loaded; otherwise, false.
        /// </returns>
        public static async Task<bool> CreateNewWindowForPageAsync(Type sourcePageType, Size desiredSize)
        {
            return await CreateNewWindowForPageAsync(sourcePageType, null, desiredSize);
        }

        /// <summary>
        /// Creates a new application <see cref="Window"/> with the specified page type and navigation parameter.
        /// </summary>
        /// <param name="sourcePageType">
        /// The page type to load in the new Window.
        /// </param>
        /// <param name="parameter">
        /// The parameter to load in the new Window.
        /// </param>
        /// <returns>
        /// True if loaded; otherwise, false.
        /// </returns>
        public static async Task<bool> CreateNewWindowForPageAsync(Type sourcePageType, object parameter)
        {
            return await CreateNewWindowForPageAsync(sourcePageType, parameter, Size.Empty);
        }

        /// <summary>
        /// Creates a new application <see cref="Window"/> with the specified page type and navigation parameter.
        /// </summary>
        /// <param name="sourcePageType">
        /// The page type to load in the new Window.
        /// </param>
        /// <param name="parameter">
        /// The parameter to load in the new Window.
        /// </param>
        /// <param name="desiredSize">
        /// The desired size of the new Window.
        /// </param>
        /// <returns>
        /// True if loaded; otherwise, false.
        /// </returns>
        public static async Task<bool> CreateNewWindowForPageAsync(
            Type sourcePageType,
            object parameter,
            Size desiredSize)
        {
#if __ANDROID__ || __WASM__ || __IOS__ || __MACOS__ || NETSTANDARD
            throw new Foundation.Platform.PlatformNotSupportedException($"{nameof(CreateNewWindowForPageAsync)} is not supported yet by this platform.");
#endif

            CoreApplicationView newApplicationView = CoreApplication.CreateNewView();

            CoreWindow coreWindow = CoreApplication.GetCurrentView().CoreWindow;
            int mainViewId = ApplicationView.GetApplicationViewIdForWindow(coreWindow);

            int newApplicationViewId = 0;

            await newApplicationView.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    newApplicationViewId = ApplicationView.GetForCurrentView().Id;

                    var frame = new Frame();
                    Window.Current.Content = frame;

                    ViewCoreDispatcherManager.Current.RegisterOrUpdate(newApplicationViewId, newApplicationView.Dispatcher);

                    frame.Navigate(sourcePageType, parameter);
                    Window.Current.Activate();
                });

            bool shown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(
                            newApplicationViewId,
                            ViewSizePreference.UseMore,
                            mainViewId,
                            ViewSizePreference.Default);

            if (shown && desiredSize != Size.Empty)
            {
                await newApplicationView.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal,
                    () =>
                    {
                        var view = ApplicationView.GetForCurrentView();
                        view?.TryResizeView(desiredSize);
                    });
            }

            return shown;
        }

        /// <summary>
        /// Creates a new application <see cref="Window"/> with the specified page type and navigation parameter.
        /// </summary>
        /// <param name="sourcePageName">
        /// The page name to load in the new Window.
        /// </param>
        /// <returns>
        /// True if loaded; otherwise, false.
        /// </returns>
        public static async Task<bool> CreateNewWindowForPageAsync(string sourcePageName)
        {
            return await CreateNewWindowForPageAsync(sourcePageName, Size.Empty);
        }

        /// <summary>
        /// Creates a new application <see cref="Window"/> with the specified page type and navigation parameter.
        /// </summary>
        /// <param name="sourcePageName">
        /// The page name to load in the new Window.
        /// </param>
        /// <param name="desiredSize">
        /// The desired size of the new Window.
        /// </param>
        /// <returns>
        /// True if loaded; otherwise, false.
        /// </returns>
        public static async Task<bool> CreateNewWindowForPageAsync(string sourcePageName, Size desiredSize)
        {
            return await CreateNewWindowForPageAsync(sourcePageName, null, desiredSize);
        }

        /// <summary>
        /// Creates a new application <see cref="Window"/> with the specified page type and navigation parameter.
        /// </summary>
        /// <param name="sourcePageName">
        /// The page name to load in the new Window.
        /// </param>
        /// <param name="parameter">
        /// The parameter to load in the new Window.
        /// </param>
        /// <returns>
        /// True if loaded; otherwise, false.
        /// </returns>
        public static async Task<bool> CreateNewWindowForPageAsync(string sourcePageName, object parameter)
        {
            return await CreateNewWindowForPageAsync(sourcePageName, parameter, Size.Empty);
        }

        /// <summary>
        /// Creates a new application <see cref="Window"/> with the specified page type and navigation parameter.
        /// </summary>
        /// <param name="sourcePageName">
        /// The page name to load in the new Window.
        /// </param>
        /// <param name="parameter">
        /// The parameter to load in the new Window.
        /// </param>
        /// <param name="desiredSize">
        /// The desired size of the new Window.
        /// </param>
        /// <returns>
        /// True if loaded; otherwise, false.
        /// </returns>
        public static async Task<bool> CreateNewWindowForPageAsync(
            string sourcePageName,
            object parameter,
            Size desiredSize)
        {
            var sourcePageType = Type.GetType(sourcePageName);
            return sourcePageType != null && await CreateNewWindowForPageAsync(sourcePageType, parameter, desiredSize);
        }
    }
}