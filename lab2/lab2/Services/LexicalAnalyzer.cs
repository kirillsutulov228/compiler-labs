using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using lab2.Models;
using System.Globalization;

namespace lab2.Services
{
    public class LexicalAnalyzer
    {
        public string Program;
        private List<string> _dataTypes = new List<string>()  {"int", "short", "char", "long", "bool", "void", "string", "float", "double" };
        private List<string> _operators = new List<string>() { "=", "+", "-", "*", "/", "==", "!=", "<", ">", ">=", "<=", "%", "+=", "-=", "++", "--", "%", "!", "&", "&&", "||", "|" };
        private List<string> _keysymbols = new List<string>() { ";", "{", "}", "(", ")", "[", "]", ".", "," };
        private List<string> _keywords = new List<string>() { "do", "while", "for", "if", "else", "return", "break" };
        private List<string> _strsymbols = new List<string>() { "\"", "\'" };
        public LexicalAnalyzer(string program)
        {
            Program = program;
        }

        public TokenizedProgram TokenizeProgram()
        {
            TokenizedProgram tokenizedProgram = new TokenizedProgram();
            List<string> allSymbols = _dataTypes.Concat(_dataTypes).Concat(_operators).Concat(_keywords).Concat(_keysymbols).Concat(_strsymbols).ToList();
            string buffer = String.Empty;
            int iter = 0;
            int varId = 0;
            int identificatorId = 0;
            int constantId = 0;
            int lineCounter = 1;
            
            while (iter < Program.Length)
            {
                char currentChar = Program[iter];
                char nextChar = (iter + 1) < Program.Length ? Program[iter + 1] : '\n';
                iter++;

                if (char.IsWhiteSpace(currentChar))
                {
                    if (currentChar == '\n') lineCounter++;
                    buffer = string.Empty;
                    continue;
                };

                buffer += currentChar;
                
                bool prevWasVarDeclaration = tokenizedProgram.Lexemes.Count > 0 && _dataTypes.Contains(tokenizedProgram.Lexemes.Last().Value ?? "");
                bool bufferIsNumber = double.TryParse(buffer, out _);
                bool isUnknownSymbol = !char.IsLetterOrDigit(currentChar) && currentChar != '_' && !allSymbols.Contains(currentChar.ToString());
                var nextCharIsSpaceOrKeysymbol = () => char.IsWhiteSpace(nextChar) || allSymbols.Contains(nextChar.ToString());

                if (currentChar == '\"') // read string
                {
                    bool isBadStr = false;
                    while (nextChar != '\"')
                    {
                        iter++;
                        buffer += nextChar;
                        if (nextChar == '\n')
                        {
                            tokenizedProgram.Lexemes.Add(
                                new Lexeme(Lexeme.LexemeType.ParsingError, -5, $"Error at line {lineCounter} <Missing quote>")
                            );
                            isBadStr = true;
                            break;
                        }

                        nextChar = Program[iter];
                    }
                    if (isBadStr) break;
                    buffer += nextChar;
                    tokenizedProgram.Lexemes.Add(new Lexeme(Lexeme.LexemeType.Constant, constantId++, buffer, new Dictionary<string, string>() { { "Type", "string" } }));
                    buffer = string.Empty;
                    iter++;
                }
                else if (currentChar == '\'') // read char
                {
                    buffer += nextChar;
                    if (nextChar == '\\')
                    {
                        iter++;
                        nextChar = Program[iter];
                        buffer += nextChar;
                    }
                    iter++;
                    nextChar = Program[iter];
                    buffer += nextChar;
                    if (nextChar != '\'')
                    {
                        tokenizedProgram.Lexemes.Add(
                            new Lexeme(Lexeme.LexemeType.ParsingError, -6, $"Error at line {lineCounter} <Bad char value>")
                        );
                        break;
                    }
                    iter++;
                    tokenizedProgram.Lexemes.Add(new Lexeme(Lexeme.LexemeType.Constant, constantId++, buffer, new Dictionary<string, string>() { { "Type", "char" } }));
                    buffer = string.Empty;
                }
                else if (buffer == "true" || buffer == "false")
                {
                    tokenizedProgram.Lexemes.Add(new Lexeme(Lexeme.LexemeType.Constant, constantId++, buffer, new Dictionary<string, string>() { { "Type", "bool" } }));
                    buffer = string.Empty;
                }
                else if (_dataTypes.Contains(buffer)) // read type
                {
                    tokenizedProgram.Lexemes.Add(
                        new Lexeme(Lexeme.LexemeType.DataType, _dataTypes.IndexOf(buffer), buffer)
                    );
                    buffer = string.Empty;
                }
                else if (_operators.Contains(buffer + nextChar)) //read double operator
                {
                    tokenizedProgram.Lexemes.Add(
                        new Lexeme(Lexeme.LexemeType.Operator, _operators.IndexOf(buffer + nextChar), buffer + nextChar)
                    );
                    buffer = string.Empty;
                    iter++;
                }
                else if (_operators.Contains(buffer)) // read operator
                {
                    tokenizedProgram.Lexemes.Add(new Lexeme(Lexeme.LexemeType.Operator, _operators.IndexOf(buffer), buffer));
                    buffer = string.Empty;
                }
                else if (_keysymbols.Contains(buffer) && !(buffer == "." && double.TryParse(nextChar.ToString(), out _))) // read keysymbol
                {
                    tokenizedProgram.Lexemes.Add(new Lexeme(Lexeme.LexemeType.Keysymbol, _keysymbols.IndexOf(buffer), buffer));
                    buffer = string.Empty;
                } 
                else if (_keywords.Contains(buffer) && !(buffer + nextChar == "dou")) // read keyword
                {
                    tokenizedProgram.Lexemes.Add(new Lexeme(Lexeme.LexemeType.Keyword, _keywords.IndexOf(buffer), buffer));
                    buffer = string.Empty;
                }
                else if (bufferIsNumber && nextCharIsSpaceOrKeysymbol()) // read number
                {
                    bool isReal = false;
                    int fractionalCnt = 0;
                    if (nextChar == '.')
                    {
                        
                        isReal = true;
                        buffer += nextChar;
                        iter++;
                        nextChar = Program[iter];
                        while (!nextCharIsSpaceOrKeysymbol())
                        {
                            fractionalCnt++;
                            buffer += nextChar;
                            iter++;
                            nextChar = Program[iter];
                        }
                        
                    }
                    if (prevWasVarDeclaration)
                    {
                        tokenizedProgram.Lexemes.Add(
                            new Lexeme(Lexeme.LexemeType.ParsingError, -4, $"Error at line {lineCounter} <Identificator \"{buffer}\" cannot be a number>")
                        );
                        break;
                    }
                    if (isReal && (!double.TryParse(buffer, NumberStyles.Float, CultureInfo.InvariantCulture, out _) || fractionalCnt == 0))
                    {
                        tokenizedProgram.Lexemes.Add(
                            new Lexeme(Lexeme.LexemeType.ParsingError, -7, $"Error at line {lineCounter} <Bad float number \"{buffer}\">")
                        );
                        break;
                    }
                    if (!isReal && buffer.Length > 1 && buffer[0] == '0')
                    {
                        tokenizedProgram.Lexemes.Add(
                            new Lexeme(Lexeme.LexemeType.ParsingError, -7, $"Error at line {lineCounter} <Bad int number \"{buffer}\">")
                        );
                        break;
                    }
                    tokenizedProgram.Lexemes.Add(new Lexeme(Lexeme.LexemeType.Constant, constantId++, buffer, new Dictionary<string, string>() { { "Type", isReal ? "float" : "int" } }));
                    buffer = string.Empty;
                }
                else if (char.IsLetterOrDigit(currentChar) && nextCharIsSpaceOrKeysymbol()) // read identifier
                {
                    if (char.IsDigit(buffer[0]))
                    {
                        tokenizedProgram.Lexemes.Add(
                            new Lexeme(Lexeme.LexemeType.ParsingError, -3, $"Error at line {lineCounter} <Identificator \"{ buffer }\" starts with number>")
                        );
                        break;
                    }
                    //if (!tokenizedProgram.Identificators.Exists(v => v.Name == buffer) && !prevWasVarDeclaration)
                    //{
                    //    tokenizedProgram.Lexemes.Add(
                    //        new Lexeme(Lexeme.LexemeType.ParsingError, -1, $"Error at line {lineCounter} <Unexpected identificator \"{buffer}\">")
                    //    );
                    //    break;
                    //}
                    if (prevWasVarDeclaration) {
                        tokenizedProgram.Identificators.Add(
                            new Variable(tokenizedProgram.Lexemes.Last().Value, buffer, varId.ToString())
                        );
                        varId++;
                    }
                    tokenizedProgram.Lexemes.Add(new Lexeme(Lexeme.LexemeType.Identifier, identificatorId++, buffer));
                    buffer = string.Empty;
                }
                else if (isUnknownSymbol) // throw error
                {
                    tokenizedProgram.Lexemes.Add(new Lexeme(Lexeme.LexemeType.ParsingError, -2, $"Error at line {lineCounter} <Cannot resolve symbol \"{ buffer }\">"));
                    break;
                }
            }
            return tokenizedProgram;
        }
    }
}
