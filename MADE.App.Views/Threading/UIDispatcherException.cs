// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UIDispatcherException.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an exception thrown by the UIDispatcher.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Views.Threading
{
    using System;

    /// <summary>
    /// Defines an exception thrown by the UIDispatcher.
    /// </summary>
    public class UIDispatcherException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UIDispatcherException"/> class.
        /// </summary>
        /// <param name="message">
        /// The error message that explains the current exception.
        /// </param>
        public UIDispatcherException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UIDispatcherException"/> class.
        /// </summary>
        /// <param name="ex">
        /// The exception that is the cause of the current exception.
        /// </param>
        public UIDispatcherException(Exception ex)
            : base(null, ex)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UIDispatcherException"/> class.
        /// </summary>
        /// <param name="message">
        /// The error message that explains the current exception.
        /// </param>
        /// <param name="ex">
        /// The exception that is the cause of the current exception.
        /// </param>
        public UIDispatcherException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}