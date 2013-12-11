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
using System.Windows.Media.Imaging;

namespace SelfCare
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor

        public object IM { get {
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = App.BG;

            return imageBrush;
        }
        }

        public object IM1
        {
            get
            {
   
                return App.BG;
            }
        }


        public MainPage()
        {
            InitializeComponent();
          //  ImageBrush imageBrush = new ImageBrush();
            //BitmapImage BG1 = new BitmapImage(new Uri("http://profile.ak.fbcdn.net/hprofile-ak-ash2/1118127_100004242222671_643383850_s.jpg"));
           // imageBrush.ImageSource = App.BG;
           // imgprofile.Background = imageBrush;

        
            DataContext = this;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

            string request = "/Views/MenuPage.xaml";

    
          NavigationService.Navigate(new Uri(request, UriKind.Relative));

        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var x = (TextBox)sender;
            x.Text = "";
           
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {

            
            var x = (TextBox)sender;
            if (String.IsNullOrEmpty(x.Text)) 
            {
                x.Text = x.Tag.ToString();
            
            }
           

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/RegistrationPage.xaml", UriKind.Relative));
            
        }

        private void hyperlinkButton1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/SimpleBinding.xaml", UriKind.Relative));
        }

        private void hyperlinkButton2_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/SimpleBinding2.xaml", UriKind.Relative));
        }

        private void hyperlinkButton3_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/SimpleBinding3.xaml", UriKind.Relative));
        }

        private void hyperlinkButton4_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/my.xaml", UriKind.Relative));
           
        }

        private void hyperlinkButton5_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("/Views/SimpleBinding4.xaml", UriKind.Relative));
        }

        private void hyperlinkButton6_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/UpdatePage.xaml", UriKind.Relative));
            
        }

        private void hyperlinkButton7_Click(object sender, RoutedEventArgs e)
        {


            NavigationService.Navigate(new Uri("/Views/SettingsPage.xaml", UriKind.Relative));
        }

        private void hyperlinkButton8_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/SettingsPage2.xaml", UriKind.Relative));
        }

    

        private void hyperlinkButton9_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/DisplayAllAgents.xaml", UriKind.Relative));

        }

        private void hyperlinkButton10_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/ContactCRUD.xaml", UriKind.Relative));
           
        }

        private void hyperlinkButton11_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/UserPage.xaml", UriKind.Relative));
          
        }
    }
}