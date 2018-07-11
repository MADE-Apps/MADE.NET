// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationSettings.Xamarin.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a helper for managing locally stored application settings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if __ANDROID__ || __IOS__
namespace MADE.App.Storage.Settings
{
    using System;

    using XPlat.Storage;

    /// <summary>
    /// Defines a helper for managing locally stored application settings.
    /// </summary>
    public class ApplicationSettings : IApplicationSettings
    {
        /// <summary>
        /// Adds or updates a value stored within the application settings for the specified key.
        /// </summary>
        /// <param name="key">
        /// The key associated with the value to store.
        /// </param>
        /// <param name="value">
        /// The value to store.
        /// </param>
        /// <typeparam name="T">
        /// The value's expected type.
        /// </typeparam>
        /// <returns>
        /// True if added or updated successfully; otherwise, false.
        /// </returns>
        public void AddOrUpdate<T>(string key, T value)
        {
            if (value == null)
            {
                return;
            }

            ApplicationData.Current.LocalSettings.Values[key] = value;
        }

        /// <summary>
        /// Attempts to add or update a value stored within the application settings for the specified key.
        /// </summary>
        /// <param name="key">
        /// The key associated with the value to store.
        /// </param>
        /// <param name="value">
        /// The value to store.
        /// </param>
        /// <typeparam name="T">
        /// The value's expected type.
        /// </typeparam>
        /// <returns>
        /// True if added or updated successfully; otherwise, false.
        /// </returns>
        public bool TryAddOrUpdate<T>(string key, T value)
        {
            try
            {
                if (value == null)
                {
                    return false;
                }

                this.AddOrUpdate(key, value);
                return true;
            }
            catch (Exception)
            {
                // Ignored
            }

            return false;
        }

        /// <summary>
        /// Gets a value stored within the application settings for the specified key.
        /// </summary>
        /// <param name="key">
        /// The key associated with the value to retrieve.
        /// </param>
        /// <typeparam name="T">
        /// The value's expected type.
        /// </typeparam>
        /// <returns>
        /// The value as the expected type if stored; otherwise, default type value.
        /// </returns>
        public T Get<T>(string key)
            where T : class
        {
            return ApplicationData.Current.LocalSettings.Values.Get<T>(key);
        }

        /// <summary>
        /// Attempts to gets a value stored within the application settings for the specified key.
        /// </summary>
        /// <param name="key">
        /// The key associated with the value to retrieve.
        /// </param>
        /// <param name="value">
        /// The value as the expected type if stored. 
        /// </param>
        /// <typeparam name="T">
        /// The value's expected type.
        /// </typeparam>
        /// <returns>
        /// True if the get was successful; otherwise, false.
        /// </returns>
        public bool TryGet<T>(string key, out T value)
            where T : class
        {
            value = default(T);

            if (!this.ContainsKey(key))
            {
                return false;
            }

            value = this.Get<T>(key);
            return true;

        }

        /// <summary>
        /// Checks whether the specified key is associated with a value stored within the application settings.
        /// </summary>
        /// <param name="key">
        /// The key to find.
        /// </param>
        /// <returns>
        /// True if the key exists; otherwise, false.
        /// </returns>
        public bool ContainsKey(string key)
        {
            return ApplicationData.Current.LocalSettings.Values.ContainsKey(key);
        }

        /// <summary>
        /// Removes a key-value pair from the application settings for the specified key.
        /// </summary>
        /// <param name="key">
        /// The key associated with the key-value pair to remove.
        /// </param>
        public void Remove(string key)
        {
            ApplicationData.Current.LocalSettings.Values.Remove(key);
        }

        /// <summary>
        /// Attempts to remove a key-value pair from the application settings for the specified key.
        /// </summary>
        /// <param name="key">
        /// The key associated with the key-value pair to remove.
        /// </param>
        /// <returns>
        ///True if the key-value pair is removed; otherwise, false.
        /// </returns>
        public bool TryRemove(string key)
        {
            if (!this.ContainsKey(key))
            {
                return false;
            }

            ApplicationData.Current.LocalSettings.Values.Remove(key);
            return true;
        }
    }
}
#endif