using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserManagement.Model;

namespace UserManagement.Data
{
    public partial class Salary : System.Web.UI.Page
    {
        public Paging Pagination { get; set; }
        public int numberOfData = 4;

        protected void Page_Load(object sender, EventArgs e)
        {
            PageProtection pageProtection = new PageProtection();
            if (Session["UserId"] != null)
            {
                int currentPage = Request.QueryString["page"] != null ? Int32.Parse(Request.QueryString["page"].ToString()) : 1;
                next.Attributes["href"] = "Employee.aspx?page=" + (currentPage + 1).ToString();
                prev.Attributes["href"] = "Employee.aspx?page=" + (currentPage - 1).ToString();

                User[] allUsers = (User[])Session["allUsers"];
                User user = (User) Session["user"];
                IQueryable<User> users = GetOtherUsers();
                ListView1.DataSource = users;
                ListView1.DataBind();

                int pagesAvailable = user.GetOtherUsersInformation(allUsers).Count() % numberOfData == 0 ? user.GetOtherUsersInformation(allUsers).Count() / numberOfData : (user.GetOtherUsersInformation(allUsers).Count() / numberOfData) + 1;

                if (currentPage == 1)
                    prevContainer.Attributes["class"] += " disabled";
                if (currentPage == pagesAvailable)
                    nextContainer.Attributes["class"] += " disabled";

                int maxPagination = 5;
                Pagination = new Paging(maxPagination, pagesAvailable, currentPage);
                Pagination.ManagePagination();
            }
            else
            {
                pageProtection.NotConnected(Session, Server, Request, Response);
            }
        }

        public IQueryable<User> GetOtherUsers()
        {
            IQueryable<User> otherUsers = null;
            User[] allUsers = (User[])Session["allUsers"];
            User user = (User)Session["user"];
            otherUsers = user.GetOtherUsersInformation(allUsers).AsQueryable();
            int page = 1;

            if (Request.QueryString["page"] != null)
            {
                page = Int32.Parse(Request.QueryString["page"].ToString());
            }

            int beginningIndex = (page * numberOfData) - numberOfData;
            int endIndex = beginningIndex + numberOfData;
            otherUsers = otherUsers.Skip(beginningIndex).Take(numberOfData);

            return otherUsers;
        }
    }
}