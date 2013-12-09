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
using Newtonsoft.Json;
using SelfCare.Entities;

namespace SelfCare
{
    public partial class FbPage : PhoneApplicationPage
    {
        public FbPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += webClient_DownloadStringCompleted;
            ProgressBarRequest.Visibility = System.Windows.Visibility.Visible;
            webClient.DownloadStringAsync(new Uri("http://graph.facebook.com/" + this.textBox1.Text + "?fields=username,link,gender,picture.type(normal)"));
        }


        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(e.Result))
                {
                    //Parse JSON result as POCO 
                    var root1 = JsonConvert.DeserializeObject<FbRootObject>(e.Result);
                    this.DataContext = root1;

                    ImageBrush imageBrush = new ImageBrush();

                    string info = string.Format("Id : {0}\nUserName : {1}\n{2}", root1.username, root1.id, root1.gender);
                    txtInfo.Text = info;
                    imageBrush.ImageSource = new BitmapImage(new Uri(root1.picture.data.url));
                    imgprofile.Background = imageBrush;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                ProgressBarRequest.Visibility = System.Windows.Visibility.Collapsed;
            }

        }

        
    }
}