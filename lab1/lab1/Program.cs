using lab1.Models;
using lab1.Services;
using System.Text.Json;

namespace lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string program = File.ReadAllText("../../../Input/program1.txt");
            LexicalAnalyzer analizer = new LexicalAnalyzer(program);
            var tokenized = analizer.TokenizeProgram();

            Console.WriteLine("============== Токены ===========================\n");
            tokenized.Lexemes.ForEach(v => Console.WriteLine($"Тип: \"{v.Type}\", ID: \"{v.Id}\", Значение: {v.Value}"));

            Console.WriteLine("\n============== Идентификаторы ======================\n");
            tokenized.Identificators.ForEach(v => Console.WriteLine($"Тип: \"{v.Type}\", ID: \"{v.Id}\" Имя: \"{v.Name}\""));

            //Console.WriteLine("\n============== JSON ============================\n");
            //Console.WriteLine(tokenized.ToJSON<TokenizedProgram>());

        }
    }
}