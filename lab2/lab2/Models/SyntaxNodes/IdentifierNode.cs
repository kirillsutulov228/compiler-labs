using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Models.SyntaxNodes
{
	public class IdentifierNode : ExpressionNode
	{
		public string Type;

		public IdentifierNode() { }
		public IdentifierNode(string id, string type) : base(id)
		{
			Type = type;
		}
	}
}
