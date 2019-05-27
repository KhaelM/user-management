using Michael.Database;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace UserManagement.Model.Service
{
    public class ServiceCeilParameter : Service
    {
        public CeilParameter GetCeilParameter(string id)
        {
            DbConnection dbConnection = GetDbConnection();
            dbConnection.Open();
            CeilParameter result = null;
            string[] attributes = { "Id" };
            string[] values = { id };
            string[] operators = { "=" };

            try
            {
                object[] objects = Reflection.Select(dbConnection, "CeilParameter", "UserManagement.Model.CeilParameter", attributes, values, operators);

                if (objects.Length != 0)
                {
                    result = (CeilParameter)objects[0];
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

        public CeilParameter GetActiveCeilParameter()
        {
            DbConnection dbConnection = GetDbConnection();
            dbConnection.Open();
            CeilParameter result = null;
            string[] attributes = { "Status" };
            string[] values = { "True" };
            string[] operators = { "=" };

            try
            {
                object[] objects = Reflection.Select(dbConnection, "CeilParameter", "UserManagement.Model.CeilParameter", attributes, values, operators);

                if (objects.Length != 0)
                {
                    result = (CeilParameter)objects[0];
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

        public CeilParameter[] GetAllCeilParameters()
        {
            DbConnection dbConnection = GetDbConnection();
            dbConnection.Open();
            CeilParameter[] results = null;

            try
            {
                object[] objects = Reflection.Select(dbConnection, "CeilParameter", "UserManagement.Model.CeilParameter", null, null, null);
                
                if(objects.Length != 0)
                {
                    results = new CeilParameter[objects.Length];
                    for (int i = 0; i < results.Length; i++)
                    {
                        results[i] = (CeilParameter)objects[i];
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