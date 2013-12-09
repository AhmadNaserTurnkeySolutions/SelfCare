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
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Diagnostics;

namespace SelfCare.DAL
{
    public class ImagesUtils 
    {

        public static BitmapImage SaveAndLoadToJpeg(Stream stream)
        {

            var filename = SaveToJpeg(stream);//, @"http://i.s-microsoft.com/global/ImageStore/PublishingImages/logos/hp/logo-lg-1x.png"); ;
            var filename2 = LoadImageFromIsolatedStorage(filename);
            return filename2;
        }

        public static string SaveToJpeg(Stream stream)//return file name
        {
            var strImageName = Guid.NewGuid().ToString()+".jpg";
            using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (iso.FileExists(strImageName))
                {
                    iso.DeleteFile(strImageName);
                }


                using (IsolatedStorageFileStream isostream = iso.CreateFile(strImageName))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.SetSource(stream);
                    WriteableBitmap wb = new WriteableBitmap(bitmap);
                    // Encode WriteableBitmap object to a JPEG stream. 
                    Extensions.SaveJpeg(wb, isostream, wb.PixelWidth, wb.PixelHeight, 0, 85);
                    isostream.Close();
                }
            }
            return strImageName;
        }






        public static BitmapImage LoadImageFromIsolatedStorage(string strImageName)
        {
            // The image will be read from isolated storage into the following byte array 
            byte[] data;

            BitmapImage bi = new BitmapImage();
            // Read the entire image in one go into a byte array 
            try
            {
                using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    // Open the file - error handling omitted for brevity 
                    // Note: If the image does not exist in isolated storage the following exception will be generated: 
                    // System.IO.IsolatedStorage.IsolatedStorageException was unhandled  
                    // Message=Operation not permitted on IsolatedStorageFileStream  
                    using (IsolatedStorageFileStream isfs = isf.OpenFile(strImageName, FileMode.Open, FileAccess.Read))
                    {
                        // Allocate an array large enough for the entire file 
                        data = new byte[isfs.Length];
                        // Read the entire file and then close it 
                        isfs.Read(data, 0, data.Length);
                        isfs.Close();
                    }
                }


                // Create memory stream and bitmap 
                MemoryStream ms = new MemoryStream(data);
              
                // Set bitmap source to memory stream 
                bi.SetSource(ms);
                // Create an image UI element Note: this could be declared in the XAML instead 
                Image image = new Image();
                // Set size of image to bitmap size for this demonstration 
                image.Height = bi.PixelHeight;
                image.Width = bi.PixelWidth;
                // Assign the bitmap image to the imageÃ¢â‚¬â„¢s source 
                image.Source = bi;
                // Add the image to the grid in order to display the bit map 
               // ContentPanel.Children.Add(image);
            }
            catch (Exception e)
            {
                // handle the exception 
                Debug.WriteLine(e.Message);
            }
            return bi;
        }



        public static void SaveToJpegOnline(byte[] sbytedata)
        {

            try
            {


                //string s = sbytedata.ToString();
                WebClient wc = new WebClient();
                Uri u = new Uri("http://localhost:2819/File/Upload");
               wc.OpenWriteCompleted += new OpenWriteCompletedEventHandler(wc_OpenWriteCompleted);
                wc.OpenWriteAsync(u, "POST", sbytedata);

            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error saving to online server "+Ex.Message);
            }

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

                string s = e.Result.ToString();

                var x = e.Result;

            }
        }









    }
}
