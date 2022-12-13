using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Models.SyntaxNodes
{
    public class BlockNode : StatementNode
    {
        public SequenceNode Body;
        public BlockNode() { }

        public BlockNode(SequenceNode body)
        {
            Body = body;
        }

        public override string ToPacalProgram()
        {
            return $"BEGIN\n{Body.ToPacalProgram()}END";
        }
    }

}
