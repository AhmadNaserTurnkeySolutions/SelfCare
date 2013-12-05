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
using Newtonsoft.Json;
using SelfCare.Entities;

namespace SelfCare
{
    public partial class WebServiceMenu : PhoneApplicationPage
    {
        public WebServiceMenu()
        {
            InitializeComponent();
        }

        private void Pivot_LoadingPivotItem(object sender, PivotItemEventArgs e)
        {
            CategoryMenu myC = (CategoryMenu)e.Item.DataContext;
            myC.LoadItems();


        }

       
            private void button1_Click(object sender, RoutedEventArgs e)
        {
        //    WebClient webClient = new WebClient();
        //    webClient.DownloadStringCompleted += webClient_DownloadStringCompleted;
        //    ProgressBarRequest.Visibility = System.Windows.Visibility.Visible;
        //    webClient.DownloadStringAsync(new Uri("http://localhost:2819/Category/Categories"));
        }


        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(e.Result))
                {
                    //Parse JSON result as POCO 
                    var root1 = JsonConvert.DeserializeObject<List<CategoryMenu>>(e.Result);

                  var x = root1.Count;

                    this.DataContext = root1;

                  //  string info = string.Format("Id : {0}\nUserName : {1}\n{2}", root1.username, root1.id, root1.gender);
 

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

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += webClient_DownloadStringCompleted;
            ProgressBarRequest.Visibility = System.Windows.Visibility.Visible;
            webClient.DownloadStringAsync(new Uri("http://localhost:2819/Category/Categories"));
        }






    }
}