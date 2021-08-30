namespace MADE.Samples
{
    using System;
    using System.Threading.Tasks;
    using CommunityToolkit.Mvvm.DependencyInjection;
    using CommunityToolkit.Mvvm.Messaging;
    using MADE.Diagnostics;
    using MADE.Diagnostics.Logging;
    using MADE.Samples.Features.Home.Pages;
    using MADE.Samples.Infrastructure.ViewModels;
    using MADE.UI.Views.Navigation;
    using Microsoft.Extensions.DependencyInjection;
    using Windows.ApplicationModel.Activation;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// Defines application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        private IServiceProvider serviceProvider;

        public App()
        {
            this.InitializeComponent();
        }

        public static IServiceProvider Services
        {
            get
            {
                IServiceProvider serviceProvider = ((App)Current).serviceProvider;

                if (serviceProvider is null)
                {
                    throw new InvalidOperationException("Service provider is not initialized.");
                }

                return serviceProvider;
            }
        }


        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            await this.ActivateAsync(e.PrelaunchActivated);
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await this.ActivateAsync(false);
        }

        private static IServiceProvider ConfigureServices(Frame rootFrame)
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                    .AddSingleton<IMessenger>(provider => WeakReferenceMessenger.Default)
                    .AddSingleton<IEventLogger, FileEventLogger>()
                    .AddSingleton<IAppDiagnostics, AppDiagnostics>()
                    .AddSingleton<INavigationService>(provider => new NavigationService(rootFrame))
                    .AddViewModels()
                    .BuildServiceProvider());

            return Ioc.Default;
        }

        private static void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new InvalidOperationException($"Failed to load page {e.SourcePageType.FullName}.");
        }

        private async Task ActivateAsync(bool isPrelaunch)
        {
#if NET5_0 && WINDOWS
			var window = new Window();
			window.Activate();
#else
            Window window = Windows.UI.Xaml.Window.Current;
#endif

            if (!(window.Content is Frame rootFrame))
            {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;

                window.Content = rootFrame;

                this.serviceProvider = ConfigureServices(rootFrame);

                IAppDiagnostics diagnostics = this.serviceProvider.GetService<IAppDiagnostics>();
                if (diagnostics != null)
                {
                    await diagnostics.StartRecordingDiagnosticsAsync();
                }
            }

            if (!isPrelaunch)
            {
                if (rootFrame.Content is null)
                {
                    rootFrame.Navigate(typeof(MainPage));
                }

                window.Activate();
            }
        }
    }
}
