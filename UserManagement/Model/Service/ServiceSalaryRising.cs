using Michael.Database;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace UserManagement.Model.Service
{
    public class ServiceSalaryRising : Service
    {
        public SalaryRising[] GetAllSalaryRisings()
        {
            DbConnection connection = GetDbConnection();
            connection.Open();
            object[] objects = Reflection.Select(connection, "SalaryRising", "UserManagement.Model.SalaryRising", null, null, null);
            SalaryRising[] results = null;

            if (objects.Length != 0)
            {
                results = new SalaryRising[objects.Length];
                for (int i = 0; i < objects.Length; i++)
                {
                    results[i] = (SalaryRising)objects[i];
                }
            }

            connection.Close();

            return results;
        }
    }
}