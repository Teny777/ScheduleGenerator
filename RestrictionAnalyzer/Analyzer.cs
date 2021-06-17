using RestrictionAnalyzer.Modules;
using System;
using System.Collections.Generic;

namespace RestrictionAnalyzer
{
    public static class Analyzer
    {
        public static (List<string> errors, List<string> log) Analyze(string restriction)
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

            //Console.WriteLine(restriction);
            parser.Parse();
            //Console.WriteLine(restriction);
            var result = (io.GetErrors(), io.GetLog());
            return result;
        }
    }
}
