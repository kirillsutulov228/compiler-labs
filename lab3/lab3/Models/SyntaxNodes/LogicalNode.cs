using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Models.SyntaxNodes
{
	public class LogicalNode : ExpressionNode
	{
		public ExpressionNode Expression1, Expression2;

		public LogicalNode() { }
		public LogicalNode(string token, ExpressionNode expression1, ExpressionNode expression2)
			: base(token)
		{
			Expression1 = expression1;
			Expression2 = expression2;
		}
	}
}
