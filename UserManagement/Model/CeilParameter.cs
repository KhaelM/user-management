using Michael.Database;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using UserManagement.Model.Service;

namespace UserManagement.Model
{
    public class CeilParameter
    {
        public void Activate()
        {
            ServiceCeilParameter serviceCeilParameter = new ServiceCeilParameter();
            CeilParameter[] allCeilParameters = serviceCeilParameter.GetAllCeilParameters();
            DbConnection connection = serviceCeilParameter.GetDbConnection();
            connection.Open();

            try
            {
                foreach (CeilParameter parameter in allCeilParameters)
                {
                    if (parameter.Id != Id)
                        parameter.Status = false;
                    else
                        parameter.Status = true;

                    Reflection.Update(connection, "CeilParameter", parameter);
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
            
        }

        public CeilParameter(int id, string name, int distanceToLastInteger, bool status)
        {
            Id = id;
            Name = name;
            DistanceToLastInteger = distanceToLastInteger;
            Status = status;
        }

        public int Id { get; }
        public string Name { get; set; }
        public int DistanceToLastInteger { get; set; }
        public bool Status { get; set; }
    }
}