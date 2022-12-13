using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace lab3.Models
{
    public class TokenizedProgram : BaseModel
    {
        public List<Lexeme> Lexemes = new List<Lexeme>();
        public List<Variable> Identificators = new List<Variable>();
        
        public TokenizedProgram() { }

    }
}
