using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace lab3.Models.SyntaxNodes
{
	public class UnaryNode : ExpressionNode
	{
		public ExpressionNode Expression;

		public UnaryNode() { }
		public UnaryNode(string op, ExpressionNode expression) : base(op)
		{
			Expression = expression;
			Maximize();
		}

        public override string Maximize()
        {
			string type = Expression.Maximize();
			if ((Operation == "-" || Operation == "!") && type == "string" )
            {
				throw new Exception($"Cannot use operation {Operation} with type string");
            }
			if (Operation == "!")
            {
				return "bool";
            }
			return type;
        }

        public override string ToPacalProgram()
        {
			return base.Operation + Expression.ToPacalProgram();
        }
    }
}
