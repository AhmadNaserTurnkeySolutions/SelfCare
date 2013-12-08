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
using System.IO;
using System.IO.IsolatedStorage;

namespace SelfCare.DAL
{
    public  class ImageBytesUtils
    {
        //byte[] imagtobyte(BitmapImage image,Stream streamx)
        public static byte[] imagtobyte(Stream streamx)
        {
            //BitmapImage imageSource = image;
            Stream stream = streamx;
            BinaryReader binaryReader = new BinaryReader(stream);
            var currentImageInBytes = new byte[0];

            currentImageInBytes = binaryReader.ReadBytes((int)stream.Length);
            //stream.Position = 0;
            //imageSource.SetSource(stream);

            return currentImageInBytes;
        }

        public static BitmapImage bytetoimage(byte[] imageBytes)
        {
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                BitmapImage im = new BitmapImage();
              
                im.SetSource(ms);
                return im;
            }
        }
        public static Stream bytetoStream(byte[] imageBytes)
        {
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
               
                return ms;
            }
        }
        //Orientation.Vertical
        public static string SaveImage(Stream imageStream, int orientation, int quality)
        {
            var fileName = Guid.NewGuid().ToString();
            using (var isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isolatedStorage.FileExists(fileName))
                    isolatedStorage.DeleteFile(fileName);

                IsolatedStorageFileStream fileStream = isolatedStorage.CreateFile(fileName);
                BitmapImage bitmap = new BitmapImage();
                bitmap.SetSource(imageStream);

                WriteableBitmap wb = new WriteableBitmap(bitmap);
                wb.SaveJpeg(fileStream, wb.PixelWidth, wb.PixelHeight, orientation, quality);
                fileStream.Close();
            }
            return fileName;
        }

        public static BitmapImage GetImage(string fileName)
        {
            BitmapImage bitmap = new BitmapImage();
            using (var myFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (myFile.FileExists(fileName))
                {

                    String sFile = fileName;
                   
                    //myFile.DeleteFile(sFile);
                    if (!myFile.FileExists(sFile))
                    {
                        IsolatedStorageFileStream dataFile = myFile.CreateFile(sFile);
                        dataFile.Close();
                    }

                   // var resource = Application.GetResourceStream(new Uri(sFile, UriKind.Relative));
                    StreamReader reader = new StreamReader(new IsolatedStorageFileStream(sFile, FileMode.Open, myFile));

                  // var text = reader.ReadToEnd();

                    reader.Close();
                    bitmap.SetSource(reader.BaseStream);
                    //StreamReader streamReader = new StreamReader(resource.Stream);
                  //  string rawData = streamReader.ReadToEnd();
                    
                    return bitmap;
                }
            }
            return bitmap;
        }




    }
}
