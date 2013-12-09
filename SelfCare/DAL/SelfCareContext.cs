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
using System.Data.Linq;
using System.Linq;
using SelfCare.Entities;
using System.Collections.Generic;
namespace SelfCare.DAL
{

    public class SelfCareContext : DataContext
    {
        public SelfCareContext() : base("Data Source=isostore:/SelfCare.sdf")
        {
           
        }



        public Table<Contact> Contacts
        {
            get
            {
                return this.GetTable<Contact>();
            }
        }


        public Table<Agent> Agents
        {
            get
            {
                return this.GetTable<Agent>();
            }
        }


        public Table<User> Users
        {
            get
            {
                return this.GetTable<User>();
            }
        }


    }
}
