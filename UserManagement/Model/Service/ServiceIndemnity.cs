using Michael.Database;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace UserManagement.Model.Service
{
    public class ServiceIndemnity : Service
    {
        public Indemnity[] GetAllIndemnities()
        {
            DbConnection connection = GetDbConnection();
            connection.Open();
            object[] objects = Reflection.Select(connection, "Indemnity", "UserManagement.Model.Indemnity", null, null, null);
            Indemnity[] results = null;

            if (objects.Length != 0)
            {
                results = new Indemnity[objects.Length];
                for (int i = 0; i < results.Length; i++)
                {
                    results[i] = (Indemnity)objects[i];
                }
            }
            connection.Close();

            return results;
        }

        public Indemnity GetIndemnity(string name)
        {
            DbConnection connection = GetDbConnection();
            connection.Open();
            string[] attributes = { "Name" };
            string[] values = { name };
            string[] operators = { "=" };
            object[] objects = Reflection.Select(connection, "Authorization", "UserManagement.Model.Authorization", attributes, values, operators);
            Indemnity result = null;

            if (objects.Length != 0)
            {
                result = (Indemnity)objects[0];
            }
            connection.Close();

            return result;
        }
    }
}