// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text;

// NOTE: This file is designed to function much like the 
// IsolatedStorageSettings type inside of the Silverlight runtime, but differs
// by requiring a filename to use. This allows multiple components and 
// specialized apps from overlapping within the same settings file. This 
// implementation is hard-coded to only application stores, instead of per-
// site, but would be easy enough to change around.

namespace System.IO.IsolatedStorage
{
    /// <summary>
    /// A class for managing key/value pairs in an isolated storage file on the
    /// Silverlight platform.
    /// </summary>
    public sealed class IsolatedStorageSettingsFile : IDictionary<string, object>, IDictionary
    {
        /// <summary>
        /// Backing field for the filename.
        /// </summary>
        private string _filename;

        /// <summary>
        /// Initializes a new instance of IsolatedStorageSettingsFile.
        /// </summary>
        /// <param name="store">The store to use.</param>
        /// <param name="filename">The filename.</param>
        public IsolatedStorageSettingsFile(IsolatedStorageFile store, string filename)
        {
            _store = store;
            Filename = filename;
        }

        /// <summary>
        /// Initializes a new instance of IsolatedStorageSettingsFile with the
        /// application store, but no specified filename by default. Will not
        /// contain settings until Filename is set.
        /// </summary>
        public IsolatedStorageSettingsFile() : this(IsolatedStorageFile.GetUserStoreForApplication(), null)
        {
        }

        /// <summary>
        /// Gets or sets the filename to use for the settings. Upon set, any
        /// existing values will be read.
        /// </summary>
        public string Filename
        {
            get
            {
                return _filename;
            }

            set
            {
                _filename = value;
                if (!string.IsNullOrEmpty(_filename))
                {
                    Reload();
                }
            }
        }

        /// <summary>
        /// Backing field for the settings.
        /// </summary>
        private Dictionary<string, object> _settings;

        /// <summary>
        /// Backing field for the target data store.
        /// </summary>
        private IsolatedStorageFile _store;

        /// <summary>
        /// Gets the number of settings.
        /// </summary>
        public int Count
        {
            get
            {
                return _settings.Count;
            }
        }

        /// <summary>
        /// Gets the keys for the settings collection.
        /// </summary>
        public ICollection Keys
        {
            get
            {
                return _settings.Keys;
            }
        }

        /// <summary>
        /// Gets the values for the settings collection.
        /// </summary>
        public ICollection Values
        {
            get
            {
                return _settings.Values;
            }
        }

        /// <summary>
        /// Gets or sets an object instance for a setting.
        /// </summary>
        /// <param name="key">The key name.</param>
        /// <returns>Returns an insance of the key, if present.</returns>
        public object this[string key]
        {
            get
            {
                RequireKey(key);
                return _settings[key];
            }
            set
            {
                RequireKey(key);
                _settings[key] = value;
            }
        }

        /// <summary>
        /// Adds a new key and value pair to the collection.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <param name="value">The value instance.</param>
        public void Add(string key, object value)
        {
            RequireKey(key);
            _settings.Add(key, value);
        }

        /// <summary>
        /// Clears the contents of the file.
        /// </summary>
        public void Clear()
        {
            _settings.Clear();
            Save();
        }

        /// <summary>
        /// Checks whether the file contains the key as a setting.
        /// </summary>
        /// <param name="key">The key value,.</param>
        /// <returns>Returns true if the key is contained.</returns>
        public bool Contains(string key)
        {
            RequireKey(key);
            return _settings.ContainsKey(key);
        }

        /// <summary>
        /// Reloads the settings file.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "Known issue.")]
        private void Reload()
        {
            if (string.IsNullOrEmpty(_filename))
            {
                return;
            }

            using (IsolatedStorageFileStream localSettingsStream = _store.OpenFile(_filename, FileMode.OpenOrCreate))
            {
                using (StreamReader reader = new StreamReader(localSettingsStream))
                {
                    if (localSettingsStream.Length > 0)
                    {
                        try
                        {
                            // Read the list of known types.
                            List<Type> knownTypes = new List<Type>();
                            string knownTypesString = reader.ReadLine();
                            foreach (string knownTypeName in knownTypesString.Split('\0'))
                            {
                                Type knownType = Type.GetType(knownTypeName, false);
                                if (knownType != null)
                                {
                                    knownTypes.Add(knownType);
                                }
                            }
                            localSettingsStream.Position = knownTypesString.Length + Environment.NewLine.Length;

                            DataContractSerializer serializer = new DataContractSerializer(typeof(Dictionary<string, object>), knownTypes);
                            _settings = (Dictionary<string, object>)serializer.ReadObject(localSettingsStream);
                        }
                        catch
                        {
                            _settings = new Dictionary<string, object>();
                        }
                    }
                    else
                    {
                        _settings = new Dictionary<string, object>();
                    }
                }
            }
        }

        /// <summary>
        /// Removes a key from the settings file.
        /// </summary>
        /// <param name="key">The key name.</param>
        /// <returns>Returns true if the removal is succesful.</returns>
        public bool Remove(string key)
        {
            RequireKey(key);
            return _settings.Remove(key);
        }

