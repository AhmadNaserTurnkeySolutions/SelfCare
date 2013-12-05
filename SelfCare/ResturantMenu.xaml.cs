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
    public partial class ResturantMenu : PhoneApplicationPage
    {
        public static int count = 0;

        public ResturantMenu()
        {
            InitializeComponent();

            DataContext = new List<CategoryMenux>() 
            {
         

                          new CategoryMenux(){
            CategoryName="PIZAA",
            CategoryDescription="hint:MASAFINA,Chicken,Vegable...."
            },
            new CategoryMenux(){
            CategoryName="Salads",
            CategoryDescription="hint:salads,turkish,chicken salads...."
            },
                      new CategoryMenux(){
            CategoryName="Drinks",
            CategoryDescription="hint:Cola,Tea,Caffe,XL...."
            },
                      new CategoryMenux(){
            CategoryName="MEALS",
            CategoryDescription="hint:subs,fried chicken...."
            }
            
            };
        }

        private void Pivot_LoadingPivotItem(object sender, PivotItemEventArgs e)
        {
      
           CategoryMenux CM= (CategoryMenux)e.Item.DataContext;
        
            CM.MenuItemsLoader();
        }
    }
}