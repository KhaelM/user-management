using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;

namespace UserManagement.Model
{
    public class PageProtection
    {
        public void RedirectToLogin(HttpSessionState httpSession, HttpServerUtility server, HttpRequest request, HttpResponse response, string message)
        {
            httpSession["errorMsg"] = message;
            string currentEncodedUrl = server.UrlEncode(request.RawUrl);
            response.Redirect("~/Account/Login.aspx?ReturnUrl=" + currentEncodedUrl);
        }

        public void NotConnected(HttpSessionState httpSession, HttpServerUtility server, HttpRequest request, HttpResponse response)
        {
            string message = "Vous devez vous connecter pour accéder à cettte page.";
            RedirectToLogin(httpSession, server, request, response, message);
        }

        public void NotAdmin(HtmlGenericControl contentToShow, HtmlGenericControl contentToHide, HtmlGenericControl message)
        {
            contentToHide.Visible = false;
            contentToShow.Visible = true;
            message.InnerText = "Vous devez être administrateur pour accéder à cette page";
        }
    }
}