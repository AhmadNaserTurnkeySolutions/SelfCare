﻿using System;
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
        private string _mail;
        private bool _true=true;
        private bool _false =false;
        private int _val = 0;

        public MySettings() : base("MySettings.xml") { }





        // The default value of our settings
         [DefaultValue(true)]
        public  bool TaxExemptSetting
         {
            get { return _true; }
            set
            {
                _true = value;
                NotifyPropertyChanged("TaxExemptSetting");
            }
        }


         [DefaultValue(0)]
        public  int StateSetting
         {
             get { return _val; }
             set
             {
                 _val = value;
                 NotifyPropertyChanged("StateSetting");
             }
         }


         [DefaultValue(true)]
        public  bool NextDayShippingSetting
         {
             get { return _true; }
             set
             {
                 _true = value;
                 NotifyPropertyChanged("NextDayShippingSetting");
             }
         }


         [DefaultValue(false)]
        public  bool TwoDayShippingSetting
         {
             get { return _false; }
             set
             {
                 _false = value;
                 NotifyPropertyChanged("TwoDayShippingSetting");
             }
         }

         [DefaultValue(false)]
        public  bool GroundShippingSetting
         {
             get { return _false; }
             set
             {
                 _false = value;
                 NotifyPropertyChanged("GroundShippingSetting");
             }
         }
         [DefaultValue("ahmadnassr@gmail.com")]
        public  string EmailSetting
         {
             get { return _mail; }
             set
             {
                 _mail = value;
                 NotifyPropertyChanged("EmailSetting");
             }
         }








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