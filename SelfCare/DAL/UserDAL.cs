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
    public class UserDAL
    {

       public SelfCareContext con { set; get; }

          public UserDAL() 
        {
            con = new SelfCareContext();
            CheckDataBase();
        }

        private void CheckDataBase()
        {

            if (!con.DatabaseExists())
                con.CreateDatabase();
            
        }


        public void DeleteUserById(int id)
        {
            User myUser = new User();
            myUser = (from x in con.Users where x.Id == id select x).First();
            con.Users.DeleteOnSubmit(myUser);
            con.SubmitChanges();
        }


        public void InsertUser(User User)
        {


     
            con.Users.InsertOnSubmit(User);
            con.SubmitChanges();

        }


        public IList<User> GetAllUsers()
        {

            List<User> Users = new List<User>();

            var query = from e in this.con.Users
                        select e;
            Users = query.ToList();



            return Users;
        }


    }
}
