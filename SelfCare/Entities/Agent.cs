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

namespace SelfCare.Entities
{
    public class Agent
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Agency { get; set; }
        public bool? IsUndercover { get; set; }
        public string Proficiency { get; set; }
        public string RecordCreatedDateTime { get; set; }

        public void Save()
        {
            // Send data to central database server
        }
    }
}
