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
using System.Data.Linq.Mapping;
using System.Windows.Media.Imaging;
using SelfCare.DAL;

namespace SelfCare.Entities
{
    [Table]
    public class User
    {

        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }
        [Column()]
        public string Fname { set; get; }
        [Column()]
        public string Lname { set; get; }
        [Column()]
        public string Address { set; get; }
        [Column()]
        public string Phone { set; get; }
        [Column()]
        public string Photo { set; get; }
        public BitmapImage BI { get { return ImagesUtils.LoadImageFromIsolatedStorage(Photo); } }

    }
}
