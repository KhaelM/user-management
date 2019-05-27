using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using UserManagement.Interface;

namespace UserManagement.Model
{
    public class CalculatedElementUser : IValidable
    {
        public CalculatedElementUser(string indemnity, bool sign = true)
        {
            Indemnity = indemnity;
        }

        public CalculatedElementUser(int id, string indemnity, int indemnityType, string formula, bool sign, string users, int status, DateTime beginningDate, DateTime? endingDate, string months, string years)
        {
            Id = id;
            Indemnity = indemnity;
            IndemnityType = indemnityType;
            Formula = formula;
            Sign = sign;
            Users = users;
            Status = status;
            BeginningDate = beginningDate;
            EndingDate = endingDate;
            Months = months;
            Years = years;
        }

        public decimal Value { get; set; }
        public int Id { get; }
        public string Indemnity { get; set; }
        public int IndemnityType { get; set; }
        public string Formula { get; set; }
        public bool Sign { get; set; }
        public string Users { get; set; }
        public int Status { get; set; }
        public DateTime BeginningDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public string Months { get; set; }
        public string Years { get; set; }
        public string[] UserList
        {
            get
            {
                return Users.Split(';');
            }
            set
            {

            }
        }
        public string[] YearsSplittedBySemiColon
        {
            get
            {
                char separator = ';';
                return Years.Split(separator);
            }
            set
            {

            }
        }
        public List<int[]> YearIntervals
        {
            get
            {
                string pattern = @"\d+-\d+";
                char separator = '-';
                MatchCollection matches = Regex.Matches(Years, pattern);
                string[] splitted = null;
                List<int[]> results = new List<int[]>();
                int[] buffer = null;
                for (int i = 0; i < matches.Count; i++)
                {
                    splitted = matches[i].Value.Split(separator);
                    buffer = new int[2];
                    buffer[0] = int.Parse(splitted[0]);
                    buffer[1] = int.Parse(splitted[1]);
                    results.Add(buffer);
                }
                return results;
            }
            set
            {

            }
        }
        public List<int[]> MonthIntervals
        {
            get
            {
                string pattern = @"\d{1,2}-\d{1,2}";
                char separator = '-';
                MatchCollection matches = Regex.Matches(Months, pattern);
                string[] splitted = null;
                List<int[]> results = new List<int[]>();
                int[] buffer = null;
                for (int i = 0; i < matches.Count; i++)
                {
                    splitted = matches[i].Value.Split(separator);
                    buffer = new int[2];
                    buffer[0] = int.Parse(splitted[0]);
                    buffer[1] = int.Parse(splitted[1]);
                    results.Add(buffer);
                }
                return results;
            }
            set { }
        }
        public string[] MonthsSplittedBySemiColon
        {
            get
            {
                char separator = ';';
                return Months.Split(separator);
            }
            set { }
        }

        public bool IsValid()
        {
            return Status >= 10;
        }
    }
}