using System;
using System.Globalization;
using System.IO;
using System.Threading;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace pjpproject
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            var fileName = "C:\\Users\\Žigy-san\\Downloads\\pjp-main (3)\\pjp-main\\input.txt";
            var outputFileName = "C:\\Users\\Žigy-san\\Downloads\\pjp-main (3)\\pjp-main\\output.txt";

            if (!File.Exists(fileName))
            {
                Console.WriteLine("❌ Soubor nebyl nalezen.");
                return;
            }

            try
            {
                using var inputFile = new StreamReader(fileName);
                var input = new AntlrInputStream(inputFile);
                var lexer = new PLCLexer(input);
                var tokens = new CommonTokenStream(lexer);
                var parser = new PLCParser(tokens);

                var errorListener = new VerboseListener();
                parser.RemoveErrorListeners();
                parser.AddErrorListener(errorListener);

                var tree = parser.program();

                if (parser.NumberOfSyntaxErrors == 0)
                {
                    Console.WriteLine("✅ Bez syntax erroru");

                    var visitor = new TypeCheckerVisitor();
                    visitor.Visit(tree);

                    if (Errors.NumberOfErrors != 0)
                    {
                        Errors.PrintAndClearErrors();
                    }
                    else
                    {
                        Console.WriteLine("✅ Žádné sémantické chyby");

                        // Zavolání CodeGenVisitor pro generování kódu
                        var codeGenVisitor = new CodeGenVisitor();
                        codeGenVisitor.Visit(tree);
                        var instructions = codeGenVisitor.GetInstructions();

                        Console.WriteLine("✅ Vygenerovaný kód:");
                        foreach (var instr in instructions)
                        {
                            if (!string.IsNullOrWhiteSpace(instr))
                                Console.WriteLine(instr);
                        }

                        // Uložení do output.txt
                        File.WriteAllLines(outputFileName, instructions);
                        Console.WriteLine($"💾 Kód uložen do souboru: {outputFileName}");
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
