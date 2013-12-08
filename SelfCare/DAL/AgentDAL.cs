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
    public class AgentDAL
    {
        public SelfCareContext con { set; get; }

        public AgentDAL() 
        {
            con = new SelfCareContext();
            CheckDataBase();
        }

        private void CheckDataBase()
        {

            if (!con.DatabaseExists())
                con.CreateDatabase();
            
        }


        public void DeleteAgentById(int id)
        {
            Agent myagent = new Agent();
            myagent = (from x in con.Agents where x.Id == id select x).First();
            con.Agents.DeleteOnSubmit(myagent);
            con.SubmitChanges();
        }


        public void InsertAgent(Agent Agent)
        {

            con.Agents.InsertOnSubmit(Agent);
            con.SubmitChanges();

        }


        public IList<Agent> GetAllAgents()
        {

            List<Agent> agents = new List<Agent>();

            var query = from e in this.con.Agents
                            select e;
                agents = query.ToList();
          


            return agents;
        }
    }
}
