using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Models.SyntaxNodes
{
    public class ArithmeticNode : ExpressionNode
    {
        public ExpressionNode Expression1, Expression2;

        public ArithmeticNode() { }
        public ArithmeticNode(string op, ExpressionNode expression1, ExpressionNode expression2) : base(op)
        {
            Expression1 = expression1;
            Expression2 = expression2;
        }
    }
}
