using Michael.Database;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace UserManagement.Model.Service
{
    public class ServiceProfile : Service
    {
        public Profile GetProfile(string id)
        {
            DbConnection connection = GetDbConnection();
            string[] attributes = { "Id" };
            string[] values = { id };
            string[] operators = { "=" };
            Profile profile = null;

            try
            {
                connection.Open();
                object[] objects = Reflection.Select(connection, "Profile", "UserManagement.Model.Profile", attributes, values, operators);

                if (objects.Length != 0)
                {
                    profile = (Profile)objects[0];
                }
            }
            catch (Exception)
            {
                throw;
            } finally
            {
                connection.Close();
            }

            return profile;
        }

        public Profile[] GetAllProfiles()
        {
            DbConnection connection = GetDbConnection();
            Profile[] profiles = null;

            try
            {
                connection.Open();
                object[] objects = Reflection.Select(connection, "Profile", "UserManagement.Model.Profile", null, null, null);
                if (objects.Length != 0)
                {
                    profiles = new Profile[objects.Length];
                    for (int i = 0; i < profiles.Length; i++)
                    {
                        profiles[i] = (Profile)objects[i];
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

            return profiles;
        }
    }
}