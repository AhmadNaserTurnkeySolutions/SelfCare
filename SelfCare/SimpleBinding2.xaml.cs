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

namespace SelfCare
{
    public partial class SimpleBinding2 : PhoneApplicationPage
    {
        public SimpleBinding2()
        {
            InitializeComponent();
            DataContext = new List<Data>() {
            new Data(){
             id= -1,
            slug= "c",
            title= "All",
            description= "",

            }
            ,
                new Data(){
             id= 21,
            slug= "c",
            title= "C#",
            description= "",

            },
                            new Data(){
             id= 16,
            slug= "c",
            title= "Programmers",
            description= "",

            }
            
            };
        }

        private void Pivot_LoadingPivotItem(object sender, PivotItemEventArgs e)
        {


            var C = (Data)e.Item.DataContext;
            C.Loader();

        }
    }
}