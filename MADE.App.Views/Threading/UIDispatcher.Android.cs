// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UIDispatcher.Android.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines the Android helper class for dispatching actions on the UI thread.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if __ANDROID__
namespace MADE.App.Views.Threading
{
    using System;
    using System.Threading.Tasks;

    using Android.App;

    using XPlat.UI.Core;

    /// <summary>
    /// Defines the Android helper class for dispatching actions on the UI thread.
    /// </summary>
    public class UIDispatcher : IUIDispatcher
    {
        /// <summary>
        /// Gets the platform specified UI object to use as a reference.
        /// </summary>
        public object Reference { get; private set; }

        /// <summary>
        /// Gets the instance of the Android CoreDispatcher.
        /// </summary>
        internal CoreDispatcher Instance { get; private set; }

        /// <summary>
        /// Sets the platform specific UI object to use as a reference for dispatching actions on the UI thread.
        /// </summary>
        /// <param name="reference">
        /// The platform specific UI reference object.
        /// </param>
        public void SetReference(object reference)
        {
            this.Instance = null;

            if (!(reference is Activity activity))
            {
                throw new UIDispatcherException("Cannot initialize the UIDispatcher without an Activity.");
            }

            this.Reference = activity;
            this.Instance = new CoreDispatcher(activity);
        }

        /// <summary>
        /// Schedules the provided action on the UI thread from a worker thread.
        /// </summary>
        /// <param name="action">
        /// The action to run on the dispatcher.
        /// </param>
        public void Run(Action action)
        {
            this.CheckInitialized();

            if (action == null)
            {
                return;
            }

            this.Run(CoreDispatcherPriority.Normal, action);
        }

        /// <summary>
        /// Schedules the provided action on the UI thread from a worker thread.
        /// </summary>
        /// <param name="priority">
        /// The priority.
        /// </param>
        /// <param name="action">
        /// The action to run on the dispatcher.
        /// </param>
        public async void Run(CoreDispatcherPriority priority, Action action)
        {
            this.CheckInitialized();

            if (action == null)
            {
                return;
            }

            await this.Instance.RunAsync(() => action());
        }

        /// <summary>
        /// Schedules the provided action on the UI thread from a worker thread.
        /// </summary>
        /// <param name="action">
        /// The action to run on the dispatcher.
        /// </param>
        /// <returns>
        /// An asynchronous operation.
        /// </returns>
        public Task RunAsync(Action action)
        {
            this.CheckInitialized();

            return action == null ? Task.CompletedTask : this.RunAsync(CoreDispatcherPriority.Normal, action);
        }

        /// <summary>
        /// Schedules the provided action on the UI thread from a worker thread.
        /// </summary>
        /// <param name="priority">
        /// The priority.
        /// </param>
        /// <param name="action">
        /// The action to run on the dispatcher.
        /// </param>
        /// <returns>
        /// An asynchronous operation.
        /// </returns>
        public Task RunAsync(CoreDispatcherPriority priority, Action action)
        {
            this.CheckInitialized();

            return action == null ? Task.CompletedTask : this.Instance.RunAsync(() => action());
        }

        private void CheckInitialized()
        {
            if (this.Instance == null)
            {
                throw new UIDispatcherException("Cannot run an action as the UIDispatcher has no UI reference set.");
            }
        }
    }
}
#endif