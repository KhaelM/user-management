using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace UserManagement.Model
{
    public class SalaryBetweenDate
    {
        public SalaryBetweenDate(DateTime date, decimal salary, string href)
        {
            Date = date;
            Salary = salary;
            Href = href;
        }

        public string SalaryFormatted
        {
            get
            {
                NumberFormatInfo numberFormatInfo = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
                numberFormatInfo.NumberGroupSeparator = " ";
                numberFormatInfo.NumberDecimalSeparator = ",";
                numberFormatInfo.NumberDecimalDigits = 2;
                return Salary.ToString("n", numberFormatInfo);
            }
            set
            {

            }
        }
        public DateTime Date { get; set; }
        public decimal Salary { get; set; }
        public string Href { get; set; }
        public string Month
        {
            get
            {
                return CultureInfo.GetCultureInfo("fr-FR").DateTimeFormat.GetMonthName(Date.Month);
            }
        }
        public List<CalculatedElementUser> elementsApplicated { get; set; }
        public decimal CalculatedSalary
        {
            get
            {
                decimal result = Salary;

                return result;
            }
            set
            {

            }
        }
    }
}