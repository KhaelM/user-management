using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserManagement.Model;
using UserManagement.Model.Service;

namespace UserManagement.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                ServiceUser serviceUser = new ServiceUser();
                User user = serviceUser.GetUser(Request.Form["inputUsername"], Request.Form["inputPassword"]);

                if (user != null)
                {
                    ServiceCalculatedElementUser serviceCalculatedElementUser = new ServiceCalculatedElementUser();
                    ServiceSalaryRising serviceSalaryRising = new ServiceSalaryRising();

                    Session["UserId"] = user.Id;
                    Session["allCalculatedElementUser"] = serviceCalculatedElementUser.GetAllCalculatedElementUsers();
                    Session["allUsers"] = serviceUser.GetAllUsers();
                    Session["user"] = serviceUser.GetUser(user.Id.ToString());
                    Session["allSalaryRisings"] = serviceSalaryRising.GetAllSalaryRisings();

                    if(Request.QueryString["ReturnUrl"] != null)
                    {
                        Response.Redirect(Server.UrlDecode(Request.QueryString["ReturnUrl"].ToString()));
                    }
                    else
                    {
                        Response.Redirect("../Default.aspx");
                    }
                }
                else
                {
                    Session["errorMsg"] = "Nom d'utilisateur/Mot de passe incorrect.";
                }
            }
            
            if(Session["errorMsg"] != null)
            {
                Message.CssClass = "text-danger";
                Message.Text = Session["errorMsg"].ToString();
            }
        }
    }
}