using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManagement.Model
{
    public class UserAuthorizationOnTable
    {
        public UserAuthorizationOnTable(int id, int user, int authorization, int dataTable)
        {
            Id = id;
            User = user;
            Authorization = authorization;
            DataTable = dataTable;
        }

        public int Id { get; }
        public int User { get; set; }
        public int Authorization { get; set; }
        public int DataTable { get; set; }
    }
}