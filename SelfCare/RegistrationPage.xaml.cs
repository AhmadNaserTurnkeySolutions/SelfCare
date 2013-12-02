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
using SelfCare.Entities;
using System.Windows.Data;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Phone.Shell;
namespace SelfCare
{
    public partial class RegistrationPage : PhoneApplicationPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            
           

            this.agentTextbox.Focus();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {

        string UserName =this.agentTextbox.Text;
        string Password =this.passwordTextbox.Text;
        string Agency="";
     
        string Proficiency = "";
        string RecordCreatedDateTime = DateTime.Now.ToString();

        bool? IsUndercover = undercoverCheckBox.IsChecked;
           
           
            if (ciaRadioButton.IsChecked == true)
            {
                Agency = "CIA";
            }
            else if(mi6RadioButton.IsChecked==true)
            {
                Agency = "MI6";
            }
            else if (nsaRadioButton.IsChecked == true)
            {
                Agency = "NSA";
            }
            else 
            {
                Agency = "NONE";
            }

           
            ListBoxItem lbi = (ListBoxItem)proficienciesListBox.SelectedItem;
             Proficiency = (string)lbi.Content;

             Agent MyAgent = new Agent();

             MyAgent.Agency = Agency;
             MyAgent.Password = Password;
             MyAgent.Proficiency = Proficiency;
             MyAgent.RecordCreatedDateTime = RecordCreatedDateTime;
             MyAgent.UserName = UserName;
             MyAgent.IsUndercover = IsUndercover;

            // var Test = MyAgent;


            //way 1
            /*
             bool? isValid = validate(MyAgent);
             string json = JsonConvert.SerializeObject(MyAgent); 
             NavigationService.Navigate(new Uri(string.Format("/RegistrationSuccessPage.xaml?parameter={0}", json), UriKind.Relative));
            */

             //way 2
             PhoneApplicationService.Current.State["param"] = MyAgent;
             NavigationService.Navigate(new Uri("/RegistrationSuccessPage.xaml", UriKind.Relative));
        
        }

        private bool? validate(Agent MyAgent)
        {
            return true;
        }
    }
}