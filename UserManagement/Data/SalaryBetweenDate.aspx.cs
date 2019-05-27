using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserManagement.Model;

namespace UserManagement.Data
{
    public partial class SalaryBetweenDate : System.Web.UI.Page
    {
        public User[] otherAccessibleUsers = null;
        public User[] allUsers = null;
        public SalaryRising[] allSalaryRisings = null;
        public Model.SalaryBetweenDate[] display = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            PageProtection pageProtection = new PageProtection();

            if (Session["UserId"] != null)
            {
                
            }
            else
            {
                pageProtection.NotConnected(Session, Server, Request, Response);
            }
        }
 
        protected void Unnamed_Click(object sender, EventArgs e)
        {
            allUsers = (User[])Session["allUsers"];
            allSalaryRisings = (SalaryRising[])Session["allSalaryRisings"];
            CalculatedElementUser[] allElements = (CalculatedElementUser[])Session["allCalculatedElementUser"];
            User user = (User)Session["user"];
            otherAccessibleUsers = user.GetOtherUsersInformation(allUsers);
        }
    }
}