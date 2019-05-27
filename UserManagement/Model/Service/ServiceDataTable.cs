using Michael.Database;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace UserManagement.Model.Service
{
    public class ServiceDataTable : Service
    {
        public DataTable GetDataTable(string name)
        {
            DbConnection connection = GetDbConnection();
            string[] attributes = { "Name" };
            string[] values = { name };
            string[] operators = { "=" };
            DataTable table = null;

            try
            {
                connection.Open();
                object[] objects = Reflection.Select(connection, "DataTable", "UserManagement.Model.DataTable", attributes, values, operators);

                if (objects.Length != 0)
                {
                    table = (DataTable)objects[0];
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

            return table;
        }

        public DataTable[] GetAllDataTables()
        {
            DbConnection connection = GetDbConnection();
            DataTable[] tables = null;
            try
            {
                connection.Open();
                object[] objects = Reflection.Select(connection, "DataTable", "UserManagement.Model.DataTable", null, null, null);

                if (objects.Length != 0)
                {
                    tables = new DataTable[objects.Length];
                    for (int i = 0; i < tables.Length; i++)
                    {
                        tables[i] = (DataTable)objects[i];
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

            return tables;
        }
    }
}