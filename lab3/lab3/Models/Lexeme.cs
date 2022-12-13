using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Models
{
    public class Lexeme : BaseModel
    {
        public enum LexemeType
        {
            ParsingError = -1,
            DataType = 0,
            Variable = 1,
            Keysymbol = 2,
            Identifier = 3,
            Constant = 4,
            Operator = 5,
            Keyword = 6,
        }

        public LexemeType Type;
        public int? Id;
        public string? Value;


        public Lexeme() {}
        public Lexeme(LexemeType type, int id, string value, Dictionary<string, string>? attributes = null)
        {
            Id = id;
            Type = type;
            Value = value;
            if (attributes != null)
                Attributes = attributes;
        }

        public override string ToString()
        {
            return base.ToJSON<Lexeme>();
        }
    }
}
