using System;
using System.Collections.Generic;
using Antlr4.Runtime;

namespace pjpproject
{
    public static class Errors
    {
        private static readonly List<string> ErrorsData = new();

        public static void ReportError(IToken token, string message)
        {
            ErrorsData.Add($"{token.Line}:{token.Column} - {message}");
        }

        public static int NumberOfErrors => ErrorsData.Count;

        public static void PrintAndClearErrors()
        {
            foreach (var error in ErrorsData)
            {
                Console.WriteLine($"❌ {error}");
            }
            ErrorsData.Clear();
        }
    }
}
