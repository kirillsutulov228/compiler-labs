using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Models.SyntaxNodes
{
    public class BracketNode : ExpressionNode
    {
        public ExpressionNode Expression;
        public BracketNode(ExpressionNode expression)
        {
            Expression = expression;
        }

        public BracketNode() { }

        public override string Maximize()
        {
            return Expression.Maximize();
        }
        public override string ToPacalProgram()
        {
            return "(" + Expression.ToPacalProgram() + ")";
        }
    }
}
