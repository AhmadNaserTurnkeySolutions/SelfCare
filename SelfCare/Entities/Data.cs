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
    public class Data
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int parent { get; set; }
        public int post_count { get; set; }

        public ObservableCollection<SubData> SubsData { set; get; }


        public Data()
        {

            SubsData = new ObservableCollection<SubData>();


        }

        public void Loader()
        {
       
            var dtemp = new List<SubData>() {
   
                new SubData(){
             id= 21,
            title= "Ali is here",
            description= "wow awesome ",

            },
                            new SubData(){
             id= 16,
            title= "Weather is so cold",
            description= "in rammallah , its 10 c",

            }
    };



            Deployment.Current.Dispatcher.BeginInvoke(() =>
    {
        
        foreach (var c in dtemp)
        {
            this.SubsData.Add(c);

           
        }
    }
    
    );

        }
    }

    public class SubData
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }

    }

}
