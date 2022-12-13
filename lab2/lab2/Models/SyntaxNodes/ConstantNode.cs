using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Models.SyntaxNodes
{
    public class ConstantNode : ExpressionNode
    {
        public string type;
        public ConstantNode() { }
        public ConstantNode(string token, string dataType) : base(token)
        {
            type = dataType;
        }
    }
}
