using System;
using System.IO;
using Antlr4.Runtime;

namespace pjpproject
{
    public class VerboseListener : BaseErrorListener
    {
        public override void SyntaxError(
            TextWriter output,
            IRecognizer recognizer,
            IToken offendingSymbol,
            int line,
            int charPositionInLine,
            string msg,
            RecognitionException e)
        {
            Console.WriteLine($"❌ Syntax Error na řádku {line}, sloupec {charPositionInLine}: {msg}");
        }
    }
}
