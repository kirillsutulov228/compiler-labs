using lab3.Models;
using lab3.Models.SyntaxNodes;
using lab3.Services;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;
namespace lab3
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

            var root = syntaxAnalyzer.GenerateTree();

            Console.WriteLine(root.ToPacalProgram());

            //Console.WriteLine("");

            //List<Type> extraTypes = new List<Type>()
            //{
            //    typeof(UnaryNode), typeof(StatementNode), typeof(SetNode), typeof(SequenceNode), typeof(NullNode), typeof(NotNode),
            //    typeof(IfNode), typeof(IdentifierNode), typeof(ExpressionNode), typeof(DoWhileNode), typeof(DeclarationNode), typeof(ConstantNode),
            //    typeof(BaseNode), typeof(BinaryNode), typeof(IncrementNode), typeof(BlockNode), typeof(BracketNode)
            //};

            //var serializer = new XmlSerializer(typeof(BlockNode), extraTypes.ToArray());
            //var sw = new StringWriter();
            //var xw = XmlWriter.Create(sw, new() { Indent = true });

            //serializer.Serialize(xw, root);
            //var xml = sw.ToString();

            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml(xml);
            //var json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);
            //Console.WriteLine(json);

        }
    }
}