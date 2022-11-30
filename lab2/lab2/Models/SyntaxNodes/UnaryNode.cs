using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace lab2.Models.SyntaxNodes
{
	public class UnaryNode : ExpressionNode
	{
		public ExpressionNode Expression;

		public UnaryNode() { }
		public UnaryNode(string op, ExpressionNode expression) : base(op)
		{
			Expression = expression;
		}
    }
}
