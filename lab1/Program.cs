using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace lab1
{
    class Program
    {
        static double readDouble()
        {
            double res;
            string input;
            Console.WriteLine("Введите вещественное число");
            input = Console.ReadLine();
            while (!double.TryParse(input, out res))
            {
                Console.WriteLine("Некорректный ввод, введите вещественное число");
                input = Console.ReadLine();
            } 
            return res;
        }

        static void printRoots(List<double> roots)
        {
            if (roots.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Корней нет");
                Console.ResetColor();
                return;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < roots.Count; ++i)
            {
                Console.WriteLine("x{0} = {1:F5}", i + 1, roots[i]);
            }
            Console.ResetColor();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Разработал студент группы РТ5-31Б Ходосов Михаил");
            Console.WriteLine("Решение биквадратных уравнений вида Ax^4 + Bx^2 + C = 0\n");

            double A, B, C;
            if (args.Length != 3)
            {
                Console.WriteLine("Ввод коэффициента A");
                A = readDouble();
                while (A == 0)
                {
                    Console.WriteLine("Коэффициент А не может быть равен нулю.");
                    A = readDouble();
                }
                Console.WriteLine("Ввод коэффициента B");
                B = readDouble();
                Console.WriteLine("Ввод коэффициента C");
                C = readDouble();
            } else
            {
                if (!double.TryParse(args[0], out A) || !double.TryParse(args[1], out B) || !double.TryParse(args[2], out C) || A == 0)
                {
                    Console.WriteLine("Некорректные значения коэффициентов");
                    return;
                }
            }

            double D = B * B - 4.0 * A * C;
            
            List<double> roots = new List<double>();

            if (D < 0)
            {
                printRoots(roots);
                return;
            }

            double y1 = (-B + Math.Sqrt(D)) / (2.0 * A), y2 = (-B - Math.Sqrt(D)) / (2.0 * A);

            if (y2 < y1) 
                (y1, y2) = (y2, y1);

            if (y1 < 0)
            {
                if (y2 < 0)
                {
                    printRoots(roots);
                    return;
                }
                if (y2 == 0)
                {
                    roots.Add(0);
                    printRoots(roots);
                    return;
                }
                roots.Add(Math.Sqrt(y2));
                roots.Add(-Math.Sqrt(y2));
                printRoots(roots);

            } else
            {
                if (y1 == 0)
                {
                    roots.Add(0);
                    if (y2 != 0)
                    {
                        roots.Add(Math.Sqrt(y2));
                        roots.Add(-Math.Sqrt(y2));
                    }
                    printRoots(roots);
                    return;
                }
                if (y1 == y2)
                {
                    roots.Add(Math.Sqrt(y2));
                    roots.Add(-Math.Sqrt(y2));
                    printRoots(roots);
                    return;
                }

                roots.Add(Math.Sqrt(y1));
                roots.Add(-Math.Sqrt(y1));
                roots.Add(Math.Sqrt(y2));
                roots.Add(-Math.Sqrt(y2));
                printRoots(roots);
            }
        }
    }
}
