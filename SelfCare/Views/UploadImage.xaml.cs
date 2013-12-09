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
using System.IO;

namespace SelfCare
{
    public partial class UploadImage : PhoneApplicationPage
    {
        Stream choosenphoto = null;
        
        public UploadImage()
        {
            InitializeComponent();
        }

        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            PhotoChooserTask task = new PhotoChooserTask();
            task.Completed += task_Completed;
            task.Show();
        }

        private void task_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                //  MessageBox.Show(e.ChosenPhoto.Length.ToString());

                string imageString = e.OriginalFileName;


               
                ImageBrush imageBrush = new ImageBrush();

               Stream stream = e.ChosenPhoto;
               byte[] x= this.imagtobyte(stream);
               choosenphoto = stream;
               BitmapImage y = this.bytetoimage(x);
         
                imageBrush.ImageSource = y;
                imagePlace.Background = imageBrush;

            }

            else if (e.TaskResult != TaskResult.OK)
                return;

           

        }

        private void task_CompletedX(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                //  MessageBox.Show(e.ChosenPhoto.Length.ToString());

               string  imageString = e.OriginalFileName;


                BitmapImage myImage = new BitmapImage();
                ImageBrush imageBrush = new ImageBrush();
                myImage.SetSource(e.ChosenPhoto);
               
                imageBrush.ImageSource = myImage;
                imagePlace.Background = imageBrush;

            }

            else if (e.TaskResult != TaskResult.OK)
                return;

            const int BLOCK_SIZE = 4096;

            Uri uri = new Uri("http://localhost:2819/File/Upload", UriKind.Absolute);

            WebClient wc = new WebClient();
            wc.AllowReadStreamBuffering = true;
            wc.AllowWriteStreamBuffering = true;

            // what to do when write stream is open
            wc.OpenWriteCompleted += (s, args) =>
            {
                using (BinaryReader br = new BinaryReader(e.ChosenPhoto))
                {
                    using (BinaryWriter bw = new BinaryWriter(args.Result))
                    {
                        long bCount = 0;
                        long fileSize = e.ChosenPhoto.Length;
                        byte[] bytes = new byte[BLOCK_SIZE];
                        do
                        {
                            bytes = br.ReadBytes(BLOCK_SIZE);
                            bCount += bytes.Length;
                            bw.Write(bytes);
                        } while (bCount < fileSize);
                    }
                }
            };

            // what to do when writing is complete
            wc.WriteStreamClosed += (s, args) =>
            {
                MessageBox.Show("Send Complete");
            };

            // Write to the WebClient
            wc.OpenWriteAsync(uri, "POST");

        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            

                const int BLOCK_SIZE = 4096;

            Uri uri = new Uri("http://localhost:2819/File/Upload", UriKind.Absolute);

            WebClient wc = new WebClient();
            wc.AllowReadStreamBuffering = true;
            wc.AllowWriteStreamBuffering = true;

            // what to do when write stream is open
            wc.OpenWriteCompleted += (s, args) =>
            {
                using (BinaryReader br = new BinaryReader(choosenphoto))
                {
                    using (BinaryWriter bw = new BinaryWriter(args.Result))
                    {
                        long bCount = 0;
                        long fileSize = (choosenphoto).Length;
                        byte[] bytes = new byte[BLOCK_SIZE];
                        do
                        {
                            bytes = br.ReadBytes(BLOCK_SIZE);
                            bCount += bytes.Length;
                            bw.Write(bytes);
                        } while (bCount < fileSize);
                    }
                }
            };

            // what to do when writing is complete
            wc.WriteStreamClosed += (s, args) =>
            {
                MessageBox.Show("Send Complete");
            };

            // Write to the WebClient
            wc.OpenWriteAsync(uri, "POST");

        }

         //byte[] imagtobyte(BitmapImage image,Stream streamx)
        byte[] imagtobyte(Stream streamx)
        {
            //BitmapImage imageSource = image;
            Stream stream =streamx;
            BinaryReader binaryReader = new BinaryReader(stream);
           var currentImageInBytes = new byte[0];

            currentImageInBytes = binaryReader.ReadBytes((int)stream.Length);
            //stream.Position = 0;
            //imageSource.SetSource(stream);

            return currentImageInBytes;
        }

    BitmapImage bytetoimage(byte[] imageBytes)
             {
              using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
    {
        BitmapImage im = new BitmapImage();
        im.SetSource(ms);
       return im;
    }
             }
    }
}