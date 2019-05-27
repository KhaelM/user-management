using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserManagement.Model;

namespace UserManagement.Account
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                ServiceUser serviceUser = new ServiceUser();
                DbConnection connection = serviceUser.GetDbConnection();
                connection.Open();
                User user = serviceUser.GetUser(Session["UserId"].ToString());
                Username.Text = user.Name;
                UserId.Text = user.Id.ToString();
                Password.Text = user.Password;
                try
                {
                    user.LoadProfile();
                    Profil.Text = user.Profile.Name;
                }
                catch(Exception exception)
                {
                    Profil.Text = exception.Message;
                    Profil.CssClass += " text-danger";
                }

                Salary.Text = user.Salary.ToString();
                connection.Close();
            }
            else
            {
                string errorMsg = "Vous devez vous connecter pour accéder à cettte page.";
                Session["errorMsg"] = errorMsg;
                string returnUrl = Server.UrlEncode(Request.RawUrl);
                Response.Redirect("Login.aspx?ReturnUrl="+returnUrl);
            }
        }
    }
}