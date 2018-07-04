// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleDependencyService.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a simple dependency service. Based on SimpleIoc.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace
    MADE.App.Dependency
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Defines a simple dependency service. Based on SimpleIoc.
    /// </summary>
    public class SimpleDependencyService : ISimpleDependencyService
    {
        private static ISimpleDependencyService instance;

        private readonly Dictionary<Type, ConstructorInfo> ctorInfos = new Dictionary<Type, ConstructorInfo>();

        private readonly Dictionary<Type, Type> interfaceToClassMapping = new Dictionary<Type, Type>();

        private readonly Dictionary<Type, Dictionary<string, Delegate>> factories =
            new Dictionary<Type, Dictionary<string, Delegate>>();

        private readonly string registrationKey = Guid.NewGuid().ToString();

        private readonly object registrationLock = new object();

        /// <summary>
        /// Gets an instance of the dependency service.
        /// </summary>
        public static ISimpleDependencyService Instance => instance ?? (instance = new SimpleDependencyService());

        /// <summary>
        /// Registers the given type.
        /// </summary>
        /// <typeparam name="TClass">
        /// The type to register.
        /// </typeparam>
        public void Register<TClass>()
            where TClass : class
        {
            Type type = typeof(TClass);

            if (type.IsInterface)
            {
                throw new InvalidOperationException(
                    "To register with an interface, use the Register<TInterface, TClass> method instead.");
            }

            lock (this.registrationLock)
            {
                if (this.factories.ContainsKey(type) && this.factories[type].ContainsKey(this.registrationKey))
                {
                    return;
                }

                if (!this.interfaceToClassMapping.ContainsKey(type))
                {
                    this.interfaceToClassMapping.Add(type, null);
                }

                this.ctorInfos.Add(type, this.GetConstructorInfo(type));
                Func<TClass> factory = this.MakeInstance<TClass>;
                this.Register(this.registrationKey, type, factory);
            }
        }

        /// <summary>
        /// Registers the given interface and associated type.
        /// </summary>
        /// <typeparam name="TInterface">
        /// The interface to register.
        /// </typeparam>
        /// <typeparam name="TClass">
        /// The interfaces associated class type to register.
        /// </typeparam>
        public void Register<TInterface, TClass>()
            where TInterface : class where TClass : class, TInterface
        {
            lock (this.registrationLock)
            {
                Type interfaceType = typeof(TInterface);
                Type classType = typeof(TClass);

                if (!this.interfaceToClassMapping.ContainsKey(interfaceType))
                {
                    this.interfaceToClassMapping.Add(interfaceType, classType);
                    this.ctorInfos.Add(classType, this.GetConstructorInfo(classType));
                }

                Func<TInterface> factory = this.MakeInstance<TInterface>;
                this.Register(this.registrationKey, interfaceType, factory);
            }
        }

        /// <summary>
        /// Registers the given type with the given factory.
        /// </summary>
        /// <param name="factory">
        /// The factory for creating instances of the given type.
        /// </param>
        /// <typeparam name="TClass">
        /// The type to register.
        /// </typeparam>
        public void Register<TClass>(Func<TClass> factory)
        {
            this.Register(this.registrationKey, factory);
        }

        /// <summary>
        /// Registers the given type with the given factory and associates it with the given key.
        /// </summary>
        /// <param name="key">
        /// The key associated with the registration.
        /// </param>
        /// <param name="factory">
        /// The factory for creating instances of the given type.
        /// </param>
        /// <typeparam name="TClass">
        /// The type to register.
        /// </typeparam>
        public void Register<TClass>(string key, Func<TClass> factory)
        {
            lock (this.registrationLock)
            {
                Type type = typeof(TClass);

                if (this.factories.ContainsKey(type) && this.factories[type].ContainsKey(key))
                {
                    return;
                }

                if (!this.interfaceToClassMapping.ContainsKey(type))
                {
                    this.interfaceToClassMapping.Add(type, null);
                }

                this.Register(key, type, factory);
            }
        }

        /// <summary>
        /// Gets an instance of the given service type that is registered.
        /// </summary>
        /// <param name="key">
        /// The key of the instance to retrieve.
        /// </param>
        /// <typeparam name="TService">
        /// The type of service to retrieve.
        /// </typeparam>
        /// <returns>
        /// Returns a registered instance of the given service.
        /// </returns>
        public TService GetInstance<TService>(string key = null)
        {
            return (TService)this.GetInstance(key, typeof(TService));
        }

        /// <summary>
        /// Checks whether the given type is registered with the service.
        /// </summary>
        /// <param name="key">
        /// The key to check is registered.
        /// </param>
        /// <typeparam name="T">
        /// The type to check.
        /// </typeparam>
        /// <returns>
        /// Returns true if the type is registered.
        /// </returns>
        public bool IsRegistered<T>(string key = null)
        {
            Type type = typeof(T);

            if (string.IsNullOrWhiteSpace(key))
            {
                return this.interfaceToClassMapping.ContainsKey(type);
            }

            if (!this.interfaceToClassMapping.ContainsKey(type) || !this.factories.ContainsKey(type))
            {
                return false;
            }

            return this.factories[type].ContainsKey(key);
        }

        private ConstructorInfo GetConstructorInfo(Type serviceType)
        {
            Type expectedClassType;

            if (this.interfaceToClassMapping.ContainsKey(serviceType))
            {
                expectedClassType = this.interfaceToClassMapping[serviceType] ?? serviceType;
            }
            else
            {
                expectedClassType = serviceType;
            }

            ConstructorInfo[] ctors = expectedClassType.GetConstructors();

            if (ctors.Length > 1)
            {
                if (ctors.Length > 2)
                {
                    throw new InvalidOperationException(
                        $"Registration failed. {expectedClassType.Name} contains multiple constructors.");
                }

                if (ctors.FirstOrDefault(i => i.Name == ".cctor") == null)
                {
                    throw new InvalidOperationException(
                        $"Registration failed. {expectedClassType.Name} contains a static constructor.");
                }

                ConstructorInfo first = ctors.FirstOrDefault(i => i.Name != ".cctor");

                if (first == null || !first.IsPublic)
                {
                    throw new InvalidOperationException(
                        $"Registration failed. {expectedClassType.Name} does not contain a public constructor.");
                }

                return first;
            }

            if (ctors.Length == 0 || (ctors.Length == 1 && !ctors[0].IsPublic))
            {
                throw new InvalidOperationException(
                    $"Registration failed. {expectedClassType.Name} does not contain a public constructor.");
            }

            return ctors[0];
        }

        private TClass MakeInstance<TClass>()
        {
            Type serviceType = typeof(TClass);

            ConstructorInfo constructor = this.ctorInfos.ContainsKey(serviceType)
                                              ? this.ctorInfos[serviceType]
                                              : this.GetConstructorInfo(serviceType);

            ParameterInfo[] parameterInfos = constructor.GetParameters();

            if (parameterInfos.Length == 0)
            {
                return (TClass)constructor.Invoke(new object[0]);
            }

            object[] parameters = new object[parameterInfos.Length];

            foreach (ParameterInfo parameterInfo in parameterInfos)
            {
                parameters[parameterInfo.Position] = this.GetInstance(parameterInfo.ParameterType);
            }

            return (TClass)constructor.Invoke(parameters);
        }

        private object GetInstance(Type serviceType)
        {
            return this.GetInstance(this.registrationKey, serviceType);
        }

        private object GetInstance(string key, Type serviceType)
        {
            lock (this.registrationLock)
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    key = this.registrationKey;
                }

                if (!this.factories.ContainsKey(serviceType))
                {
                    return null;
                }

                if (!this.factories[serviceType].ContainsKey(key))
                {
                    throw new InvalidOperationException($"Type {serviceType.FullName} not found in cache");
                }

                return this.factories[serviceType][key].DynamicInvoke(null);
            }
        }

        private void Register<TClass>(string key, Type classType, Func<TClass> factory)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                key = this.registrationKey;
            }

            if (this.factories.ContainsKey(classType))
            {
                if (this.factories[classType].ContainsKey(key))
                {
                    return;
                }

                this.factories[classType].Add(key, factory);
            }
            else
            {
                Dictionary<string, Delegate> list = new Dictionary<string, Delegate> { { key, factory } };

                this.factories.Add(classType, list);
            }
        }
    }
}