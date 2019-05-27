using System;
using System.Collections.Generic;
using System.Linq;
using Michael.Database;
using System.Web;
using System.Data.Common;

namespace UserManagement.Model
{
    public class ServiceUser : Service.Service
    {
        public User[] GetAllUsers()
        {
            DbConnection connection = GetDbConnection();
            User[] users = null;

            try
            {
                connection.Open();
                object[] objects = Reflection.Select(connection, "UserCompleteInfo", "UserManagement.Model.User", null, null, null);
                if (objects.Length != 0)
                {
                    users = new User[objects.Length];
                    for (int i = 0; i < users.Length; i++)
                    {
                        users[i] = (User)objects[i];
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }

            return users;
        }

        public User GetUser(string name, string password)
        {
            DbConnection connection = GetDbConnection();
            connection.Open();
            string[] attributes = { "Name", "Password" };
            string[] values = { name, password };
            string[] operators = { "=", "=" };
            object[] objects = Reflection.Select(connection, "User", "UserManagement.Model.User", attributes, values, operators);
            User user = null;

            if(objects.Length != 0)
            {
                user = (User)objects[0];
            }
            connection.Close();

            return user;
        }

        public User GetUser(string id)
        {
            DbConnection connection = GetDbConnection();
            string[] attributes = { "Id" };
            string[] values = { id };
            string[] operators = { "=" };
            User user = null;

            try
            {
                connection.Open();
                object[] objects = Reflection.Select(connection, "UserCompleteInfo", "UserManagement.Model.User", attributes, values, operators);

                if (objects.Length != 0)
                {
                    user = (User)objects[0];
                }
            }
            catch (Exception)
            {
                throw;
            } finally
            {
                connection.Close();
            }

            return user;
        }
    }
}