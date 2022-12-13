using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab3.Services;
using lab3.Models;
using lab3.Models.SyntaxNodes;

namespace lab3.Services
{

    public class SyntaxAnalyzer
    {
        private TokenizedProgram _tokenizedProgram { get; set; }
        private Lexeme? _currToken;
        private Lexeme? _nextToken;
        private Lexeme? _prevToken;
        private int _currTokenIndex = -1;
        IdentifiersTable? top = null;
        public SyntaxAnalyzer(TokenizedProgram tokenizedProgram)
        {
            _tokenizedProgram = tokenizedProgram;
        }
        public StatementNode GenerateTree()
        {
            Reset();
            NextToken();
            StatementNode node = GetBlock();
            return node;
        }
        private void NextToken()
        {
            if (_currTokenIndex + 1 < _tokenizedProgram.Lexemes.Count)
            {
                _currTokenIndex++;
                _currToken = _tokenizedProgram.Lexemes[_currTokenIndex];
                if (_currTokenIndex + 1 < _tokenizedProgram.Lexemes.Count)
                {
                    _nextToken = _tokenizedProgram.Lexemes[_currTokenIndex + 1];
                }
                else
                {
                    _nextToken = null;
                }
                if (_currTokenIndex - 1 >= 0)
                {
                    _prevToken = _tokenizedProgram.Lexemes[_currTokenIndex - 1];
                }
            }
            else
            {
                top = new IdentifiersTable(null);
                Reset();
            }
        }
        private void Reset()
        {
            top = new IdentifiersTable(null);
            _currTokenIndex = -1;
            _currToken = null;
            _nextToken = null;
            _prevToken = null;
        }
        private void CheckToken(string value)
        {
            if (_currToken == null || _currToken.Value != value)
            {
                throw new Exception($"Invalid token with index {_currTokenIndex} (excepted: {value})");
            }
            NextToken();
        }
        private StatementNode GetBlock()
        {
            CheckToken("{");
            IdentifiersTable table = top;
            top = new IdentifiersTable(top);
            SequenceNode node = GetStatementSequense();
            CheckToken("}");
            top = table;
            return new BlockNode(node);
        }

        private StatementNode GetStatement()
        {
            if (_currToken.Value == "{")
            {
                throw new Exception("invalid block");
            }
            if (_currToken.Value == ";")
            {
                NextToken();
                return null; //new NullNode();
            }
            if (_currToken.Type == Lexeme.LexemeType.DataType)
            {
                string type = _currToken.Value;
                NextToken();
                string name = _currToken.Value;
                if (top.Get(_currToken) != null)
                {
                    throw new Exception($"Variable {name} already declared");
                }
                if (_currToken?.Type != Lexeme.LexemeType.Identifier)
                {
                    throw new Exception($"Expected Identificator");
                }
                top.Put(_currToken, new IdentifierNode(name, type));
                DeclarationNode node = new DeclarationNode(name, type);
                NextToken();
                return node;
            }
            if (_currToken.Value == "if")
            {
                NextToken();
                CheckToken("(");
                ExpressionNode condition = BoolOr();
                CheckToken(")");
                StatementNode then = GetBlock();
                StatementNode? elseStatement = null;
                if (_currToken.Value == "else")
                {
                    NextToken();
                    elseStatement = null;
                    if (_currToken.Value == "{")
                    {
                        elseStatement = GetBlock();
                    } else if (_currToken.Value == "if")
                    {
                        elseStatement = GetStatementSequense();
                    }
                }
                return new IfNode(condition, then, elseStatement);
            }
            if (_currToken.Value == "do")
            {
                NextToken();
                StatementNode body = GetBlock();
                CheckToken("while");
                CheckToken("(");
                ExpressionNode cond = BoolOr();
                CheckToken(")");
                CheckToken(";");
                return new DoWhileNode(body, cond);
            }
            if (_currToken.Value == "++" || _currToken.Value == "--" && _prevToken.Type == Lexeme.LexemeType.Identifier)
            {
                string op = _currToken.Value;
                string id = _prevToken.Value;
                var idNode = top.Get(_prevToken);
                if (idNode == null)
                {
                    throw new Exception($"Variable {_prevToken.Value} undefined");
                }
                NextToken();
                CheckToken(";");
                return new IncrementNode(op, id, idNode.Type);
            }
            if (_currToken.Value == "=")
            {
                return Assign();
            }
            if (_currToken.Type == Lexeme.LexemeType.Identifier)
            {
                NextToken();
                return GetStatement();
            }
            
            var exp = BoolOr();
            CheckToken(";");
            return exp; //new NullNode();
        }

