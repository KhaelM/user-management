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
    public partial class Template : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string activepage = Request.RawUrl;
            if (activepage.Contains("/Default.aspx") || activepage.Equals("/"))
            {
                homepage.Attributes["class"] += " active";
            }
            else if (activepage.Contains("/Account/Profile.aspx"))
            {
                profile.Attributes["class"] += " active";
            }
            else if (activepage.Contains("/Data/"))
            {
                data.Attributes["class"] += " active";
            }
            else if (activepage.Contains("/Parameter/"))
            {
                sysparam.Attributes["class"] += " active";
            }


            if (Session["UserId"] != null)
            {
                ServiceUser serviceUser = new ServiceUser();
                User user = serviceUser.GetUser(Session["UserId"].ToString());
                Profil.Text = user.Name;
                Profil.Visible = true;
                Connexion.NavigateUrl = "Account/Signout.aspx";
                Connexion.Text = "Deconnexion";
                user.LoadProfile();

                if(user.IsAdmin())
                {
                    sysparam.Visible = true;
                }
            }
            else
            {
                Connexion.NavigateUrl = "Account/Login.aspx";
                Connexion.Text = "Connexion";
            }
        }
    }
}