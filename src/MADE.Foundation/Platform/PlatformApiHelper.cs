namespace MADE.Foundation.Platform
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Defines a helper for checking platform support for APIs.
    /// </summary>
    public static class PlatformApiHelper
    {
        private static readonly object Lock = new();

        private static readonly Dictionary<Type, bool> CheckedTypes = new();

        /// <summary>
        /// Indicates whether the specified <paramref name="type"/> is supported by the platform.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if supported; otherwise, false.</returns>
        public static bool IsTypeSupported(Type type)
        {
            lock (Lock)
            {
                if (!CheckedTypes.TryGetValue(type, out var result))
                {
                    CheckedTypes[type] = result = IsSupported(type);
                }

                return result;
            }
        }

        /// <summary>
        /// Indicates whether the specified <paramref name="methodName"/> on <paramref name="type"/> is supported by the platform.
        /// </summary>
        /// <param name="type">The type where the method should be checked.</param>
        /// <param name="methodName">The name of the method to check.</param>
        /// <returns>True if supported; otherwise, false.</returns>
        /// <exception cref="AmbiguousMatchException">More than one method is found with the specified name.</exception>
        public static bool IsMethodSupported(Type type, string methodName)
        {
            var result = IsTypeSupported(type);
            if (!result)
            {
                result = IsSupported(type.GetMethod(methodName));
            }

            return result;
        }

        /// <summary>
        /// Indicates whether the specified <paramref name="propertyName"/> on <paramref name="type"/> is supported by the platform.
        /// </summary>
        /// <param name="type">The type where the property should be checked.</param>
        /// <param name="propertyName">The name of the property to check.</param>
        /// <returns>True if supported; otherwise, false.</returns>
        /// <exception cref="AmbiguousMatchException">More than one property is found with the specified name.</exception>
        public static bool IsPropertySupported(Type type, string propertyName)
        {
            var result = IsTypeSupported(type);
            if (!result)
            {
                result = IsSupported(type.GetProperty(propertyName));
            }

            return result;
        }

        private static bool IsSupported(ICustomAttributeProvider attributeProvider)
        {
            return (attributeProvider?.GetCustomAttributes(typeof(PlatformNotSupportedAttribute), false).Length ?? -1) == 0;
        }
    }
}
