using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManagement.Model
{
    public class Checkbox
    {
        public Checkbox(string id, string name, string @checked, string label, string value)
        {
            Id = id;
            Name = name;
            Checked = @checked;
            Label = label;
            Value = value;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Checked { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
    }
}