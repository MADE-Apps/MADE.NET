// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UncaughtExceptionHandler.Android.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an implementation of a Java uncaught exception handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if __ANDROID__
namespace MADE.App.Diagnostics.Exceptions
{
    using System;

    using Java.Lang;

    using Object = Java.Lang.Object;

    /// <summary>
    /// Defines an implementation of a Java uncaught exception handler.
    /// </summary>
    public class UncaughtExceptionHandler : Object, Thread.IUncaughtExceptionHandler
    {
        private readonly Action<System.Exception> onException;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="UncaughtExceptionHandler"/> class.
        /// </summary>
        /// <param name="onException">The action that will be called when an exception is observed.</param>
        public UncaughtExceptionHandler(Action<System.Exception> onException)
        {
            this.onException = onException;
        }

        /// <summary>
        /// Called when an uncaught exception is observed.
        /// </summary>
        /// <param name="t">The thread where the exception originated from.</param>
        /// <param name="e">The exception.</param>
        public void UncaughtException(Thread t, Throwable e)
        { 
            this.onException?.Invoke(e);
        }
    }
}
#endif