using ResturantServiceCenter.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ResturantServiceCenter.DAL
{
    public class ResturantContext : DbContext
    {
        public ResturantContext()
            : base("DefaultConnection") 
        {
        
        }

        public DbSet<CategoryMenu> Categories { set; get; }
        public DbSet<CategoryItem> Items { set; get; }


    }
}