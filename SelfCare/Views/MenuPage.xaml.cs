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
            this.image_Tap(sender, e);
            MessageBox.Show("hi");
        }

        private void image5_Tap(object sender, GestureEventArgs e)
        {

            this.image_Tap(sender, e);
            NavigationService.Navigate(new Uri("/Views/ResturantMenu.xaml", UriKind.Relative));
            
        }

        private void image4_Tap(object sender, GestureEventArgs e)
        {
this.image_Tap(sender, e);
            string t1 = "ax";
            string t2 = "ax2";
            string request = "/Views/SecondPage.xaml?text1=" + t1 + "&text2=" + t2;
            //NavigationService.Navigate(new Uri("/SecondPage.xaml", UriKind.Relative));
            NavigationService.Navigate(new Uri(request, UriKind.Relative));
        }

        private void image7_Tap(object sender, GestureEventArgs e)
        {
            this.image_Tap(sender, e);
            NavigationService.Navigate(new Uri("/Views/FbPage.xaml", UriKind.Relative));
   }

        private void image8_Tap(object sender, GestureEventArgs e)
        {
            this.image_Tap(sender, e);
            NavigationService.Navigate(new Uri("/Views/WebServiceMenu.xaml", UriKind.Relative));
        }

        private void image6_Tap(object sender, GestureEventArgs e)
        {
            this.image_Tap(sender, e);
            NavigationService.Navigate(new Uri("/Views/UploadImage.xaml", UriKind.Relative));

        }

        private void image2_Tap(object sender, GestureEventArgs e)
        {
            this.image_Tap(sender, e);
            NavigationService.Navigate(new Uri("/Views/IsolatedStorageEx1.xaml", UriKind.Relative));
            
        }

        private void image3_Tap(object sender, GestureEventArgs e)
        {
            this.image_Tap(sender, e);
            NavigationService.Navigate(new Uri("/Views/LocalDatabase.xaml", UriKind.Relative));
        }

        private void image9_Tap(object sender, GestureEventArgs e)
        {
            this.image_Tap(sender,e);
            NavigationService.Navigate(new Uri("/Views/Maps.xaml", UriKind.Relative));
        }

     private void image_Tap(object sender, GestureEventArgs e)
        {
            Image image = (Image)sender;
            var s ="";

            if (image.Tag!=null) { s = image.Tag.ToString()+ ""; }

            MessageBox.Show(s);

        }

           


        }

    

}