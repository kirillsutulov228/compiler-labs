using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Models.SyntaxNodes
{
    public class DoWhileNode : StatementNode
    {
        public StatementNode Body;
        public ExpressionNode Condition;

        public DoWhileNode() { }
        public DoWhileNode(StatementNode body, ExpressionNode condition)
        {
            this.Body = body;
            this.Condition = condition;

            string type = condition.Maximize();
            if (!IsNumeric(type))
            {
                throw new Exception($"Cannot convert type {type} to bool");
            }
        }

        public override string ToPacalProgram()
        {
            return $"REPEAT\n{Body.ToPacalProgram()}\nUNTIL {Condition.ToPacalProgram()};\n";
        }
    }
}
