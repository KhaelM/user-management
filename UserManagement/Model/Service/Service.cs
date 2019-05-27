using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using Michael.Database;

namespace UserManagement.Model.Service
{
    public class Service
    {
        public DbConnection GetDbConnection()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\miker\Dropbox\C#\UserManagement\UserManagement\App_Data\Database.mdf;Integrated Security=True";

            return ConnectionManager.GetMssqlConnection(connectionString);
        }
    }
}