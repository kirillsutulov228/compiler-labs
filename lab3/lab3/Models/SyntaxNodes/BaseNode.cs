using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3.Models.SyntaxNodes
{
    public class BaseNode
    {
        public BaseNode()
        {

        }

        private  List<string> _numericTypes = new() { "bool", "char", "int", "float" };

        protected string MaxNumType(string type1, string type2)
        {
            if (!IsNumeric(type1) || !IsNumeric(type2)) throw new Exception("Type error: expected numeric type");
            int i1 = _numericTypes.IndexOf(type1);
            int i2 = _numericTypes.IndexOf(type2);
            return (i1 > i2) ? type1 : type2;
        }

        protected bool IsNumeric(string type)
        {
            return _numericTypes.Contains(type);
        }

        public virtual string Maximize()
        {
            throw new Exception("Not implemented");
        }

        public virtual string ToPacalProgram()
        {
            throw new Exception("Not implemented");
        }
    }
}
