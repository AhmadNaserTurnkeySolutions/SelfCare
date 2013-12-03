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
    public class MyProfile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get { return this.FirstName + " " + this.LastName; } }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string NumberID { get; set; }
        public string Phone { get; set; }
        public string Emali { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public float BMI { get; set; }
        public float Temp { get; set; }
        public float Pulse { get; set; }
        public float Res_Rate { get; set; }
        public float Pain { get; set; }
        public float BP_Systolic { get; set; }
        public float BP_Diastolic { get; set; }

        public void Save()
        {
            // Send data to central database server
        }
    }
}
