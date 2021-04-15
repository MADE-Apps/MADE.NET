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
    /// <typeparam name="TEventArgs">
    /// The event argument type.
    /// </typeparam>
    public sealed class WeakReferenceEventListener<TInstance, TSource, TEventArgs>
        where TInstance : class
    {
        private readonly WeakReference weakInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakReferenceEventListener{TInstance,TSource,TEventArgs}"/> class.
        /// </summary>
        /// <param name="instance">
        /// The instance.
        /// </param>
        public WeakReferenceEventListener(TInstance instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            this.weakInstance = new WeakReference(instance);
        }

        /// <summary>
        /// Gets or sets the action to be fired when the event is triggered.
        /// </summary>
        public Action<TInstance, TSource, TEventArgs> OnEventAction { get; set; }

        /// <summary>
        /// Gets or sets the action to be fired when the listener is detached.
        /// </summary>
        public Action<TInstance, WeakReferenceEventListener<TInstance, TSource, TEventArgs>> OnDetachAction { get; set; }

        /// <summary>
        /// Called when the event is fired.
        /// </summary>
        /// <param name="source">
        /// The source of the event.
        /// </param>
        /// <param name="eventArgs">
        /// The event arguments.
        /// </param>
        public void OnEvent(TSource source, TEventArgs eventArgs)
        {
            var target = (TInstance)this.weakInstance.Target;
            if (target != null)
            {
                this.OnEventAction?.Invoke(target, source, eventArgs);
            }
            else
            {
                this.Detach();
            }
        }

        /// <summary>
        /// Called when detaching the event listener.
        /// </summary>
        public void Detach()
        {
            var target = (TInstance)this.weakInstance.Target;
            if (this.OnDetachAction == null)
            {
                return;
            }

            this.OnDetachAction(target, this);
            this.OnDetachAction = null;
        }
    }
}