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
using SelfCare.Entities;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;

namespace SelfCare
{
    public partial class MenuPage : PhoneApplicationPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

  

        private void image1_Tap(object sender, GestureEventArgs e)
        {
            MessageBox.Show("hi");
        }

        private void image5_Tap(object sender, GestureEventArgs e)
        {
           


            string t1 = "ax";
            string t2 = "ax2";
            string request = "/SecondPage.xaml?text1=" + t1 + "&text2=" + t2;
            //NavigationService.Navigate(new Uri("/SecondPage.xaml", UriKind.Relative));
            NavigationService.Navigate(new Uri(request, UriKind.Relative));
        }

        private void image4_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ResturantMenu.xaml", UriKind.Relative));
        }

        private void image7_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/FbPage.xaml", UriKind.Relative));
   }

        private void image8_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/WebServiceMenu.xaml", UriKind.Relative));
        }

    


    
        




    }
}