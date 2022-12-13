using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Models.SyntaxNodes
{
    public class BinaryNode : ExpressionNode
    {
        public ExpressionNode Expression1, Expression2;
        public string Type;
        public BinaryNode() { }
        public BinaryNode(string op, ExpressionNode expression1, ExpressionNode expression2) : base(op)
        {
            Expression1 = expression1;
            Expression2 = expression2;
            Type = Maximize();
        }

        public override string Maximize()
        {
            string type1 = Expression1.Maximize();
            string type2 = Expression2.Maximize();
            string op = base.Operation;
            if ((op == "==" || op == "!=") && type1 == "string" && type2 == "string")
            {
                return "bool";
            }
            if ((op == "&&" || op == "||" || op == "==" || op == ">" || op == "<" || op == ">=" || op == "<=") && type1 != "string" && type2 != "string")
            {
                return "bool";
            }
            if ((op == "+" || op == "-" || op == "*" || op == "/"))
            {
                if (type1 == "string" || type2 == "string")
                {
                    if (type1 != type2 || op != "+") throw new Exception("Type error: strings can only be added with each other");
                    return "string";
                }
                return MaxNumType(type1, type2);
            }

            throw new Exception($"Type error: cannot make operation \"{op}\" with types \"{type1}\", \"{type2}\"");
        }

        public override string ToPacalProgram()
        {
            string o;
            switch (Operation)
            {
                case "&&":
                    o = "AND";
                    break;
                case "||":
                    o = "OR";
                    break;
                default:
                    o = Operation;
                    break;
            }
            return ($" {Expression1.ToPacalProgram()} {o} {Expression2.ToPacalProgram()} ");
        }
    }
}
