using Michael.Database;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace UserManagement.Model.Service
{
    public class ServiceAuthorization : Service
    {
        public Authorization GetAuthorization(string id)
        {
            DbConnection connection = GetDbConnection();
            connection.Open();
            string[] attributes = { "Id" };
            string[] values = { id };
            string[] operators = { "=" };
            object[] objects = Reflection.Select(connection, "Authorization", "UserManagement.Model.Authorization", attributes, values, operators);
            Authorization authorization = null;

            if (objects.Length != 0)
            {
                authorization = (Authorization)objects[0];
            }
            connection.Close();

            return authorization;
        }

        public Authorization[] GetAllAuthorizations()
        {
            DbConnection connection = GetDbConnection();
            connection.Open();
            object[] objects = Reflection.Select(connection, "Authorization", "UserManagement.Model.Authorization", null, null, null);
            Authorization[] authorizations = null;

            if (objects.Length != 0)
            {
                authorizations = new Authorization[objects.Length];
                for (int i = 0; i < objects.Length; i++)
                {
                    authorizations[i] = (Authorization)objects[i];
                }
            }

            connection.Close();

            return authorizations;
        }
    }
}