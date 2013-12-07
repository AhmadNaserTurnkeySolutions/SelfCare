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

using System.Windows.Media.Imaging;

namespace SelfCare.Entities
{
    public class Person
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Month { set; get; }
        public string Day { set; get; }
        public string year { set; get; }
        public string Gender { set; get; }
        public string ID { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public string Image { set; get; }
        public byte[] Imagebytes { set; get; }
    }
}
