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

    }
}
