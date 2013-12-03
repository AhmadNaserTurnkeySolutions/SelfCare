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
    public partial class SimpleBinding4 : PhoneApplicationPage
    {
        public SimpleBinding4()
        {
            InitializeComponent();

            DataContext = new List<Agent>()
            {
                new Agent()
                {
                UserName="Ahmad",
                Agency="PITA",
                IsUndercover=true,
                Password="mps",
                Proficiency="Master",
                RecordCreatedDateTime=DateTime.Now.ToString(),
                Addresses=new List<Address>()
                {
                new Address(){Name="Ramallah",Location="Palestine"},
                 new Address(){Name="Jordan",Location="AMaan"}
                }
                },
                     new Agent()
                {
                UserName="Samer",
                Agency="PICTE",
                IsUndercover=true,
                Password="mps",
                Proficiency="Diploma",
                RecordCreatedDateTime=DateTime.Now.ToString(),
                Addresses=new List<Address>()
                {
                new Address(){Name="Miami",Location="USA"},
                 new Address(){Name="Jordan",Location="AMaan"}
                }
                },
                     new Agent()
                {
                UserName="Nadeem",
                Agency="PITA",
                IsUndercover=true,
                Password="mps",
                Proficiency="Languages",
                RecordCreatedDateTime=DateTime.Now.ToString(),
                Addresses=new List<Address>()
                {
                new Address(){Name="Alaska",Location="USA"}
                }
                }

            };


        }
    }
}