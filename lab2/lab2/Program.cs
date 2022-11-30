using lab2.Models;
using lab2.Models.SyntaxNodes;
using lab2.Services;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using Newtonsoft.Json;
namespace lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var program = File.ReadAllText("../../../Input/program1.txt");
            LexicalAnalyzer lexicalAnalyzer = new LexicalAnalyzer(program);
            var tokens = lexicalAnalyzer.TokenizeProgram();
            if (tokens.Lexemes.Last<Lexeme>().Type == Lexeme.LexemeType.ParsingError)
            {
                throw new Exception(tokens.Lexemes.Last<Lexeme>().Value);
            }
            SyntaxAnalyzer syntaxAnalyzer = new SyntaxAnalyzer(tokens);

            SequenceNode root = syntaxAnalyzer.GenerateTree() as SequenceNode;

            List<Type> extraTypes = new List<Type>()
            {
                typeof(UnaryNode), typeof(StatementNode), typeof(SetNode), typeof(SequenceNode), typeof(NullNode), typeof(NotNode),
                typeof(IfNode), typeof(IdentifierNode), typeof(ExpressionNode), typeof(DoWhileNode), typeof(DeclarationNode), typeof(ConstantNode),
                typeof(BaseNode), typeof(ArithmeticNode), typeof(IncrementNode)
            };

            var serializer = new XmlSerializer(typeof(SequenceNode), extraTypes.ToArray());
            var sw = new StringWriter();
            var xw = XmlWriter.Create(sw, new() { Indent = true });

            serializer.Serialize(xw, root);
            var xml = sw.ToString();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            var json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(json);

        }
    }
}