using RestrictionAnalyzer.Modules;
using RestrictionAnalyzer.Tools;
using System;

namespace RestrictionAnalyzer
{
    public static class Analyzer
    {
        public static void Analyze(string restriction)
        {
            if (restriction is null)
            {
                throw new ArgumentNullException(nameof(restriction));
            }

            restriction = restriction
                .Replace("&&", "И")
                .Replace("||", "ИЛИ")
                .Replace("==", "=");

            var io = new IO(restriction);
            var lexer = new Lexer(io);
            var parser = new Parser(lexer);

            parser.Parse();
            Console.WriteLine(restriction);

            while (true)
            {
                var token = lexer.GetNextToken();
                if (token is EndToken) break;
                Console.WriteLine(token);
            }

            Console.WriteLine(restriction);

            io.PrintListing();
        }
    }
}
