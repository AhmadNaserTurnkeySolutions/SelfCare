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
using System.Data.Linq;
using System.Data.Linq.Mapping;
namespace SelfCare
{
    public partial class LocalDatabase : PhoneApplicationPage
    {
        const string ConnectionString = "Data Source=isostore:/EmailDB.sdf";

        public LocalDatabase()
        {
            InitializeComponent();
          

            InputScope _scope = new InputScope();
            InputScopeName _scopeName = new InputScopeName();
            _scopeName.NameValue = InputScopeNameValue.EmailNameOrAddress;
            _scope.Names.Add(_scopeName);

            txtEmail.InputScope = _scope;

            using (var db = new EmailContext(ConnectionString))
            {
                if (!db.DatabaseExists())
                    db.CreateDatabase();

                LoadData();
            }
        }



        public void LoadData()
        {
            DataContext = GetEmails(); 
           // lstEmails.ItemsSource = GetEmails();
        }

        public IList<EmailClass> GetEmails()
        {
            List<EmailClass> emails = new List<EmailClass>();
            using (var db = new EmailContext(ConnectionString))
            {
                var query = from e in db.Emails
                            select e;
                emails = query.ToList();
            }

            return emails;
        }

        public void AddEmail(EmailClass _email)
        {
            using (var db = new EmailContext(ConnectionString))
            {
                db.Emails.InsertOnSubmit(_email);
                db.SubmitChanges();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtEmail.Text.Trim() != "")
            {
                var emailObj = new EmailClass() { EmailAddress = txtEmail.Text.Trim() };
                AddEmail(emailObj);
            }
            LoadData();
            txtEmail.Text = "";
        }
    }

    //Model Class
    [Table]
    public class EmailClass
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }
        [Column()]
        public string EmailAddress { get; set; }
    }

    public class EmailContext : DataContext
    {
        public EmailContext(string sConnectionString)
            : base(sConnectionString)
        { }

        public Table<EmailClass> Emails
        {
            get
            {
                return this.GetTable<EmailClass>();
            }
        }

    }



}