using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManagement.Model
{
    public class ProfileAuthorizationOnTable
    {
        public ProfileAuthorizationOnTable(int id, string profile, int dataTable, int authorization)
        {
            Id = id;
            Profile = profile;
            DataTable = dataTable;
            Authorization = authorization;
        }

        public int Id { get; }
        public string Profile { get; set; }
        public int DataTable { get; set; }
        public int Authorization { get; set; }
    }
}