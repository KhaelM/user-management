using Michael.Database;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace UserManagement.Model.Service
{
    public class ServiceCalculatedElementUser : Service
    {
        public CalculatedElementUser[] GetAllCalculatedElementUsers()
        {
            DbConnection dbConnection = GetDbConnection();
            dbConnection.Open();
            CalculatedElementUser[] results = null;

            try
            {
                object[] objects = Reflection.Select(dbConnection, "CalculatedElementUser", "UserManagement.Model.CalculatedElementUser", null, null, null);

                if (objects.Length != 0)
                {
                    results = new CalculatedElementUser[objects.Length];
                    for (int i = 0; i < results.Length; i++)
                    {
                        results[i] = (CalculatedElementUser)objects[i];
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                dbConnection.Close();
            }

            return results;
        }
    }
}