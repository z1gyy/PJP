using System;
using System.Globalization;
using System.IO;
using System.Threading;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using pjpproject;

namespace MyNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            var fileName = "C:\\Users\\Žigy-san\\Downloads\\pjp-main (1)\\pjp-main\\input.txt";

            if (!File.Exists(fileName))
            {
                Console.WriteLine("❌ Soubor nebyl nalezen.");
                return;
            }

            try
            {
                using var inputFile = new StreamReader(fileName);
                AntlrInputStream input = new AntlrInputStream(inputFile);
                PLCLexer lexer = new PLCLexer(input);
                CommonTokenStream tokens = new CommonTokenStream(lexer);
                PLCParser parser = new PLCParser(tokens);

                var errorListener = new VerboseListener();
                parser.RemoveErrorListeners();
                parser.AddErrorListener(errorListener);

                IParseTree tree = parser.program();

                if (parser.NumberOfSyntaxErrors == 0)
                {
                    Console.WriteLine("✅ Bez syntax erroru");

                    if (Errors.NumberOfErrors != 0)
                    {
                        Errors.PrintAndClearErrors();
                    }
                    else
                    {
                        Console.WriteLine("✅ Žádné sémantické chyby");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Výjimka při parsování: {ex.Message}");
            }
        }
    }
}
