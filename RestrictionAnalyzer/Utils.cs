using RestrictionAnalyzer.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestrictionAnalyzer
{
    internal static class Utils
    {
        internal static string ReadWhile(this IO io, Func<char, bool> func)
        {
            var builder = new StringBuilder();
            var ch = io.CurrentChar;
            while (ch != null && func((char)ch))
            {
                builder.Append((char)ch);
                ch = io.GetNextChar();
            }
            var str = builder.ToString();
            return str;
        }
    }
}