        private StatementNode Assign()
        {
            if (_prevToken.Type != Lexeme.LexemeType.Identifier) throw new Exception("Syntax error");
            string name = _prevToken.Value;
            IdentifierNode identifier = top.Get(_prevToken);
            if (identifier == null)
            {
                throw new Exception($"undefined variable {name}");
            }
            NextToken();
            SetNode statement = new SetNode(name, BoolOr(), identifier.Type);
            CheckToken(";");
            return statement;
        }
        private SequenceNode GetStatementSequense()
        {
            if (_currToken == null || _currToken.Value == "}") return null; //new NullNode();
            while (_currToken.Value == ";") NextToken();
            return new SequenceNode(GetStatement(), GetStatementSequense());
        }


        private ExpressionNode BoolOr()
        {
            ExpressionNode expression = BoolAnd();
            while (_currToken.Value == "||")
            {
                string op = _currToken.Value;
                NextToken();
                expression = new BinaryNode(op, expression, BoolAnd());
            }
            return expression;
        }
        private ExpressionNode BoolAnd()
        {
            ExpressionNode expression = BoolMatch();
            while (_currToken.Value == "&&")
            {
                string op = _currToken.Value;
                NextToken();
                expression = new BinaryNode(op, expression, BoolMatch());
            }
            return expression;
        }

        private ExpressionNode BoolMatch()
        {
            ExpressionNode expression = Expression();
            while (_currToken.Value == "==" || _currToken.Value == ">" || _currToken.Value == "<" || _currToken.Value == "<=" || _currToken.Value == ">=" || _currToken.Value == "!=")
            {
                string op = _currToken.Value;
                NextToken();
                expression = new BinaryNode(op, expression, Expression());
            }
            return expression;
        }
        private ExpressionNode Expression()
        {
            ExpressionNode expression = Term();
            while (_currToken.Value == "+" ||
                _currToken.Value == "-")
            {
                string op = _currToken.Value;
                NextToken();
                expression = new BinaryNode(op, expression, Term());
            }
            return expression;
        }

        private ExpressionNode Term()
        {
            ExpressionNode expression = Unary();
            while (_currToken.Value == "*" ||
                _currToken.Value == "/")
            {
                string op = _currToken.Value;
                NextToken();
                expression = new BinaryNode(op, expression, Unary());
            }
            return expression;
        }

        private ExpressionNode Unary()
        {
            if (_currToken.Value == "-" || _currToken.Value == "!")
            {
                NextToken();
                return new UnaryNode("-", Unary());
            }
            else
            {
                return Factor();
            }
        }
        private ExpressionNode Factor()
        {
            ExpressionNode expressionNode = null;

            if (_currToken.Type == Lexeme.LexemeType.Constant)
            {
                expressionNode = new ConstantNode(_currToken.Value, _currToken.Attributes["Type"]);
                NextToken();
                return expressionNode;
            }
            if (_currToken.Type == Lexeme.LexemeType.Identifier)
            {
                IdentifierNode identifier = top.Get(_currToken);
                if (identifier == null)
                {
                    throw new Exception($"undefined variable {_currToken.Value}");
                }
                NextToken();
                return identifier;
            }
            if (_currToken.Value == "(")
            {
                NextToken();
                expressionNode = BoolOr();
                CheckToken(")");
                return new BracketNode(expressionNode);
            }
            throw new Exception("Syntax error");
        }
    }
}
