using System;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace ResturantServiceCenter.Models
{
    public class CategoryMenu
    {
        public int Id { set; get; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public bool IsDataLoaded { set; get; }

      
      
    }
   
    public class CategoryItem : IEquatable<CategoryItem>
{
        public int Id { set; get; }
        public string Name { get; set; }
        public int price { get; set; }

        public virtual CategoryMenu CategoryMenu { set; get; }
        public virtual int CategoryMenuId { set; get; }

        public bool Equals(CategoryItem other)
        {
            if (other == null)
                return false;

            return this.Name == other.Name;
        }
}
}
