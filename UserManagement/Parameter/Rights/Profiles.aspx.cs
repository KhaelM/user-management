using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserManagement.Model;
using UserManagement.Model.Service;

namespace UserManagement.Parameter
{
    public partial class Profiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageProtection pageProtection = new PageProtection();
            ServiceAuthorization serviceAuthorization = new ServiceAuthorization();
            Authorization[] authorizations = serviceAuthorization.GetAllAuthorizations();
            List<Checkbox> checkBoxes = new List<Checkbox>();
            ListView1.DataSource = checkBoxes;

            foreach (Authorization authorization in authorizations)
            {
                checkBoxes.Add(new Checkbox(authorization.Name, "rights", "", authorization.Name, authorization.Id.ToString()));
            }

            if (Session["UserId"] != null)
            {
                ServiceUser serviceUser = new ServiceUser();
                User user = serviceUser.GetUser(Session["UserId"].ToString());

                if (!user.IsAdmin())
                {
                    pageProtection.NotAdmin(hiddenContent, content, message);
                }

                if (IsPostBack)
                {
                    if (Request.Form[profiles.UniqueID] != null)
                        profiles.Value = Request.Form[profiles.UniqueID].ToString();

                    if (Request.Form[tables.UniqueID] != null)
                        tables.Value = Request.Form[tables.UniqueID].ToString();

                    if (Request.Form["rights"] != null)
                    {
                        foreach (Authorization authorization in authorizations)
                        {
                            if (Request.Form["rights"].Contains(authorization.Id.ToString()))
                            {
                                foreach (Checkbox checkbox in checkBoxes)
                                {
                                    if (authorization.Id.ToString().Equals(checkbox.Value))
                                    {
                                        checkbox.Checked = "checked";
                                    }
                                }
                            }
                        }
                    }

                    try
                    {
                        user.ModifyProfilsRightsOnDataTable(Request.Form[profiles.UniqueID], Request.Form[tables.UniqueID], Request.Form["rights"]);
                    }
                    catch (Exception exc)
                    {
                        exception.Visible = true;
                        exception.InnerText = exc.Message;
                        throw;
                    } finally
                    {
                    }
                }

                ListView1.DataBind();

            }
            else
            {
                pageProtection.NotConnected(Session, Server, Request, Response);
            }
        }
    }
}