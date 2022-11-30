using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Models.SyntaxNodes
{
    public class ExpressionNode : StatementNode
    {
        public string Operation;

        public ExpressionNode() { }
        
        public ExpressionNode(string operation)
        {
            Operation = operation;
        }
    }
}
