// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISimpleDependencyService.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for a simple dependency service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Common.Dependency
{
	/// <summary>
	/// Defines an interface for a simple dependency service.
	/// </summary>
	public interface ISimpleDependencyService
	{
		/// <summary>
		/// Registers the given type.
		/// </summary>
		/// <typeparam name="TClass">
		/// The type to register.
		/// </typeparam>
		void Register<TClass>() where TClass : class;

		/// <summary>
		/// Registers the given interface and associated type.
		/// </summary>
		/// <typeparam name="TInterface">
		/// The interface to register.
		/// </typeparam>
		/// <typeparam name="TClass">
		/// The interfaces associated class type to register.
		/// </typeparam>
		void Register<TInterface, TClass>()
			where TInterface : class where TClass : class, TInterface;

		/// <summary>
		/// Gets an instance of the given service type that is registered.
		/// </summary>
		/// <typeparam name="TService">
		/// The type of service to retrieve.
		/// </typeparam>
		/// <returns>
		/// Returns a registered instance of the given service.
		/// </returns>
		TService GetInstance<TService>();

		/// <summary>
		/// Checks whether the given type is registered with the service.
		/// </summary>
		/// <typeparam name="T">
		/// The type to check.
		/// </typeparam>
		/// <returns>
		/// Returns true if the type is registered.
		/// </returns>
		bool IsRegistered<T>();
	}
}