// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Reflection;
using System.Windows;

namespace SelfCare.Settings
{
    /// <summary>
    /// A base class for a settings provider. Any properties in the derived
    /// class will be used as property names, types, and their values read.
    /// </summary>
    /// <remarks>There is no clear versioning story. Type changes should require
    /// property name changes at this time.</remarks>
    public abstract class SettingsProvider : PropertyChangedBase
    {
        /// <summary>
        /// An empty object array.
        /// </summary>
        private static readonly object[] EmptyObjectArray = new object[] { };

        /// <summary>
        /// Backing field for the filename to use.
        /// </summary>
        private string _filename;

        /// <summary>
        /// Backing store for the data file with the settings.
        /// </summary>
        private IsolatedStorageFile _store;

        /// <summary>
        /// The set of property names and reflected PropertyInfo instances.
        /// </summary>
        private Dictionary<string, PropertyInfo> _properties;

        /// <summary>
        /// The set of default values read from the derived class/es.
        /// </summary>
        private Dictionary<string, object> _defaultValues;

        /// <summary>
        /// The file to store in isolated storage with the settings.
        /// </summary>
        private IsolatedStorageSettingsFile _settingsFile;

        /// <summary>
        /// A value indicating whether the class has initialized yet.
        /// </summary>
        private bool _initialized;

        /// <summary>
        /// Backing field for a value indicating whether to always save.
        /// </summary>
        private bool _alwaysSaveOnChange;

        /// <summary>
        /// Gets or sets a value indicating whther to always save on change.
        /// </summary>
        public bool AlwaysSaveOnChange
        {
            get { return _alwaysSaveOnChange; }
            set { _alwaysSaveOnChange = value; }
        }

        /// <summary>
        /// Initializes a new instance of the settings provider, using a
        /// specific filename for the storage of settings data that is not the
        /// default value.
        /// </summary>
        /// <param name="filename">The filename to use.</param>
        public SettingsProvider(string filename) : this()
        {
            Filename = filename;
        }

        /// <summary>
        /// Initializes a new instance of the settings provider.
        /// </summary>
        public SettingsProvider()
        {
            _properties = new Dictionary<string, PropertyInfo>();

            // Connect to the exit event so that the exit code always runs.
            Application.Current.Exit += OnExit;

            Type type = this.GetType();
            Type b = typeof(SettingsProvider);
            List<string> standard = new List<string>();
            foreach (PropertyInfo pi in b.GetProperties())
            {
                standard.Add(pi.Name);
            }

            // Read all of the properties we can get to (no private reflection
            // in Silverlight)
            List<PropertyInfo> entire = new List<PropertyInfo>(type.GetProperties(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.FlattenHierarchy));
            foreach (PropertyInfo pi in entire)
            {
                if (!standard.Contains(pi.Name))
                {
                    _properties.Add(pi.Name, pi);
                }
            }

            // Stores the initial sets. By doing this once, we won't fire silly
            // property changed notifications twice when a default is overridden
            // by a user-stored value that is read in.
            Dictionary<string, object> initialValues = new Dictionary<string, object>();

            // Default values
            _defaultValues = new Dictionary<string, object>();
            foreach (PropertyInfo pi in _properties.Values)
            {
                DefaultValueAttribute dva;
                if (pi.TryGetAttribute(out dva))
                {
                    initialValues[pi.Name] = dva.Value;
                    _defaultValues[pi.Name] = dva.Value;
                }
            }

            // Read values from isolated storage
            if (!string.IsNullOrEmpty(_filename))
            {
                foreach (KeyValuePair<string, object> r in ReadValues())
                {
                    initialValues[r.Key] = r.Value;
                }
            }

            // Apply all at once
            foreach (string property in initialValues.Keys)
            {
                _properties[property].SetValue(this, initialValues[property], EmptyObjectArray);
                NotifyPropertyChanged(property);
            }

            _initialized = true;
        }

        /// <summary>
        /// Overrides the notify property changed to automatically save changed
        /// values to isolated storage.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        protected override void NotifyPropertyChanged(string propertyName)
        {
            base.NotifyPropertyChanged(propertyName);

            // Windows Phone doesn't fire Exit events the same as Silverlight 4,
            // so this always saves on change.
            if (_initialized && AlwaysSaveOnChange)
            {
                Save();
            }
        }

        /// <summary>
        /// Hooks up to the application exit event, and never throw an
        /// exception.
        /// </summary>
        /// <param name="sender">The source object.</param>
        /// <param name="e">The event data.</param>
        protected virtual void OnExit(object sender, EventArgs e)
        {
            try
            {
                Save();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Clears any custom settings.
        /// </summary>
        public void Clear()
        {
            if (_settingsFile != null)
            {
                _settingsFile.Clear();
            }
        }

        /// <summary>
        /// Stores the settings into isolated storage.
        /// </summary>
        public void Save()
        {
            if (_settingsFile != null && _properties != null)
            {
                foreach (string property in _properties.Keys)
                {
                    PropertyInfo pi = _properties[property];
                    object value = pi.GetValue(this, EmptyObjectArray);
                    if (_defaultValues.ContainsKey(property))
                    {
                        object defaultValue = _defaultValues[property];
                        if (value != defaultValue)
                        {
                            _settingsFile[property] = value;
                        }
                    }
                    else
                    {
                        _settingsFile[property] = value;
                    }
                }

                _settingsFile.Save();
            }
        }

        /// <summary>
        /// Gets or sets the filename to use for the settings in isolated
        /// storage.
        /// </summary>
        public string Filename
        {
            get { return _filename; }
            set
            {
                _filename = value;
                if (!DesignerProperties.IsInDesignTool)
                {
                    try
                    {
                        _store = IsolatedStorageFile.GetUserStoreForApplication();
                    }
                    catch
                    {
                    }
                }
                Reload();
            }
        }

        /// <summary>
        /// Reads the current values from isolated storage.
        /// </summary>
        /// <returns>Returns a dictionary of keys and object values.</returns>
        private Dictionary<string, object> ReadValues()
        {
            Dictionary<string, object> vs = new Dictionary<string, object>();

            try
            {
                if (_filename != null && _store != null)
                {
                    _settingsFile = new IsolatedStorageSettingsFile(_store, _filename);
                    foreach (string property in _properties.Keys)
                    {
                        PropertyInfo pi = _properties[property];
                        Type propertyType = pi.PropertyType;
                        object instance;
                        if (_settingsFile.TryGetValueOfType(property, propertyType, out instance))
                        {
                            vs[property] = instance;
                        }
                    }
                }
            }
            catch
            {
            }

            return vs;
        }

        /// <summary>
        /// Reloads the property values from another store.
        /// </summary>
        protected void Reload()
        {
            if (_properties == null || _properties.Count == 0)
            {
                return;
            }

            Dictionary<string, object> values = ReadValues();
            foreach (string property in values.Keys)
            {
                PropertyInfo pi = _properties[property];
                pi.SetValue(this, values[property], EmptyObjectArray);
            }
        }
    }
}