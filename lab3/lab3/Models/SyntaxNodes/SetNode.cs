using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Models.SyntaxNodes
{
    public class SetNode : StatementNode
    {
        public string Name;
        public ExpressionNode Expression;
        public string Type;

        public SetNode() { }
        public SetNode(string name, ExpressionNode expression, string type)
        {
            Name = name;
            Expression = expression;
            Type = type;
            Check();
        }


        public void Check()
        {
            string expressionType = Expression.Maximize();
            if (
                (Type == "bool" && expressionType != "string") || 
                (Type == "string" && expressionType == "string") ||
                (base.IsNumeric(expressionType) && base.IsNumeric(Type)) ||
                (IsNumeric(Type) && expressionType == "bool")
               )
            {
                return;
            }
            throw new Exception($"Type error: cannot cast type \"{expressionType}\" into \"{Type}\"");
        }

        public override string ToPacalProgram()
        {
            return $"{Name} := {Expression.ToPacalProgram()};\n";
        }
    }
}
