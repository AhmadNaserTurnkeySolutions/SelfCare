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
using SelfCare.Entities;
using SelfCare.DAL;

namespace SelfCare
{
    public partial class ContactCRUD : PhoneApplicationPage
    {

        ContactDAL dao;

        PhotoChooserTask photoChooserTask;
        public ContactCRUD()
        {
            InitializeComponent();

            photoChooserTask = new PhotoChooserTask();
            dao = new ContactDAL();
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
                BitmapImage myImage = new BitmapImage();
                myImage.SetSource(e.ChosenPhoto);

                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = myImage;
                this.imagepanel.Background = imageBrush;

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Contact c = new Contact();
            c.Address = this.txtAddress.Text;
            c.Fname = this.txtFname.Text;
            c.Lname = this.txtLName.Text;
            c.Phone = this.txtPhone.Text;

            dao.InsertContact(c);
            MessageBox.Show(c.Lname +" is inserted");

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            allContacts.ItemsSource = dao.GetAllContacts();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string x = this.txtDeleteItem.Text;
            int x2 = int.Parse(x);
            dao.DeleteContactById(x2);
            MessageBox.Show(x +" is deleted");
        }

    }
}