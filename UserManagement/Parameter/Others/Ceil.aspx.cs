using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserManagement.Model;
using UserManagement.Model.Service;

namespace UserManagement.Parameter.Others
{
    public partial class Ceil : System.Web.UI.Page
    {
        public CeilParameter ActiveParameter { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageProtection pageProtection = new PageProtection();
            ServiceCeilParameter serviceCeilParameter = new ServiceCeilParameter();
            List<CeilParameter> parameters = serviceCeilParameter.GetAllCeilParameters().ToList();
            ListView1.DataSource = parameters;

            if (Session["UserId"] != null)
            {
                ServiceUser serviceUser = new ServiceUser();
                User user = serviceUser.GetUser(Session["UserId"].ToString());

                if (!user.IsAdmin())
                {
                    pageProtection.NotAdmin(hiddenContent, content, message);
                }

                if(IsPostBack)
                {
                    if(Request.Form["ceil"] != null)
                    {
                        CeilParameter ceilParameter = serviceCeilParameter.GetCeilParameter(Request.Form["ceil"]);
                        ceilParameter.Activate();
                    }
                    parameters = serviceCeilParameter.GetAllCeilParameters().ToList();
                }
                ActiveParameter = serviceCeilParameter.GetActiveCeilParameter();
                ListView1.DataBind();

            }
            else
            {
                pageProtection.NotConnected(Session, Server, Request, Response);
            }
        }
    }
}