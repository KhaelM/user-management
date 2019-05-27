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
    public partial class CalculatedElements : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageProtection pageProtection = new PageProtection();

            if(Session["UserId"] != null)
            {
                ServiceUser serviceUser = new ServiceUser();
                User user = serviceUser.GetUser(Session["UserId"].ToString());
                ServiceIndemnityType serviceIndemnityType = new ServiceIndemnityType();
                IndemnityType[] indemnityTypes = serviceIndemnityType.GetAllIndemnityTypes();
                Indemnities.DataSource = indemnityTypes.ToList();
                Indemnities.DataBind();


                if(!user.IsAdmin())
                {
                    pageProtection.NotAdmin(hiddenContent, content, message);
                }

                if(IsPostBack)
                {
                    string indemnitiesInput = Request.Form["indemnityInput"];
                    string indemnityTypeInput = Request.Form["IndemnityType"];
                    string formulaInput = Request.Form["formulaInput"];
                    string signInput = Request.Form["sign"];
                    string usersInput = Request.Form["usersInput"];
                    string beginningDateInput = Request.Form["beginningDate"];
                    string endingDateInput = Request.Form["endingDate"];
                    string monthsInput = Request.Form["monthsInput"];
                    string yearsInput = Request.Form["yearsInput"];


                    user.InsertCalculatedElementUser(indemnitiesInput, indemnityTypeInput, formulaInput, signInput, usersInput, beginningDateInput, endingDateInput, monthsInput, yearsInput);
                    
                }
            }
            else
            {
                pageProtection.NotConnected(Session, Server, Request, Response);
            }
        }
    }
}