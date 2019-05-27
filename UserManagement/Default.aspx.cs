using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserManagement.Model;

namespace UserManagement
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["UserId"] != null)
            {
                ServiceUser serviceUser = new ServiceUser();
                DbConnection connection = serviceUser.GetDbConnection();
                connection.Open();
                User user = serviceUser.GetUser(Session["UserId"].ToString());
                Username.Text = user.Name;
                connection.Close();
            }

        }
    }
}