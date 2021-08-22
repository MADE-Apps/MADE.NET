namespace MADE.Runtime.Extensions
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Defines a collection of extensions for object reflection.
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Gets a value for a property of the <paramref name="obj">specified object</paramref> based on the <paramref name="property">specified property name</paramref>.
        /// </summary>
        /// <param name="obj">The object to retrieve the property value from.</param>
        /// <param name="property">The name of the property to retrieve a value for.</param>
        /// <typeparam name="T">The type of expected value.</typeparam>
        /// <returns>The value of the property.</returns>
        public static T GetPropertyValue<T>(this object obj, string property)
            where T : class
        {
            Type type = obj.GetType();
            PropertyInfo prop = type.GetProperty(property);
            return prop?.GetValue(obj) as T;
        }
    }
}