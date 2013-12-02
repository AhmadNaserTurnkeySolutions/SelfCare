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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.Phone.Shell;
using SelfCare.DAL;
namespace SelfCare
{
    public partial class RegistrationSuccessPage : PhoneApplicationPage
    {
        public Agent AgentReference { set; get; }

        public RegistrationSuccessPage()
        {
            InitializeComponent();
        }

        
 protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
{
    base.OnNavigatedTo(e);
    
    //Way 1
    //string parameter = this.NavigationContext.QueryString["parameter"];
    //Agent MyAgent = JsonConvert.DeserializeObject<Agent>(parameter);


    //Way2
    //Agent MyAgent = PhoneApplicationService.Current.State["param"] as Agent;
 
    //Way3 :: using inObject Reference

    Agent MyAgent = this.AgentReference;
    
    string content = String.Format("UserName    : {0}    \n" +
                                   "Password    : {1}   \n" +
                                   "Agency  : {2}   \n" +
                                   "Proficiency : {3}   \n" +
                                   "RecordCreatedDateTime   : {4}   \n" +
                                   "IsUndercover    : {5}   \n",

MyAgent.UserName, MyAgent.Password, MyAgent.Agency, MyAgent.Proficiency, MyAgent.RecordCreatedDateTime, MyAgent.IsUndercover);

    this.textBlock2.Text = content;

    //if (int.TryParse(parameter, out countryId))
    //{

    //}
 
    //this.DataContext = country;
}

      
    }
}