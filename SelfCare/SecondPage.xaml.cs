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

            InitializeComponent();


            DataContext = new[] {
                                new Category(){
             id= -1,
            slug= "c",
            title= "All",
            description= "",

            }
            ,
                new Category(){
             id= 21,
            slug= "c",
            title= "C#",
            description= "",

            },
                            new Category(){
             id= 16,
            slug= "c",
            title= "Programmers",
            description= "",

            },
                            new Category(){
             id= 1,
            slug= "c",
            title= "Programming",
            description= "",

            },
                            new Category(){
             id= 20,
            slug= "c",
            title= "SalesForce",
            description= "",

            },
                            new Category(){
             id= 17,
            slug= "c",
            title= "Study Abroad",
            description= "",

            },
                            new Category(){
             id= 18,
            slug= "c",
            title= "Tech",
            description= "",

            },
                            new Category(){
             id= 8,
            slug= "c",
            title= "Technical Articles",
            description= "",

            },
                            new Category(){
             id= 19,
            slug= "c",
            title= "Training",
            description= "",

            },
                            new Category(){
             id= 23,
            slug= "c",
            title= "WP7",
            description= "",

            }

            };



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
           // C.LoadCategories();
            C.LoadPostsPerCategory();
        }

     




    }
}