        /// <summary>
        /// Saves the settings file.
        /// </summary>
        public void Save()
        {
            if (string.IsNullOrEmpty(_filename))
            {
                return;
            }

            using (IsolatedStorageFileStream localSettingsStream = _store.OpenFile(_filename, FileMode.OpenOrCreate))
            {
                using (MemoryStream intermediateStream = new MemoryStream())
                {
                    Dictionary<Type, bool> knownTypes = new Dictionary<Type, bool>();
                    StringBuilder knownTypesString = new StringBuilder();
                    foreach (object value in _settings.Values)
                    {
                        if (value != null)
                        {
                            Type knownType = value.GetType();
                            if (!knownType.IsPrimitive && knownType != typeof(string))
                            {
                                knownTypes[knownType] = true;
                                if (knownTypesString.Length > 0)
                                {
                                    knownTypesString.Append('\0');
                                }
                                knownTypesString.Append(knownType.AssemblyQualifiedName);
                            }
                        }
                    }
                    knownTypesString.Append(Environment.NewLine);

                    byte[] knownTypesBuffer = Encoding.UTF8.GetBytes(knownTypesString.ToString());
                    intermediateStream.Write(knownTypesBuffer, 0, knownTypesBuffer.Length);

                    // Save the settings.
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Dictionary<string, object>), knownTypes.Keys);
                    serializer.WriteObject(intermediateStream, _settings);

                    // Verify there's enough free space in the isolated store.
                    if (intermediateStream.Length > (_store.AvailableFreeSpace + localSettingsStream.Length))
                    {
                        throw new IsolatedStorageException("Not enough space!");
                    }

                    // Clear contents.
                    localSettingsStream.SetLength(0);

                    // Copy stream.
                    byte[] buffer = intermediateStream.ToArray();
                    localSettingsStream.Write(buffer, 0, buffer.Length);
                }
            }
        }

        /// <summary>
        /// Tries to retrieve a value from the settings file.
        /// </summary>
        /// <typeparam name="T">The type of the object instance.</typeparam>
        /// <param name="key">The key name.</param>
        /// <param name="value">The instance to get.</param>
        /// <returns>Returns true if the key exists.</returns>
        public bool TryGetValue<T>(string key, out T value)
        {
            RequireKey(key);
            object tempValue;
            if (_settings.TryGetValue(key, out tempValue))
            {
                value = (T)tempValue;
                return true;
            }
            value = default(T);
            return false;
        }

        /// <summary>
        /// Tries to retrieve a value of a specific type from the settings.
        /// </summary>
        /// <param name="key">The key name.</param>
        /// <param name="targetType">The target type to try and retrieve.</param>
        /// <param name="value">The instance to get.</param>
        /// <returns>Returns true if the key exists.</returns>
        public bool TryGetValueOfType(string key, Type targetType, out object value)
        {
            RequireKey(key);
            object tempValue;
            if (_settings.TryGetValue(key, out tempValue))
            {
                if (targetType.IsInstanceOfType(tempValue))
                {
                    value = tempValue;
                    return true;
                }
            }
            value = null;
            return false;
        }

        /// <summary>
        /// A guard that requires a non-null key.
        /// </summary>
        /// <param name="key">The key value.</param>
        private void RequireKey(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
        }

        #region IDictionary<string,object> Members

        bool IDictionary<string, object>.ContainsKey(string key)
        {
            RequireKey(key);
            return _settings.ContainsKey(key);
        }

        ICollection<string> IDictionary<string, object>.Keys
        {
            get
            {
                return _settings.Keys;
            }
        }

        bool IDictionary<string, object>.TryGetValue(string key, out object value)
        {
            RequireKey(key);
            return _settings.TryGetValue(key, out value);
        }

        ICollection<object> IDictionary<string, object>.Values
        {
            get
            {
                return _settings.Values;
            }
        }
        #endregion

        #region ICollection<KeyValuePair<string,object>> Members

        void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
        {
            ((ICollection<KeyValuePair<string, object>>)_settings).Add(item);
        }

        void ICollection<KeyValuePair<string, object>>.Clear()
        {
            _settings.Clear();
        }

        bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
        {
            return ((ICollection<KeyValuePair<string, object>>)_settings).Contains(item);
        }

        void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, object>>)_settings).CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<string, object>>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
        {
            return ((ICollection<KeyValuePair<string, object>>)_settings).Remove(item);
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,object>> Members

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return _settings.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _settings.GetEnumerator();
        }

        #endregion

        #region IDictionary Members

        void IDictionary.Add(object key, object value)
        {
            RequireKey(key);
            ((IDictionary)_settings).Add(key, value);
        }

        void IDictionary.Clear()
        {
            ((IDictionary)_settings).Clear();
        }

        bool IDictionary.Contains(object key)
        {
            RequireKey(key);
            return ((IDictionary)_settings).Contains(key);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary)_settings).GetEnumerator();
        }

        bool IDictionary.IsFixedSize
        {
            get
            {
                return false;
            }
        }

        bool IDictionary.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        void IDictionary.Remove(object key)
        {
            RequireKey(key);
            ((IDictionary)_settings).Remove(key);
        }

        object IDictionary.this[object key]
        {
            get
            {
                RequireKey(key);
                return ((IDictionary)_settings)[key];
            }
            set
            {
                RequireKey(key);
                ((IDictionary)_settings)[key] = value;
            }
        }

        #endregion

        #region ICollection Members

        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)_settings).CopyTo(array, index);
        }

        int ICollection.Count
        {
            get
            {
                return _settings.Count;
            }
        }

        bool ICollection.IsSynchronized
        {
            get
            {
                return ((ICollection)_settings).IsSynchronized;
            }
        }

        object ICollection.SyncRoot
        {
            get
            {
                return ((ICollection)_settings).SyncRoot;
            }
        }

        #endregion
    }
}