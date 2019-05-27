using Michael.Database;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace UserManagement.Model.Service
{
    public class ServiceUserAuthorizationOnTable : Service
    {
        public void InsertUserAuthorizationOnTable(UserAuthorizationOnTable userAuthorizationOnTable)
        {
            DbConnection connection = GetDbConnection();
            connection.Open();
            try
            {
                Reflection.Insert(connection, "UserAuthorizationOnTable", userAuthorizationOnTable);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public UserAuthorizationOnTable GetUserAuthorizationOnTable(string userId, string authorizationId, string dataTableId)
        {
            DbConnection connection = GetDbConnection();
            connection.Open();
            UserAuthorizationOnTable userAuthorizationOnTable = null;
            string[] attributes = { "User", "Authorization", "DataTable" };
            string[] values = { userId, authorizationId, dataTableId };
            string[] operators = { "=", "=", "=" };

            try
            {
                object[] objects = Reflection.Select(connection, "UserAuthorizationOnTable", "UserManagement.Model.UserAuthorizationOnTable", attributes, values, operators);
                if (objects.Length != 0)
                    userAuthorizationOnTable = (UserAuthorizationOnTable)objects[0];
            }
            catch (Exception)
            {

                throw;
            } finally
            {
                connection.Close();
            }

            return userAuthorizationOnTable;
        }
    }
}