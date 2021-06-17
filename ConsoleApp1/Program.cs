using RestrictionAnalyzer;
using System;

namespace ConsoleApp1
{
    public class Program
    {
        // test
        public static void Main(string[] args)
        {
            var a = new string[]
            {
                //"R(t1, s1, k1, c1, x1, d1) И x1 = 1 -> d1 != 1",
                //"R(t1, s1, k1, c1, x1, d1) И t1 = \"Иванов В.В.\" -> d1 != 5",
                //"R(t1, s1, k1, c1, x1, d1) И t1 ? = \"Иванов В.В.\" -> d1 != 5",
                "R(t1, s1, k1, c1, x1, d1) И \"Иванов В.В.\"= d1 -> d1 != 5 И x1 = 1",
            };

            foreach (var aa in a)
            {
                var (errors, log) = Analyzer.Analyze(aa);
                Console.WriteLine(aa);
                if (errors.Count > 0)
                {
                    foreach (var error in errors)
                    {
                        Console.WriteLine(error);
                    }
                }
                else
                {
                    Console.WriteLine("Ошибок нет");
                }
                Console.WriteLine();
                
            }

            Console.ReadLine();
        }
    }
}
