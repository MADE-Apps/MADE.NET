namespace MADE.App.Storage
{
    /// <summary>
    /// Defines an interface for managing locally stored application settings.
    /// </summary>
    public interface IApplicationSettings
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
        void AddOrUpdate<T>(string key, T value);

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
        bool TryAddOrUpdate<T>(string key, T value);

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
        T Get<T>(string key)
            where T : class;

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
        bool TryGet<T>(string key, out T value)
            where T : class;

        /// <summary>
        /// Checks whether the specified key is associated with a value stored within the application settings.
        /// </summary>
        /// <param name="key">
        /// The key to find.
        /// </param>
        /// <returns>
        /// True if the key exists; otherwise, false.
        /// </returns>
        bool ContainsKey(string key);

        /// <summary>
        /// Removes a key-value pair from the application settings for the specified key.
        /// </summary>
        /// <param name="key">
        /// The key associated with the key-value pair to remove.
        /// </param>
        void Remove(string key);

        /// <summary>
        /// Attempts to remove a key-value pair from the application settings for the specified key.
        /// </summary>
        /// <param name="key">
        /// The key associated with the key-value pair to remove.
        /// </param>
        /// <returns>
        ///True if the key-value pair is removed; otherwise, false.
        /// </returns>
        bool TryRemove(string key);
    }
}