using System;
using Figures;

namespace lab2
{

    class Program
    {
        static void Main(string[] args)
        {
            Rectangle rectangle = new Rectangle(2, 3);
            rectangle.Print();

            Square square = new Square(5);
            square.Print();

            Circle circle = new Circle(4);
            circle.Print();
        }
    }
}
