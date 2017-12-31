// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.ConcurrentDictionary.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a collection of extensions for a <see cref="ConcurrentDictionary{TKey,TValue}" /> for network processes.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Networking
{
	using System.Collections.Concurrent;

	using MADE.Networking.Requests;

	/// <summary>
	/// Defines a collection of extensions for a <see cref="ConcurrentDictionary{TKey,TValue}"/> for network processes.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Adds a network request callback to the network request queue.
		/// </summary>
		/// <param name="queue">
		/// The network request queue to add to.
		/// </param>
		/// <param name="networkRequestCallback">
		/// The network request callback to add.
		/// </param>
		public static void AddOrUpdate(
			this ConcurrentDictionary<string, NetworkRequestCallback> queue,
			NetworkRequestCallback networkRequestCallback)
		{
			if (queue != null && networkRequestCallback != null)
			{
				queue.AddOrUpdate(
					networkRequestCallback.Request.Id.ToString(),
					networkRequestCallback,
					(s, callback) => networkRequestCallback);
			}
		}
	}
}