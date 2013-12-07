using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using SelfCare.Entities;
namespace SelfCare
{
    public partial class IsolatedStorageEx1 : PhoneApplicationPage
    {
        Data myData { set; get; }
    

        public IsolatedStorageEx1()
        {
            InitializeComponent();
            myData = new Data() { url=@"http://google.com",is_silhouette=true};
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            // txtInput is a TextBox defined in XAML.
            if (!settings.Contains("CustomData"))
            {
                settings.Add("CustomData", myData);
            }
            else
            {
                settings["CustomData"] = myData;
            }
            settings.Save();
        }

        private void btnDisplay_Click(object sender, RoutedEventArgs e)
        {
            // txtDisplay is a TextBlock defined in XAML.
            txtDisplay.Text = "Custom Data: ";
            if (IsolatedStorageSettings.ApplicationSettings.Contains("CustomData"))
            {
               
            var c=    IsolatedStorageSettings.ApplicationSettings["CustomData"] as Data;
            txtDisplay.Text += c.url + "\n" + c.is_silhouette;
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("CustomData"))
            {
                IsolatedStorageSettings.ApplicationSettings.Remove("CustomData");
            }
        }
















        private void btnWrite_Click(object sender, RoutedEventArgs e)
        {
            // Get the local folder.
            System.IO.IsolatedStorage.IsolatedStorageFile local =
                System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication();

            // Create a new folder named DataFolder.
            if (!local.DirectoryExists("DataFolder"))
                local.CreateDirectory("DataFolder");

            // Create a new file named DataFile.txt.
            using (var isoFileStream =
                    new System.IO.IsolatedStorage.IsolatedStorageFileStream(
                        "DataFolder\\DataFile.txt",
                        System.IO.FileMode.OpenOrCreate,
                            local))
            {
                // Write the data from the textbox.
                using (var isoFileWriter = new System.IO.StreamWriter(isoFileStream))
                {
                    isoFileWriter.WriteLine(this.textBox1.Text);
                }
            }

            // Update UI.
            this.btnWrite.IsEnabled = false;
            this.btnRead.IsEnabled = true;
        }



        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            // Obtain a virtual store for the application.
            System.IO.IsolatedStorage.IsolatedStorageFile local =
                System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication();

            // Specify the file path and options.
            using (var isoFileStream =
                    new System.IO.IsolatedStorage.IsolatedStorageFileStream
                        ("DataFolder\\DataFile.txt", System.IO.FileMode.Open, local))
            {
                // Read the data.
                using (var isoFileReader = new System.IO.StreamReader(isoFileStream))
                {
                    this.textBlock1.Text = isoFileReader.ReadLine();
                }
            }
            // Update UI.
            this.btnWrite.IsEnabled = true;
            this.btnRead.IsEnabled = false;
        }





    }
}