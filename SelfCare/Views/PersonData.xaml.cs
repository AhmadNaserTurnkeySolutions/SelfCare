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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Windows.Media.Imaging;


using SelfCare.Entities;
using Microsoft.Phone.Shell;
using SelfCare.DAL;
using System.Collections;

namespace SelfCare
{
    public partial class PersonData : PhoneApplicationPage
    {
        public PersonData()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);




            //string parameter = this.NavigationContext.QueryString["parameter"];
            //Person person = JsonConvert.DeserializeObject<Person>(parameter);
            Person person = PhoneApplicationService.Current.State["preson"] as Person;
            string print = "Name: " + person.FirstName + " " + person.LastName + "\n" +
                           "Birthdate: " + person.Day + "-" + person.Month + "-" + person.year + "\n" + 
                            "Gender: " + person.Gender + "\n" +
                            "ID: " + person.ID + "\n" +
                            "Phone: " + person.Phone + "\n" +
                            "Email: " + person.Email + "\n" + "photo: "+person.Image;

      
          //  BitmapImage bitmapGet = PhoneApplicationService.Current.State["photo"] as BitmapImage;
            BitmapImage im = ImageBytesUtils.bytetoimage(person.Imagebytes);

            image1.Source = im; //new BitmapImage(new Uri(person.Image, UriKind.Relative));
            image1.Width = 400;
           image1.Height = 400;
            textBlock1.Text = print;

        }




    }
}