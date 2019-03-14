// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUIDispatcher.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for a dispatcher that performs actions on the UI thread.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Views.Threading
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines an interface for a dispatcher that performs actions on the UI thread.
    /// </summary>
    [Obsolete("IUIDispatcher will be removed. We recommend using XPlat.UI.Core.ICoreDispatcher.")]
    public interface IUIDispatcher
    {
        /// <summary>
        /// Gets the platform specified UI object to use as a reference.
        /// </summary>
        object Reference { get; }

        /// <summary>
        /// Sets the platform specific UI object to use as a reference for dispatching actions on the UI thread.
        /// </summary>
        /// <param name="reference">
        /// The platform specific UI reference object.
        /// </param>
        void SetReference(object reference);

        /// <summary>
        /// Schedules the provided action on the UI thread from a worker thread.
        /// </summary>
        /// <param name="action">
        /// The action to run on the dispatcher.
        /// </param>
        /// <returns>
        /// An asynchronous operation.
        /// </returns>
        Task RunAsync(Action action);
    }
}