// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WeakCallback.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a callback involving a weak reference to the action's target.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Common.Actions
{
	using System;
	using System.Reflection;

	/// <summary>
	/// Defines a callback involving a weak reference to the action's target.
	/// </summary>
	public sealed class WeakCallback
	{
		private readonly MethodInfo actionInfo;

		private readonly WeakReference weakReference;

		/// <summary>
		/// Initializes a new instance of the <see cref="WeakCallback"/> class.
		/// </summary>
		/// <param name="action">
		/// The callback action to invoke.
		/// </param>
		/// <param name="callbackType">
		/// The expected type for the callback.
		/// </param>
		public WeakCallback(Delegate action, Type callbackType)
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
		/// <exception cref="InvalidOperationException">
		/// Thrown if the weak reference is no longer alive.
		/// </exception>
		public void Invoke(object param)
		{
			if (this.IsAlive)
			{
				this.actionInfo.Invoke(this.weakReference.Target, new[] { param });
			}
		}
	}
}