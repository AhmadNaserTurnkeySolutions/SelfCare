using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SelfCare.Entities
{
    public class CategoryMenux
    {

        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public bool IsDataLoaded { set; get; }

        public ObservableCollection<CategoryItem> Items { set; get; }

        public CategoryMenux() 
        {

           Items = new ObservableCollection<CategoryItem>();
        
        }

        public void MenuItemsLoader() 
        {


            string cat1 = "PIZAA";
            string cat2 = "Salads";
            string cat3 = "Drinks";
            string cat4 = "MEALS";


            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                if (this.CategoryName.Equals(cat1))
                {
                    var item1 = new CategoryItem() { Name = "MASAFINA", price = 20 };

                   

                    /*//Solution # 1
                    
                     //Solving the problem using flag for loaded or not
                     
                    if (!this.IsDataLoaded)
                    {
                    Items.Add(item1);
                    this.IsDataLoaded = true;
                    }
                     * */



                    /*//Solution # 2
                    
                      //Solving the problem using IEquatable<CategoryItem> Contains for Equals 
                      * */

                    if (Items.Contains(item1) == false)
                        Items.Add(item1);


                }

                if (this.CategoryName.Equals(cat2)){
                
                    if(!this.IsDataLoaded){
                    Items.Add(new CategoryItem() { Name = "Chicken Salad", price = 20 });
                    Items.Add(new CategoryItem() { Name = "Vegatble", price = 50 });
                    this.IsDataLoaded = true;
                    }
                }
                if (this.CategoryName.Equals(cat3))
                {
                      if(!this.IsDataLoaded){
                    Items.Add(new CategoryItem() { Name = "Cola", price = 20 });
                    Items.Add(new CategoryItem() { Name = "pepsi", price = 50 });
                    this.IsDataLoaded = true;
                      }
                }

                if (this.CategoryName.Equals(cat4))
                {
                      if(!this.IsDataLoaded){
                    Items.Add(new CategoryItem() { Name = "Chicken Sandwish", price = 20 });
                    Items.Add(new CategoryItem() { Name = "Tuna Sandwish", price = 50 });
                    Items.Add(new CategoryItem() { Name = "Friez Sandwish", price = 50 });
                    this.IsDataLoaded = true;
                      }
                }
        
            }
 );






            
        }

        private bool CheckIfExist(ObservableCollection<CategoryItem> Items, CategoryItem categoryItem)
        {

            return true;
        }


      
    }
   
    public class CategoryItem : IEquatable<CategoryItem>
{
        public string Name { get; set; }
        public int price { get; set; }

        public bool Equals(CategoryItem other)
        {
            if (other == null)
                return false;

            return this.Name == other.Name;
        }
}
}
