using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Models.SyntaxNodes
{
    public class IncrementNode : ExpressionNode
    {
        public string Identifier;
        public string Type;
        public IncrementNode() { }
        public IncrementNode(string op, string id, string type) : base(op)
        {
            Identifier = id;
            Type = type;
            if (!IsNumeric(type))
            {
                throw new Exception("Type error: cannot increment non-numeric value");
            }
        }

        public override string ToPacalProgram()
        {
            return Operation == "++" 
                ? $"INC({Identifier});\n" 
                : $"DEC({Identifier});\n"
            ;
        }
    }
}
