// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NavigatingCancelEventArgs.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an event argument for a page navigation that supports cancelling the navigation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Views.Navigation
{
    using System;

    /// <summary>
    /// Defines an event argument for a page navigation that supports cancelling the navigation.
    /// </summary>
    public class NavigatingCancelEventArgs : NavigationEventArgs
    {
        private readonly Action cancelAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigatingCancelEventArgs"/> class.
        /// </summary>
        /// <param name="cancelAction">
        /// The action to invoke when the navigation is being cancelled.
        /// </param>
        public NavigatingCancelEventArgs(Action cancelAction)
        {
            this.cancelAction = cancelAction;
            this.Cancelled = false;
        }

        /// <summary>
        /// Gets a value indicating whether the navigation has been cancelled.
        /// </summary>
        public bool Cancelled { get; private set; }

        /// <summary>
        /// Cancels the navigation event.
        /// </summary>
        public void Cancel()
        {
            this.cancelAction?.Invoke();
            this.Cancelled = true;
        }
    }
}