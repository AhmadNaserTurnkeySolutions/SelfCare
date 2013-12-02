SelfCare
========
//1- download http://json.codeplex.com/ 

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


2-serialize the object into string and send it

             string json = JsonConvert.SerializeObject(MyAgent); 
           
            NavigationService.Navigate(new Uri(string.Format("/RegistrationSuccessPage.xaml?parameter={0}", json), UriKind.Relative));

			
3- 		DeserializeObject back from string to object	
			
			    string parameter = this.NavigationContext.QueryString["parameter"];

    Agent MyAgent = JsonConvert.DeserializeObject<Agent>(parameter);

    string content = String.Format("UserName    : {0}    \n" +
                                   "Password    : {1}   \n" +
                                   "Agency  : {2}   \n" +
                                   "Proficiency : {3}   \n" +
                                   "RecordCreatedDateTime   : {4}   \n" +
                                   "IsUndercover    : {5}   \n",

MyAgent.UserName, MyAgent.Password, MyAgent.Agency, MyAgent.Proficiency, MyAgent.RecordCreatedDateTime, MyAgent.IsUndercover);

    this.textBlock2.Text = content;
	
	
	
