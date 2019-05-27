using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserManagement.Interface;

namespace UserManagement.Model
{
    public class SalaryRising : IValidable
    {
        public SalaryRising(int? id, int user, decimal salary, int status, DateTime risingDate)
        {
            Id = id;
            User = user;
            Salary = salary;
            Status = status;
            RisingDate = risingDate;
        }

        public int? Id { get; }
        public int User { get; set; }
        public decimal Salary { get; set; }
        public int Status { get; set; }
        public DateTime RisingDate { get; set; }

        public bool IsValid()
        {
            return Status >= 10;
        }
    }
}