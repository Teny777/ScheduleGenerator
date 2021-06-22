using RestrictionAnalyzer.Modules;
using System;
using System.Collections.Generic;

namespace RestrictionAnalyzer
{
    public static class Analyzer
    {
        public static (List<string> errors, string expr) Analyze(string restriction)
        {
            if (restriction is null)
            {
                throw new ArgumentNullException(nameof(restriction));
            }

            var io = new IO(restriction);
            var lexer = new Lexer(io);
            var parser = new Parser(lexer);

            parser.Parse();

            var result = (io.GetErrors(), parser.GetExpression());
            return result;
        }
    }
}
