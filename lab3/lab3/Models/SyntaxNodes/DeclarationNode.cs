using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Models.SyntaxNodes
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

        public override string ToPacalProgram()
        {
            string pascalType;
            switch (Type)
            {
                case "bool": 
                    pascalType = "BOOLEAN";
                    break;
                case "string": 
                    pascalType = "STRING";
                    break;
                case "float": 
                    pascalType = "REAL";
                    break;
                case "int": 
                    pascalType = "INTEGER";
                    break;
                case "char": 
                    pascalType = "CHAR";
                    break;
                default: 
                    throw new Exception($"type {Type} not implemented");
            }
            return $"VAR {Identificator}: {pascalType};";
        }
    }
}
