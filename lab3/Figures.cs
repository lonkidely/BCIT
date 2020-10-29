using System;

namespace Figures
{
    abstract class GeometricFigure : IComparable
    {
        public string Type { get; protected set; }

        public abstract double Area();

        public override string ToString()
        {
            return this.Type + " площадью " + this.Area().ToString();
        }

        public int CompareTo(object obj)
        {
            GeometricFigure right = (GeometricFigure)obj;

            if (this.Area() < right.Area()) 
                return -1;
            else if (this.Area() == right.Area()) 
                return 0;
            else 
                return 1;
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

        public override string ToString()
        {
            return this.Type + " шириной " + this.Width.ToString() + ", высотой " + this.Height.ToString() + ", площадью " + this.Area().ToString();
        }

        public void Print()
        {
            Console.WriteLine(this.ToString());
        }
    }

    class Square : Rectangle, IPrint
    {
        public Square(double size) : base(size, size)
        {
            this.Type = "Квадрат";
        }

        public override string ToString()
        {
            return this.Type + " со стороной " + this.Width.ToString() + ", площадью " + this.Area().ToString();
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

        public override string ToString()
        {
            return this.Type + " радиусом " + this.Radius.ToString() + ", площадью " + this.Area().ToString();
        }

        public void Print()
        {
            Console.WriteLine(this.ToString());
        }
    }
}