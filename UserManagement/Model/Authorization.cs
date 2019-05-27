using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManagement.Model
{
    public class Authorization
    {
        public int Id { get;}
        public string Name { get; set; }

        public Authorization(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}