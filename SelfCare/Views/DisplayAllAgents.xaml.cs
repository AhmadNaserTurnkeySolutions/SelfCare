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
using SelfCare.DAL;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;

namespace SelfCare
{
    public partial class DisplayAllAgents : PhoneApplicationPage
    {
        PhotoChooserTask photoChooserTask;

        public DisplayAllAgents()
        {
            InitializeComponent();
            photoChooserTask = new PhotoChooserTask();
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {

            AgentDAL dao = new AgentDAL();
            this.AgentsList.ItemsSource = dao.GetAllAgents();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            AgentDAL dao = new AgentDAL();
           // dao.DeleteAgentById(int.Parse(this.textBox1.Text));
        }

      

        void photoChooserTask_Completed(object sender, PhotoResult e)
        {


            if (e.TaskResult == TaskResult.OK)
            {
                //  MessageBox.Show(e.ChosenPhoto.ToString());
                  BitmapImage myImage = new BitmapImage();
                  myImage.SetSource(e.ChosenPhoto);
                 
                  ImageBrush imageBrush = new ImageBrush();
                  imageBrush.ImageSource = myImage;
                  this.imagepanel.Background = imageBrush;
                /*
                imageString = e.OriginalFileName;


            
                myImage.SetSource(e.ChosenPhoto);
                image1.Source = myImage;
                // PhoneApplicationService.Current.State["photo"] = myImage;

                person = new Person();

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

                MemoryStream ms = new MemoryStream();
                WriteableBitmap wb = new WriteableBitmap(myImage);
                wb.SaveJpeg(ms, myImage.PixelWidth, myImage.PixelHeight, 0, 100);
                byte[] imageBytes = ms.ToArray();


                person.Imagebytes = imageBytes;
                //      person.Imagebytes = ConvertStreamToImage(e.ChosenPhoto);

                */

            }










        }

        private void button2_Click_1(object sender, RoutedEventArgs e)
        {
            photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);
            photoChooserTask.Show();
        }
    }
}