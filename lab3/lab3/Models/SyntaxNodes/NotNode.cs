using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Models.SyntaxNodes
{
	public class NotNode : LogicalNode
	{
		public NotNode() { }
		public NotNode(string token, ExpressionNode expression1)
			: base(token, expression1, expression1)
		{
		}
	}
}
