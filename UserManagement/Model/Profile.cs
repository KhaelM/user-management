using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserManagement.Interface;

namespace UserManagement.Model
{
    public class Profile : IValidable
    {
        public string Id { get; }
        public string Name { get; set; }
        public int Status { get; set; }

        public Profile(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public bool IsValid()
        {
            return Status > 1;
        }
    }
}