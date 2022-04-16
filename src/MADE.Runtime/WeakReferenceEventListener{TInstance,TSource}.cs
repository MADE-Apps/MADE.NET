// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Runtime
{
    using System;

    /// <summary>
    /// Defines a model for providing a weak referenced event listener.
    /// </summary>
    /// <typeparam name="TInstance">
    /// The instance type for the listener.
    /// </typeparam>
    /// <typeparam name="TSource">
    /// The source type.
    /// </typeparam>
    public sealed class WeakReferenceEventListener<TInstance, TSource>
        where TInstance : class
    {
        private readonly WeakReference weakReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakReferenceEventListener{TInstance,TSource}"/> class.
        /// </summary>
        /// <param name="instance">
        /// The instance.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="instance"/> is <see langword="null"/>.</exception>
        public WeakReferenceEventListener(TInstance instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            this.weakReference = new WeakReference(instance);
        }

        /// <summary>
        /// Gets or sets the action to be fired when the event is triggered.
        /// </summary>
        public Action<TInstance, TSource> OnEventAction { get; set; }

        /// <summary>
        /// Gets or sets the action to be fired when the listener is detached.
        /// </summary>
        public Action<TInstance, WeakReferenceEventListener<TInstance, TSource>> OnDetachAction { get; set; }

        /// <summary>
        /// Called when the event is fired.
        /// </summary>
        /// <param name="source">
        /// The source of the event.
        /// </param>
        /// <exception cref="Exception">Potentially thrown by the <see cref="OnEventAction"/> delegate callback.</exception>
        public void OnEvent(TSource source)
        {
            var target = (TInstance)this.weakReference.Target;
            if (target != null)
            {
                this.OnEventAction?.Invoke(target, source);
            }
            else
            {
                this.Detach();
            }
        }

        /// <summary>
        /// Called when detaching the event listener.
        /// </summary>
        /// <exception cref="Exception">Potentially thrown by the <see cref="OnDetachAction"/> delegate callback.</exception>
        public void Detach()
        {
            var target = (TInstance)this.weakReference.Target;
            if (this.OnDetachAction == null)
            {
                return;
            }

            this.OnDetachAction(target, this);
            this.OnDetachAction = null;
        }
    }
}