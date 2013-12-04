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

using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



using SelfCare.Entities;
using System.IO.IsolatedStorage;
using System.IO;
using Microsoft.Phone.Shell;


namespace SelfCare
{
    public partial class UpdatePage : PhoneApplicationPage
    {
        PhotoChooserTask photoChooserTask;
      //  WriteableBitmap myImage;
        string imageString;
        int ImageWidth, ImageHeight;
        public UpdatePage()
        {
            InitializeComponent();
            photoChooserTask = new PhotoChooserTask();
            ImageWidth = (int)image1.Width;
            ImageHeight = (int)image1.Height;
            photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);
        }

        private void image1_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

            

        }

        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
              //  MessageBox.Show(e.ChosenPhoto.Length.ToString());

              imageString = e.OriginalFileName;
   

                BitmapImage  myImage = new BitmapImage();
                myImage.SetSource(e.ChosenPhoto);
                PhoneApplicationService.Current.State["photo"] = myImage;
                image1.Source = myImage;
          
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            photoChooserTask.Show();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Person person = new Person();

            person.FirstName = firstName.Text;
            person.LastName = lastName.Text;
            person.Month = month.Text;
            person.Day = day.Text;
            person.year = year.Text;
            person.Gender = gender.Text;
            person.Image = imageString;
            person.ID = id.Text;
            person.Email = email.Text;
            person.Phone = phone.Text;


            string json = JsonConvert.SerializeObject(person);
            NavigationService.Navigate(new Uri(string.Format("/PersonData.xaml?parameter={0}", json), UriKind.Relative));

        }


        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var x = (TextBox)sender;
            Color color = new Color() { R = 146 , G = 192, B = 224, A = 255};
            x.Background = new SolidColorBrush(color);
            x.Foreground = new SolidColorBrush(new Color() { R = 255, G = 255, B = 255, A = 255 });
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var x = (TextBox)sender;
            Color color = new Color() { R = 255, G = 255, B = 255, A = 255 };
            x.Background = new SolidColorBrush(color);
            x.Foreground = new SolidColorBrush(new Color() { R = 0, G = 0, B = 0, A = 255 });
        }



 
    }
}