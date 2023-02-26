using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Services
{
    /// <summary>
    ///偏好存储接口.
    /// </summary>
    public interface IPreferenceStorage
    {
        /// <param name="key">Preference key.</param>
        /// <param name="value">Preference value.</param>
        /// <summary>Sets a value for a given key.</summary>
        void Set(string key, string value);

        void Set(string key, int value);
        /// <param name="key">Preference key.</param>
        /// <param name="defaultValue">Default value to return if the key does not exist.</param>
        /// <summary>Gets the value for a given key, or the default specified if the key does not exist.</summary>
        /// <returns>Value for the given key, or the default if it does not exist.</returns>
        string Get(string key, string defaultValue);

       int Get(string key, int defaultValue);
    }
}
