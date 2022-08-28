// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Runtime.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines an implementation for a chain of objects.
    /// </summary>
    /// <typeparam name="T">The type of object being chained.</typeparam>
    public class Chain<T> : IChain<T>
        where T : class
    {
        private readonly List<WeakReference<T>> chain = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Chain{T}"/> class with an instance.
        /// </summary>
        /// <param name="instance">The instance to begin the chain.</param>
        public Chain(T instance)
        {
            this.chain.Add(new WeakReference<T>(instance));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Chain{T}"/> class with a collection of instances.
        /// </summary>
        /// <param name="instances">The instances to begin the chain.</param>
        public Chain(IEnumerable<T> instances)
        {
            this.chain.AddRange(instances.Select(i => new WeakReference<T>(i)));
        }

        /// <summary>
        /// Concatenates the current instances in the chain with the specified instance.
        /// </summary>
        /// <param name="instance">The instance to chain.</param>
        /// <returns>The updated <see cref="Chain{T}"/>.</returns>
        public Chain<T> With(T instance)
        {
            this.chain.Add(new WeakReference<T>(instance));
            return this;
        }

        /// <summary>
        /// Concatenates the current instances in the chain with the specified instances.
        /// </summary>
        /// <param name="instances">The instances to chain.</param>
        /// <returns>The updated <see cref="Chain{T}"/>.</returns>
        public Chain<T> With(IEnumerable<T> instances)
        {
            this.chain.AddRange(instances.Select(i => new WeakReference<T>(i)));
            return this;
        }

        /// <summary>
        /// Invokes an action with the chain.
        /// </summary>
        /// <param name="func">The action to invoke.</param>
        /// <exception cref="Exception">Potential exceptions thrown if delegate callback throws an exception.</exception>
        public void Invoke(Action<T> func)
        {
            foreach (WeakReference<T> instance in this.chain)
            {
                if (instance.TryGetTarget(out T i))
                {
                    func(i);
                }
            }
        }

        /// <summary>
        /// Invokes an asynchronous action with the chain.
        /// </summary>
        /// <param name="func">The asynchronous action to invoke.</param>
        /// <returns>An asynchronous operation.</returns>
        /// <exception cref="Exception">Potential exceptions thrown if delegate callback throws an exception.</exception>
        public async Task InvokeAsync(Func<T, Task> func)
        {
            foreach (WeakReference<T> instance in this.chain)
            {
                if (instance.TryGetTarget(out T i))
                {
                    await func(i);
                }
            }
        }
    }
}