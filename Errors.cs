using Antlr4.Runtime;

namespace pjpproject
{   
    public class Errors
    {
        private static readonly List<string> ErrorsData = new List<string>();
        static public void ReportError(IToken token, string message)
        {
            ErrorsData.Add($"{token.Line}:{token.Column} - {message}");
        }
        public static int NumberOfErrors {  get { return ErrorsData.Count; } }
        public static void PrintAndClearErrors()
        {
            foreach (var error in ErrorsData)
            {
                Console.WriteLine(error);
            }
            ErrorsData.Clear(); 
        }
    }
}