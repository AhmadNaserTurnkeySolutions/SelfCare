﻿using System;
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

namespace SelfCare
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

    
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

            string request = "/MenuPage.xaml";
           
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
            NavigationService.Navigate(new Uri("/RegistrationPage.xaml", UriKind.Relative));
            
        }

        private void hyperlinkButton1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SimpleBinding.xaml", UriKind.Relative));
        }

        private void hyperlinkButton2_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SimpleBinding2.xaml", UriKind.Relative));
        }

        private void hyperlinkButton3_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SimpleBinding3.xaml", UriKind.Relative));
        }

        private void hyperlinkButton4_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/my.xaml", UriKind.Relative));
           
        }

        private void hyperlinkButton5_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("/SimpleBinding4.xaml", UriKind.Relative));
        }

        private void hyperlinkButton6_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/UpdatePage.xaml", UriKind.Relative));
            
        }

        private void hyperlinkButton7_Click(object sender, RoutedEventArgs e)
        {


            NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
        }

        private void hyperlinkButton8_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingsPage2.xaml", UriKind.Relative));
        }
    }
}