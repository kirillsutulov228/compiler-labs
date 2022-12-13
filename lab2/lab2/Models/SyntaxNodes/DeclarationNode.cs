using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Models.SyntaxNodes
{
    public class DeclarationNode : StatementNode
    {
        public string Identificator;
        public string Type;

        public DeclarationNode() { }
        public DeclarationNode(string identificator, string type)
        {
            this.Identificator = identificator;
            this.Type = type;
        }
    }
}
