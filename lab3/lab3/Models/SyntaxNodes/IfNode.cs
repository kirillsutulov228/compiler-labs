using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Models.SyntaxNodes
{
    public class IfNode : StatementNode
    {
        public ExpressionNode Condition;
        public StatementNode Then;
        public StatementNode? Else;
        public IfNode() { }
        public IfNode(ExpressionNode condition, StatementNode then, StatementNode? els = null)
        {
            this.Condition = condition;
            this.Then = then;
            this.Else = els;
            string type = Condition.Maximize();
            if (!IsNumeric(type))
            {
                throw new Exception($"Cannot convert type {type} to bool");
            }
        }

        public override string ToPacalProgram()
        {
            return $"IF {Condition.ToPacalProgram()} THEN\n" + Then.ToPacalProgram() + (Else != null ? $"\nELSE {Else.ToPacalProgram()}\n" : "\n");
        }
    }
}
