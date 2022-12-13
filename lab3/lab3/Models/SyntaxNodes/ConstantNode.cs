using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Models.SyntaxNodes
{
    public class ConstantNode : ExpressionNode
    {
        public string Type;
        public ConstantNode() { }
        public ConstantNode(string token, string dataType) : base(token)
        {
            Type = dataType;
        }

        public override string Maximize() {
            return Type;
        }
    }
}
