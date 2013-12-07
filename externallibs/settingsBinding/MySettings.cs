using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SelfCare.Settings;
using System.ComponentModel;

namespace SelfCare
{
    /// <summary>
    /// My settings class for storing application data and setting specific to
    /// the user.
    /// </summary>
    public class MySettings : SettingsProvider
    {
        private bool _isFirstRun;
        private string _hello;

        public MySettings() : base("MySettings.xml") { }

        [DefaultValue("What's up?")]
        public string HelloWorld
        {
            get { return _hello; }
            set
            {
                _hello = value;
                NotifyPropertyChanged("HelloWorld");
            }
        }

        [DefaultValue(true)]
        public bool IsFirstRun
        {
            get { return _isFirstRun; }
            set
            {
                bool old = _isFirstRun;
                _isFirstRun = value;
                if (value != old)
                {
                    NotifyPropertyChanged("IsFirstRun");
                }
            }
        }
    }
}