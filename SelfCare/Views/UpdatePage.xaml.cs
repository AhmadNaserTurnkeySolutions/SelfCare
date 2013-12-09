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
using SelfCare.DAL;
using System.Text;
using System.Collections;


namespace SelfCare
{
    public partial class UpdatePage : PhoneApplicationPage
    {
        PhotoChooserTask photoChooserTask;
      //  WriteableBitmap myImage;
        string imageString;
      Person person;
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


                BitmapImage myImage = new BitmapImage();
                myImage.SetSource(e.ChosenPhoto);
                image1.Source = myImage;
                MemoryStream ms = new MemoryStream();
                WriteableBitmap wb = new WriteableBitmap(myImage);
                wb.SaveJpeg(ms, myImage.PixelWidth, myImage.PixelHeight, 0, 100);
                byte[] imageBytes = ms.ToArray();


                person.Imagebytes = imageBytes;
          //      person.Imagebytes = ConvertStreamToImage(e.ChosenPhoto);



        








                //string json = JsonConvert.SerializeObject(person);
               // MessageBox.Show(person.Imagebytes.ToString());
                //NavigationService.Navigate(new Uri(string.Format("/PersonData.xaml?parameter={0}", json), UriKind.Relative));


               
          
            }
         



      





        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            photoChooserTask.Show();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
     
            //string json = JsonConvert.SerializeObject(this.person);
            //Uri url = new Uri(string.Format("/PersonData.xaml?parameter={0}", json), UriKind.Relative);

            try
            {

                byte[] sbytedata = this.person.Imagebytes;// ReadToEnd(e.ChosenPhoto);
                string s = sbytedata.ToString();
                WebClient wc = new WebClient();
                Uri u = new Uri("http://localhost:2819/File/Upload");
                wc.OpenWriteCompleted += new OpenWriteCompletedEventHandler(wc_OpenWriteCompleted);
                wc.OpenWriteAsync(u, "POST", sbytedata);

                PhoneApplicationService.Current.State["preson"] = this.person;

                Uri url = new Uri(string.Format("/PersonData.xaml"), UriKind.Relative);
                NavigationService.Navigate(url);
            }
            catch(Exception Ex)
            {
                MessageBox.Show("No Photo is selected");
            }

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


     
        public static void wc_OpenWriteCompleted(object sender, OpenWriteCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                object[] objArr = e.UserState as object[];
                byte[] fileContent = e.UserState as byte[];

                Stream outputStream = e.Result;
                outputStream.Write(fileContent, 0, fileContent.Length);
                outputStream.Flush();
                outputStream.Close();
                string s = e.Result.ToString(); ;

            }
        }
        public static byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = stream.Position;
            stream.Position = 0;

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                stream.Position = originalPosition;
            }
        }
    }
}