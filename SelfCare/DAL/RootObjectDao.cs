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
using SelfCare.Entities;
using System.Runtime.Serialization.Json;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SelfCare.DAL
{
    public class RootObjectDao
    {

        public Category[] cats { set; get; }
        public RootObjectDao() 
        {
           
        
            cats = new[] {
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

       
     
    }
}
