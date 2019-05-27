using Michael.Database;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace UserManagement.Model.Service
{
    public class ServiceIndemnityType : Service
    {
        public IndemnityType GetIndemnityType(string id)
        {
            DbConnection dbConnection = GetDbConnection();
            dbConnection.Open();
            IndemnityType result = null;
            string[] attributes = { "Id" };
            string[] values = { id };
            string[] operators = { "=" };

            try
            {
                object[] objects = Reflection.Select(dbConnection, "IndemnityType", "UserManagement.Model.IndemnityType", attributes, values, operators);

                if (objects.Length != 0)
                {
                    result = (IndemnityType)objects[0];
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

            return result;
        }

        public IndemnityType[] GetAllIndemnityTypes()
        {
            DbConnection dbConnection = GetDbConnection();
            dbConnection.Open();
            IndemnityType[] results = null;

            try
            {
                object[] objects = Reflection.Select(dbConnection, "IndemnityType", "UserManagement.Model.IndemnityType", null, null, null);

                if (objects.Length != 0)
                {
                    results = new IndemnityType[objects.Length];
                    for (int i = 0; i < results.Length; i++)
                    {
                        results[i] = (IndemnityType)objects[i];
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