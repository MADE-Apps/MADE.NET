// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetworkRequestCallback.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a callback for a network request.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Networking.Requests
{
	using MADE.Common.Actions;

	/// <summary>
	/// Defines a callback for a network request.
	/// </summary>
	public sealed class NetworkRequestCallback
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NetworkRequestCallback"/> class.
		/// </summary>
		/// <param name="request">
		/// The network request to invoke a callback on.
		/// </param>
		public NetworkRequestCallback(NetworkRequest request)
			: this(request, null, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NetworkRequestCallback"/> class.
		/// </summary>
		/// <param name="request">
		/// The network request to invoke a callback on.
		/// </param>
		/// <param name="successCallback">
		/// The callback for if the network request was successful.
		/// </param>
		public NetworkRequestCallback(NetworkRequest request, WeakCallback successCallback)
			: this(request, successCallback, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NetworkRequestCallback"/> class.
		/// </summary>
		/// <param name="request">
		/// The network request to invoke a callback on.
		/// </param>
		/// <param name="successCallback">
		/// The callback for if the network request was successful.
		/// </param>
		/// <param name="errorCallback">
		/// The callback for if the network request was unsuccessful.
		/// </param>
		public NetworkRequestCallback(NetworkRequest request, WeakCallback successCallback, WeakCallback errorCallback)
		{
			this.Request = request;
			this.SuccessCallback = successCallback;
			this.ErrorCallback = errorCallback;
		}

		/// <summary>
		/// Gets the network request to invoke a callback on.
		/// </summary>
		public NetworkRequest Request { get; }

		/// <summary>
		/// Gets the callback for if the network request was successful.
		/// </summary>
		public WeakCallback SuccessCallback { get; }

		/// <summary>
		/// Gets the callback for if the network request was unsuccessful.
		/// </summary>
		public WeakCallback ErrorCallback { get; }

		/// <summary>
		/// Gets or sets a value indicating whether to allow the network request to be retried if it is unsuccessful in the <see cref="INetworkRequestManager"/>.
		/// </summary>
		public bool AllowRetry { get; set; }

		/// <summary>
		/// Gets a value indicating whether the request has been retried.
		/// </summary>
		/// <remarks>
		/// This property is only useful for use in the <see cref="INetworkRequestManager"/>.
		/// </remarks>
		public bool HasRetried { get; private set; }

		/// <summary>
		/// Called when the network request is to be retried by the associated <see cref="INetworkRequestManager"/>.
		/// </summary>
		public void OnRetry()
		{
			this.HasRetried = true;
		}
	}
}