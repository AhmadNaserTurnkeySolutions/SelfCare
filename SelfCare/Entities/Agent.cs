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
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
namespace SelfCare.Entities
{
     [Table]
    public class Agent
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }
        [Column()]
        public string EmailAddress { get; set; }
        [Column()]
        public string UserName { get; set; }
        [Column()]
        public string Password { get; set; }
        [Column()]
        public string Agency { get; set; }
        [Column()]
        public bool? IsUndercover { get; set; }
        [Column()]
        public string Proficiency { get; set; }
        [Column()]
        public string RecordCreatedDateTime { get; set; }

        public List<Address> Addresses { get; set; }

        public void Save()
        {
            // Send data to central database server
        }
    }
}
