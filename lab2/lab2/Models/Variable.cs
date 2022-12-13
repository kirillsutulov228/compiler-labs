using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Models
{
    public class Variable : BaseModel
    {
        public string? Type;
        public string? Name;
        public string? Id;
        public Variable() {}
        public Variable(string? type, string? name, string? id, Dictionary<string, string>? attributes = null)
        {
            Id = id;
            Type = type;
            Name = name;
            if (attributes != null)
                Attributes = attributes;
        }

        public override string ToString()
        {
            return base.ToJSON<Variable>();
        }
    }
}
