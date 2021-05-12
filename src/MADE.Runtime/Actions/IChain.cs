namespace MADE.Runtime.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines an interface for a chain of objects.
    /// </summary>
    /// <typeparam name="T">The type of object being chained.</typeparam>
    public interface IChain<T>
        where T : class
    {
        /// <summary>
        /// Concatenates the current instances in the chain with the specified instance.
        /// </summary>
        /// <param name="instance">The instance to chain.</param>
        /// <returns>The updated <see cref="Chain{T}"/>.</returns>
        Chain<T> With(T instance);

        /// <summary>
        /// Concatenates the current instances in the chain with the specified instances.
        /// </summary>
        /// <param name="instances">The instances to chain.</param>
        /// <returns>The updated <see cref="Chain{T}"/></returns>
        Chain<T> With(IEnumerable<T> instances);

        /// <summary>
        /// Invokes an action with the chain.
        /// </summary>
        /// <param name="func">The action to invoke.</param>
        void Invoke(Action<T> func);

        /// <summary>
        /// Invokes an asynchronous action with the chain.
        /// </summary>
        /// <param name="func">The asynchronous action to invoke.</param>
        /// <returns>An asynchronous operation.</returns>
        Task InvokeAsync(Func<T, Task> func);
    }
}