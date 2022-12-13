using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Models.SyntaxNodes
{
	public class IdentifierNode : ExpressionNode
	{
		public string Type;

		public IdentifierNode() { }
		public IdentifierNode(string id, string type) : base(id)
		{
			Type = type;
		}

        public override string Maximize()
        {
			return Type;
        }

        public override string ToPacalProgram()
        {
			return base.ToPacalProgram();
        }
    }
}
