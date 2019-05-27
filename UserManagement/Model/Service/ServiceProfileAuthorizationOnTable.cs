using Michael.Database;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace UserManagement.Model
{
    public class ServiceProfileAuthorizationOnTable : Service.Service
    {
        public ProfileAuthorizationOnTable GetProfileAuthorizationOnTable(string profileId, string authorizationId, string tableId)
        {
            DbConnection connection = GetDbConnection();
            connection.Open();
            string[] attributes = { "Profile", "Authorization", "DataTable" };
            string[] values = { profileId, authorizationId, tableId };
            string[] operators = { "=", "=", "=" };
            object[] objects = Reflection.Select(connection, "ProfileAuthorizationOnTable", "UserManagement.Model.ProfileAuthorizationOnTable", attributes, values, operators);
            ProfileAuthorizationOnTable profileAuthorizationOnTable = null;

            if (objects.Length != 0)
            {
                profileAuthorizationOnTable = (ProfileAuthorizationOnTable)objects[0];
            }
            connection.Close();

            return profileAuthorizationOnTable;
        }

        public void InsertProfileAuthorizationOnTable(ProfileAuthorizationOnTable profileAuthorizationOnTable)
        {
            DbConnection connection = GetDbConnection();
            connection.Open();
            try
            {
                Reflection.Insert(connection, "ProfileAuthorizationOnTable", profileAuthorizationOnTable);
            }
            catch (Exception)
            {

                throw;
            } finally
            {
                connection.Close();
            }
        }
    }
}