using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManagement.Model
{
    public class Indemnity
    {
        public Indemnity(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; set; }
    }
}