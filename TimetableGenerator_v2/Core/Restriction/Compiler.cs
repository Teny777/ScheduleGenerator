using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Generator.Core.Restriction
{
    public static class Compilier
    {
        public static CompilerResults Compile(string[] lines)
        {
            CompilerResults result;
            try
            {

                var po = new Dictionary<string, string>
                {
                    { "CompilerVersion", "v4.0"}
                };
                var codeCompiler = new CSharpCodeProvider(po);
                var parameters = new CompilerParameters()
                {
                    GenerateInMemory = true,
                    IncludeDebugInformation = false
                };
                //parameters.ReferencedAssemblies.Add(ReferenceLocation);
                //results = codeCompiler.CompileAssemblyFromFile(parameters, CodeFile);
                result = codeCompiler.CompileAssemblyFromSource(parameters, lines);
                if (result.Errors.HasErrors)
                {
                    var build = new StringBuilder();
                    foreach (CompilerError my in result.Errors)
                    {
                        build.AppendLine(my.ErrorText);
                    }
                    Console.WriteLine(build.ToString(), "Ошибки компиляции.");
                    //return null;
                    throw new Exception("Синтаксическая ошибка");
                }
            }
            catch (Exception e)
            {
                if (e.Message == "Синтаксическая ошибка")
                {
                    throw new Exception("Синтаксическая ошибка");
                }
                Console.WriteLine(e.Message, "Ошибка компиляции");
                //return null;
                throw new Exception("Семантическая ошибка");
            }
            //Console.WriteLine("Компиляция завершена");

            return result;
        }

        public static string CreateFunction(string expression, int weightNegative)
        {
            var assessment = expression.Substring(0, expression.IndexOf(' '));
            var lambdasPrev = Regex.Matches(expression.Substring(0, expression.IndexOf("->")), @"R[(][a-z0-9,\s]+[)]") // тут от a до z - лучше 6 стандартных символов перечислить
                .Cast<Match>()
                .Select(x => x.Value)
                .ToArray();
            var lambdasNext = Regex.Matches(expression.Substring(expression.IndexOf("->")), @"R[(][a-z0-9,\s]+[)]") // тут от a до z - лучше 6 стандартных символов перечислить
                .Cast<Match>()
                .Select(x => x.Value)
                .ToArray();
            string start = expression.Substring(expression.LastIndexOf(lambdasPrev.Last()) + lambdasPrev.Last().Length + 4);
            string requirements;
            if (start.IndexOf(" -> ") == -1)
            {
                requirements = "true";
            }
            else
            {
                requirements = start.Remove(start.IndexOf(" -> "));
            }
            string conditions = expression.Substring(expression.IndexOf("->") + 3);
            if (lambdasNext.Length > 0)
            {
                conditions = conditions.Substring(conditions.LastIndexOf(lambdasNext.Last()) + lambdasNext.Last().Length + 4);
            }

            string _in = " in ";
            string _not_in = " not in ";

            while (requirements.Contains(_not_in))
            {
                string res = Regex.Match(requirements, @"[a-z]+[0-9]+ not in").Value;
                int index = requirements.IndexOf(res);
                int index2 = requirements.IndexOf(_not_in) + 1;
                string sub = res.Remove(res.Length - 7);
                string st = requirements.Substring(index2 + 8);
                int index4 = st.IndexOf(']');
                string listreq = '{' + st.Remove(index4) + '}';
                string mas = $"!MyContains(new[] { listreq }, {sub})";
                string hh = $"{sub} not in [{st.Remove(index4)}]";
                requirements = requirements.Replace(hh, mas);
            }

            while (conditions.Contains(_not_in))
            {
                string res = Regex.Match(conditions, @"[a-z]+[0-9]+ not in").Value;
                int index = conditions.IndexOf(res);
                int index2 = conditions.IndexOf(_not_in) + 1;
                string sub = res.Remove(res.Length - 7);
                string st = conditions.Substring(index2 + 8);
                int index4 = st.IndexOf(']');
                string listreq = '{' + st.Remove(index4) + '}';
                string mas = $"!MyContains(new[] { listreq }, {sub})";
                string hh = $"{sub} not in [{st.Remove(index4)}]";
                conditions = conditions.Replace(hh, mas);
            }

            while (requirements.Contains(_in))
            {
                string res = Regex.Match(requirements, @"[a-z]+[0-9]+ in").Value;
                int index = requirements.IndexOf(res);
                int index2 = requirements.IndexOf(_in) + 1;
                string sub = res.Remove(res.Length - 3);
                string st = requirements.Substring(index2 + 4);
                int index4 = st.IndexOf(']');
                string listreq = '{' + st.Remove(index4) + '}';
                string mas = $"MyContains(new[] { listreq }, {sub})";
                string hh = $"{sub} in [{st.Remove(index4)}]";
                requirements = requirements.Replace(hh, mas);
            }

            while (conditions.Contains(_in))
            {
                string res = Regex.Match(conditions, @"[a-z]+[0-9]+ in").Value;
                int index = conditions.IndexOf(res);
                int index2 = conditions.IndexOf(_in) + 1;
                string sub = res.Remove(res.Length - 3);
                string st = conditions.Substring(index2 + 4);
                int index4 = st.IndexOf(']');
                string listreq = '{' + st.Remove(index4) + '}';
                string mas = $"MyContains(new[] { listreq }, {sub})";
                string hh = $"{sub} in [{st.Remove(index4)}]";
                conditions = conditions.Replace(hh, mas);
            }

            StringBuilder function = new StringBuilder();

            #region fillFunc;
            function.Append(@"using System.Collections.Generic;
namespace IntroductionRestrictions_v2
{
    public static class TestClass
    {
        public static string Method(object[] obj)
        {
            int assessmentInt = " + assessment + @";
            int assessmentNegat = " + weightNegative + @";
            int assessmentPositive = 0;
            int assessmentNegative = 0;
            var items1 = (List<string>)obj[0];
            var items2 = (List<string>)obj[1];
            var items3 = (List<int>)obj[2];
            var items4 = (List<string>)obj[3];
            var items5 = (List<int>)obj[4];
            var items6 = (List<int>)obj[5];

            var rows = new List<Row2>();
            for (int i = 0; i < items1.Count; i++)
            {
                rows.Add(new Row2(items1[i], items2[i], items3[i], items4[i], items5[i], items6[i]));
            }" + "\n");

            for (int i = 0; i < lambdasPrev.Length; i++)
            {
                function.AppendLine($"foreach (var row{i + 1} in rows)");
                function.AppendLine("{");
            }

            function.AppendLine($"if ({MyReplace(requirements)})");
            function.AppendLine("{");
            function.AppendLine("bool resultT = false;");

            for (int i = 0; i < lambdasNext.Length; i++)
            {
                function.AppendLine($"foreach (var row{lambdasPrev.Length + i + 1} in rows)");
                function.AppendLine("{");
                function.AppendLine("if (resultT) break;");
            }

            function.AppendLine($"if ({MyReplace(conditions)})");
            function.AppendLine("{");
            function.AppendLine("resultT = true;");
            function.AppendLine("}");

            for (int i = 0; i < lambdasNext.Length; i++)
            {
                function.Append("\n}\n");
            }

            function.Append($"if (resultT)");
            function.Append("\n{\n");
            function.Append("assessmentPositive++;");
            function.Append("\n}\n");
            function.Append("else");
            function.Append("\n{\n");
            function.Append("assessmentNegative++;");
            function.Append("\n}\n");

            function.Append("\n}\n");

            for (int i = 0; i < lambdasPrev.Length; i++)
            {
                function.Append("\n}\n");
            }

            function.Append(@"var result = assessmentPositive * assessmentInt - assessmentNegative * assessmentNegat;
                        string space = " + "\" \";" +
            @"return " + '\"' + '\"' + @"+ assessmentPositive + space + assessmentNegative + space + result;
        }

        private static bool MyContains(string[] a, string b)
        {
            foreach (var c in a) if (c == b) return true;
            return false;
        }

        private static bool MyContains(int[] a, int b)
        {
            foreach (var c in a) if (c == b) return true;
            return false;
        }
    }

    public class Row2
    {
        public Row2(string t, string s, int k, string c, int x, int d)
        {
            this.t = t;
            this.s = s;
            this.k = k;
            this.c = c;
            this.x = x;
            this.d = d;
        }
        public string t { get; set; }
        public string s { get; set; }
        public int k { get; set; }
        public string c { get; set; }
        public int x { get; set; }
        public int d { get; set; }
    }
}");
            #endregion fillFunc;

            return function.ToString();
        }

        private static string MyReplace(string value)
        {
            string[] vars = Regex.Matches(value, @"\w+\d+") // тут от a до z - лучше 6 стандартных символов перечислить
                .Cast<Match>()
                .Select(x => x.Value)
                .Distinct()
                .Where(x => char.IsLetter(x[0]))
                .ToArray();

            foreach (var p in vars)
            {
                string a = p;
                string b = string.Empty;
                int val;

                for (int i = 0; i < a.Length; i++)
                {
                    if (char.IsDigit(a[i]))
                        b += a[i];
                }

                val = int.Parse(b);

                value = value.Replace(p, $"row{val}.{p[0]}");
            }

            return value;
        }

        public static MethodInfo CreateMethod(CompilerResults compilerResults)
        {
            MethodInfo str = compilerResults.CompiledAssembly.GetType("IntroductionRestrictions_v2.TestClass").GetMethod("Method");
            return str;
        }

        public static string RunMethod(List<Row> rows, MethodInfo method)
        {
            object[] arrParams =
            {
                rows.Select(x => x.t).ToList(),
                rows.Select(x => x.s).ToList(),
                rows.Select(x => x.k).ToList(),
                rows.Select(x => x.c).ToList(),
                rows.Select(x => x.x).ToList(),
                rows.Select(x => x.d).ToList(),
            };

            var str = (string)method.Invoke(null, new object[] { arrParams });
            return str;
        }
    }
}
