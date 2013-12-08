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
using SelfCare.Entities;
using System.Collections.Generic;
using System.Linq;
namespace SelfCare.DAL
{
    public class ContactDAL
    {
          public SelfCareContext con { set; get; }

          public ContactDAL() 
        {
            con = new SelfCareContext();
            CheckDataBase();
        }

        private void CheckDataBase()
        {

            if (!con.DatabaseExists())
                con.CreateDatabase();
            
        }


        public void DeleteContactById(int id)
        {
            Contact myContact = new Contact();
            myContact = (from x in con.Contacts where x.Id == id select x).First();
            con.Contacts.DeleteOnSubmit(myContact);
            con.SubmitChanges();
        }


        public void InsertContact(Contact Contact)
        {

            con.Contacts.InsertOnSubmit(Contact);
            con.SubmitChanges();

        }


        public IList<Contact> GetAllContacts()
        {

            List<Contact> contacts = new List<Contact>();

            var query = from e in this.con.Contacts
                        select e;
            contacts = query.ToList();



            return contacts;
        }




    }
}
