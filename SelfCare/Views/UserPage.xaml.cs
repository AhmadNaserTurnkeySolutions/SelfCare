﻿using System;
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
using System.IO;
using SelfCare.Entities;

namespace SelfCare.Views
{
    public partial class UserPage : PhoneApplicationPage
    {

        UserDAL dao;
        byte[] choosenphoto = null;
       string choosenphotofile = null;
        PhotoChooserTask photoChooserTask;
        public UserPage()
        {
            InitializeComponent();

            photoChooserTask = new PhotoChooserTask();
            dao = new UserDAL();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);
            photoChooserTask.Show();
        }


        void photoChooserTask_Completed(object sender, PhotoResult e)
        {


            if (e.TaskResult == TaskResult.OK)
            {
                //  MessageBox.Show(e.ChosenPhoto.ToString());
             
                


              //  choosenphotofile = ImageBytesUtils.SaveImage(e.ChosenPhoto, 0, 100);

                //ImagesUtils.SaveAndLoadToJpeg(e.ChosenPhoto);
                ImageBrush imageBrush = new ImageBrush();
                BitmapImage myImage = new BitmapImage();


                #region 1-start(save image to offline storage (stream))

                string savedImagefilename = ImagesUtils.SaveToJpeg(e.ChosenPhoto);
                choosenphotofile = savedImagefilename;

                #endregion 1-start(save image to offline storage (stream))







                #region 2-start(read image from offline storage (stream))
                // 2 read image from offline storage (stream)
               myImage = ImagesUtils.LoadImageFromIsolatedStorage(savedImagefilename);// myImage;
               imageBrush.ImageSource = myImage;
               this.imagepanel.Background = imageBrush;

                #endregion start(read image to offline storage (stream))


               #region 3 save image to online server storage (bytes)
               // 2 save image to online server storage (bytes)
                MemoryStream ms = new MemoryStream();
                WriteableBitmap wb = new WriteableBitmap(myImage);
                wb.SaveJpeg(ms, myImage.PixelWidth, myImage.PixelHeight, 0, 100);
                byte[] imageBytes = ms.ToArray();

                ImagesUtils.SaveToJpegOnline(imageBytes);


               #endregion 3 save image to online server storage (bytes)



                /*   MemoryStream ms = new MemoryStream();
                WriteableBitmap wb = new WriteableBitmap(myImage);
                wb.SaveJpeg(ms, myImage.PixelWidth, myImage.PixelHeight, 0, 100);
                byte[] imageBytes = ms.ToArray();
                choosenphoto = imageBytes;

                */



            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            User c = new User();
            c.Address = this.txtAddress.Text;
            c.Fname = this.txtFname.Text;
            c.Lname = this.txtLName.Text;
            c.Phone = this.txtPhone.Text;
            c.Photo = choosenphotofile;
          //  c.Imagebytes = choosenphoto;
            dao.InsertUser(c);
            MessageBox.Show(c.Lname +" is inserted");

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
            allUser.ItemsSource = dao.GetAllUsers();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string x = this.txtDeleteItem.Text;
            int x2 = int.Parse(x);
            dao.DeleteUserById(x2);
            MessageBox.Show(x +" is deleted");
        }
    }
}