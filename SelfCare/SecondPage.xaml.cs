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
using System.IO;
using System.Runtime.Serialization.Json;
using SelfCare.Entities;
using SelfCare.DAL;


namespace SelfCare
{
    public partial class SecondPage : PhoneApplicationPage
    {
        public SecondPage()
        {
            RootObjectDao d = new RootObjectDao();
            InitializeComponent();
           

            DataContext = d.cats;



        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
    
           NavigationService.Navigate(new Uri("/MainPage.xaml?msg=" + "ahmadnaser", UriKind.Relative));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string t1 = "";
            string t2 = "";
        NavigationContext.QueryString.TryGetValue("text1", out t1);

        NavigationContext.QueryString.TryGetValue("text2", out t2);
        //this.textBlock1.Text = t1 + " " + t2;
    

        }

        private void Pivot_LoadingPivotItem(object sender, PivotItemEventArgs e)
        {
            var C = (Category)e.Item.DataContext;
            C.LoadCategories();
            C.LoadCategoryPerPost();
        }

     




    }
}