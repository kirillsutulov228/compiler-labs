using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Models.SyntaxNodes
{
    public class IncrementNode : ExpressionNode
    {
        public string Identifier;
        public IncrementNode() { }
        public IncrementNode(string op, string id) : base(op)
        {
            Identifier = id;
        }
    }
}
