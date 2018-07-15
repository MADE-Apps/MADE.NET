// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UIDispatcher.Windows.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a dispatcher that performs actions on the UI thread.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if WINDOWS_UWP
namespace MADE.App.Views.Threading
{
    using System;
    using System.Threading.Tasks;

    using Windows.UI.Core;

    /// <summary>
    /// Defines a dispatcher that performs actions on the UI thread.
    /// </summary>
    public class UIDispatcher : IUIDispatcher
    {
        public object Reference { get; private set; }

        private CoreDispatcher CoreDispatcher => this.Reference as CoreDispatcher;

        /// <summary>
        /// Sets the <see cref="Windows.UI.Core.CoreDispatcher"/> to use as a reference for dispatching actions on the UI thread.
        /// </summary>
        /// <param name="reference">
        /// The <see cref="Windows.UI.Core.CoreDispatcher"/> object.
        /// </param>
        public void SetReference(object reference)
        {
            this.Reference = null;

            if (!(reference is CoreDispatcher coreDispatcher))
            {
                return;
            }

            this.Reference = coreDispatcher;
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
        public async Task RunAsync(Action action)
        {
            UIDispatcher dispatcher = this;
            dispatcher.CheckInitialized();
            if (action == null)
            {
                return;
            }

            if (dispatcher.CoreDispatcher.HasThreadAccess)
            {
                action.Invoke();
            }
            else
            {
                await dispatcher.CoreDispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action());
            }
        }

        private void CheckInitialized()
        {
            if (this.Reference == null)
            {
                throw new InvalidOperationException(
                    "Cannot run an action as the CoreDispatcher has not be set as the reference in the SetReference method.");
            }
        }
    }
}
#endif