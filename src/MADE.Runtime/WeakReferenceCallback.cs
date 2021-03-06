// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Runtime
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Defines a model for providing a weak referenced callback.
    /// </summary>
    public sealed class WeakReferenceCallback
    {
        private readonly MethodInfo actionInfo;

        private readonly WeakReference weakReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakReferenceCallback"/> class.
        /// </summary>
        /// <param name="action">
        /// The callback action.
        /// </param>
        /// <param name="callbackType">
        /// The expected type for the callback.
        /// </param>
        /// <exception cref="T:System.Exception">The <paramref name="action"/> callback throws an exception acquiring method info.</exception>
        public WeakReferenceCallback(Delegate action, Type callbackType)
        {
            this.actionInfo = action.GetMethodInfo();
            this.weakReference = new WeakReference(action.Target);
            this.Type = callbackType;
        }

        /// <summary>
        /// Gets a value indicating whether the callback is alive.
        /// </summary>
        public bool IsAlive => this.weakReference.IsAlive;

        /// <summary>
        /// Gets the expected type for the callback.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Invokes the callback with the specified parameter.
        /// </summary>
        /// <param name="param">
        /// The parameter to pass to the callback.
        /// </param>
        /// <exception cref="InvalidOperationException">The associated weak reference is no longer alive.</exception>
        public void Invoke(object param)
        {
            if (this.IsAlive)
            {
                this.actionInfo.Invoke(this.weakReference.Target, new[] { param });
            }
            else
            {
                throw new InvalidOperationException("The callback is no longer alive.");
            }
        }
    }
}