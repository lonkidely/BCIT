using System;
using System.Collections;
using System.Collections.Generic;
using Figures;
using Matrix;

namespace lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            //Создание объектов классов фигур
            Rectangle rectangle = new Rectangle(3, 4);
            Square square = new Square(5);
            Circle circle = new Circle(2.7282);


            ArrayList array = new ArrayList
            {
                rectangle,
                square,
                circle
            };

            Console.WriteLine("Array list перед сортировкой");
            foreach (var elem in array)
                Console.WriteLine(elem);
            

            array.Sort();

            Console.WriteLine("\nArray list после сортировки");
            foreach (var elem in array)
                Console.WriteLine(elem);


            List<GeometricFigure> list = new List<GeometricFigure>
            {
                rectangle,
                square,
                circle
            };

            Console.WriteLine("\n\nList перед сортировкой");
            foreach (var elem in array)
                Console.WriteLine(elem);

            list.Sort();

            Console.WriteLine("\nList после сортировки");
            foreach (var elem in array)
                Console.WriteLine(elem);

            Console.WriteLine("\n\nМатрица");
            Matrix<GeometricFigure> matrix = new Matrix<GeometricFigure>(3, 3, 3, new FigureMatrixCheckEmpty());
            matrix[0, 0, 0] = rectangle;
            matrix[1, 1, 1] = square;
            matrix[2, 2, 2] = circle;
            Console.WriteLine(matrix.ToString());

            try
            {
                GeometricFigure tmp = matrix[66, 1777, 11];
            }
            catch (ArgumentOutOfRangeException err)
            {
                Console.WriteLine(err.Message);
            }


            Console.WriteLine("\n\nСтек");

            SimpleStack<GeometricFigure> stack = new SimpleStack<GeometricFigure>();
            stack.Push(rectangle);
            stack.Push(square);
            stack.Push(circle);

            while (stack.Count > 0)
            {
                GeometricFigure f = stack.Pop();
                Console.WriteLine(f);
            }
        }
    }
}
