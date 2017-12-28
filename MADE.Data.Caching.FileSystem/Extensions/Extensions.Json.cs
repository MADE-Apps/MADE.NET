// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.Json.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a collection of extensions for handling JSON in file system data caching.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Data.Caching.FileSystem
{
    using Newtonsoft.Json;

    /// <summary>
    /// Defines a collection of extensions for handling JSON in file system data caching.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Deserializes the JSON content of the given cached data object as the given type.
        /// </summary>
        /// <param name="data">
        /// The cached data to deserialize.
        /// </param>
        /// <typeparam name="T">
        /// The type of object to deserialize as.
        /// </typeparam>
        /// <returns>
        /// Returns the deserialized JSON object as the given type.
        /// </returns>
        public static T DeserializeJsonContentAs<T>(this CachedData data)
        {
            return data == null ? default(T) : DeserializeJsonContentAs<T>(data, null);
        }

        /// <summary>
        /// Deserializes the JSON content of the given cached data object as the given type.
        /// </summary>
        /// <param name="data">
        /// The cached data to deserialize.
        /// </param>
        /// <param name="settings">
        /// The JSON serialize settings.
        /// </param>
        /// <typeparam name="T">
        /// The type of object to deserialize as.
        /// </typeparam>
        /// <returns>
        /// Returns the deserialized JSON object as the given type.
        /// </returns>
        public static T DeserializeJsonContentAs<T>(this CachedData data, JsonSerializerSettings settings)
        {
            if (data == null)
            {
                return default(T);
            }

            return settings != null
                       ? JsonConvert.DeserializeObject<T>(data.Content, settings)
                       : JsonConvert.DeserializeObject<T>(data.Content);
        }
    }
}