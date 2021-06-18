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
                //"R(t1, s1, k1, c1, x1, d1) И \"Иванов В.В.\" = d1 -> d1 != 5 И x1 = 1",
                //"R(t1, s1, k1, c1, x1, d1) И t1 = t1 + 1 -> d1 != 5 И x1 = 1",
                "R(t1, s1, k1, c1, x1, d1) И R(t2, s2, k2, c2, x2, d2) И t1 = t2 И x1 + 2 = x2 И d1 = d3 -> R(t3, s3, k3, c3, x3, d3) И t2 = t3 И x1 + 1 = x3 И d2 = d3 И d1 = d3",
                "R(x1, s1, k1, c1, t1, d1) И t1 = t1 + 1 - 1-> d1 != 5 И x1 = \"123\"",
            };
            
            foreach (var aa in a)
            {
                var (errors, log, expr) = Analyzer.Analyze(aa);
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
