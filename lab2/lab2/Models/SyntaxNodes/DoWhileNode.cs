using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Models.SyntaxNodes
{
    public class DoWhileNode : StatementNode
    {
        public StatementNode body;
        public ExpressionNode condition;

        public DoWhileNode() { }
        public DoWhileNode(StatementNode body, ExpressionNode condition)
        {
            this.body = body;
            this.condition = condition;
        }
    }
}
