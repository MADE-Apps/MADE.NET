// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationExtensions.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a collection of extensions for handling navigation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Views.Navigation
{
    /// <summary>
    /// Defines a collection of extensions for handling navigation.
    /// </summary>
    public static partial class Extensions
    {
#if WINDOWS_UWP
        public static NavigatingCancelEventArgs ToNavigatingCancelEventArgs(
            this Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs args)
        {
            NavigatingCancelEventArgs navArgs =
                new NavigatingCancelEventArgs(() => args.Cancel = true)
                    {
                        NavigationMode =
                            args.NavigationMode.ToNavigationMode(),
                        Parameter = args.Parameter,
                        SourcePageType = args.SourcePageType
                    };

            return navArgs;
        }

        public static NavigationEventArgs ToNavigationEventArgs(
            this Windows.UI.Xaml.Navigation.NavigationEventArgs args)
        {
            NavigationEventArgs navArgs =
                new NavigationEventArgs
                    {
                        NavigationMode = args.NavigationMode.ToNavigationMode(),
                        Parameter = args.Parameter,
                        SourcePageType = args.SourcePageType
                    };
            return navArgs;
        }

        public static NavigationMode ToNavigationMode(
            this Windows.UI.Xaml.Navigation.NavigationMode mode)
        {
            switch (mode)
            {
                case Windows.UI.Xaml.Navigation.NavigationMode.Back:
                    return NavigationMode.Back;
                case Windows.UI.Xaml.Navigation.NavigationMode.Forward:
                    return NavigationMode.Forward;
                case Windows.UI.Xaml.Navigation.NavigationMode.Refresh:
                    return NavigationMode.Refresh;
            }

            return NavigationMode.New;
        }
#elif __ANDROID__
        /// <summary>
        /// Gets the currently active support fragment for the given support fragment manager.
        /// </summary>
        /// <param name="fragmentManager">
        /// The support fragment manager to retrieve the support fragment from.
        /// </param>
        /// <returns>
        /// Returns the active support fragment, if exists; otherwise, null.
        /// </returns>
        public static Android.Support.V4.App.Fragment GetCurrentFragment(
            this Android.Support.V4.App.FragmentManager fragmentManager)
        {
            if (fragmentManager == null)
            {
                return null;
            }

            System.Collections.Generic.IList<Android.Support.V4.App.Fragment> fragments = fragmentManager.Fragments;
            for (int i = fragments.Count - 1; i >= 0; i--)
            {
                Android.Support.V4.App.Fragment fragment = fragments[i];
                if (fragment != null)
                {
                    return fragment;
                }
            }

            return null;
        }
#endif
    }
}