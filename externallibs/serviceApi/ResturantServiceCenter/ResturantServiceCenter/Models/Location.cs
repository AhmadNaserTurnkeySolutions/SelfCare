using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResturantServiceCenter.Models
{
    public class Location
    {

        public int Id{set;get;}
         public double lat{set;get;}
         public double lng{set;get;}
         public string address{set;get;}
         public double distance { set; get; }
        


    }
}