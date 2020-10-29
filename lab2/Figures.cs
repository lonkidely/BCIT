using System;

namespace Figures
{
    abstract class GeometricFigure
    {
        public string Type { get; protected set; }

        public abstract double Area();

        public override string ToString()
        {
            return this.Type + " площадью " + this.Area().ToString();
        }
    }

    interface IPrint
    {
        void Print();
    }

    class Rectangle : GeometricFigure, IPrint
    {

        public double Height { get; protected set; }
        public double Width { get; protected set; }

        public Rectangle(double ph, double pw)
        {
            this.Height = ph;
            this.Width = pw;
            this.Type = "Прямоугольник";
        }

        public override double Area()
        {
            return (this.Height * this.Width);
        }

        public void Print()
        {
            Console.WriteLine(this.ToString());
        }
    }

    class Square: Rectangle, IPrint
    {
        public Square(double size) : base(size, size)
        {
            this.Type = "Квадрат";
        }
    }

    class Circle : GeometricFigure, IPrint
    {
        public double Radius { get; protected set; }

        public Circle(double pr)
        {
            this.Radius = pr;
            this.Type = "Круг";
        }

        public override double Area()
        {
            return this.Radius * this.Radius * Math.PI;
        }

        public void Print()
        {
            Console.WriteLine(this.ToString());
        }
    }
}