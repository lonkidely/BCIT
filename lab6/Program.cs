using System;
using System.Reflection;
using Figures;

namespace lab6
{
    delegate double ArithmeticOrGeometricMean(double x, double y);
    class Program
    {
        static void Main(string[] args)
        {
            static double ArithmeticMean(double x, double y)
            {
                return 0.5 * (x + y);
            }

            static double GeometricMean(double x, double y)
            {
                return Math.Sqrt(x * x + y * y);
            }

            static void ArithmeticOrGeometricMeanMethod(double x, double y, ArithmeticOrGeometricMean function)
            {
                Console.WriteLine(function(x, y));
            }

            //Вызов метода с использованием метода соответствующего делегату
            Console.Write("Среднее арифметическое чисел 4 и 6 равно ");
            ArithmeticOrGeometricMeanMethod(4, 6, ArithmeticMean);

            //Вызов метода с использованием лямбда-выражения
            Console.Write("Среднее геометрическое чисел 12 и 9 равно ");
            ArithmeticOrGeometricMeanMethod(12, 9, (double x, double y) => { return GeometricMean(x, y); });

            static void ArithmeticOrGeometricMeanFunc(double x, double y, Func<double, double, double> func)
            {
                Console.WriteLine(func(x, y));
            }

            Console.WriteLine("\nИспользование обобщенного делегата");
            Console.Write("Среднее арифметическое чисел 11 и 13 равно ");
            ArithmeticOrGeometricMeanFunc(11, 13, ArithmeticMean);

            Console.Write("Среднее арифметическое чисел 33 и 44 равно ");
            ArithmeticOrGeometricMeanFunc(33, 44, (x, y) => { return GeometricMean(x, y); });


            Console.WriteLine("\n\nРефлексия\n");

            Rectangle obj = new Rectangle(9, 12);
            Type t = obj.GetType();

            Console.WriteLine("Информация о прямоугольнике:\n");
            Console.WriteLine("Тип " + t.FullName + " унаследован от " + t.BaseType.FullName);
            Console.WriteLine("Пространство имён: " + t.Namespace);
            Console.WriteLine("В сборке " + t.AssemblyQualifiedName);

            Console.WriteLine("\nКонструкторы\n");
            foreach (var constructor in t.GetConstructors())
            {
                Console.WriteLine(constructor);
            }

            Console.WriteLine("\nМетоды\n");
            foreach (var method in t.GetMethods())
            {
                Console.WriteLine(method);
            }

            Console.WriteLine("\nСвойства\n");
            foreach (var property in t.GetProperties())
            {
                Console.WriteLine(property);
            }

            Console.WriteLine("\nВывод свойств с атрибутами:\n");
            foreach (var prop in t.GetProperties())
            {
                object attrObj;
                if (GetPropertyAttribute(prop, typeof(NewAttribute), out attrObj))
                {
                    NewAttribute attr = attrObj as NewAttribute;
                    Console.WriteLine(prop);
                }
            }

            Console.WriteLine("\nВызов метода класса с помощью рефлексии\n");
            object[] parameters = new object[] { };
            object Result = t.InvokeMember("Area", BindingFlags.InvokeMethod, null, obj, parameters);
            Console.WriteLine("Площадь прямоугольника = {0}", Result);

        }

        private static bool GetPropertyAttribute(PropertyInfo checkType, Type attributeType, out object attribute)
        {
            bool Result = false;
            attribute = null;
            //Поиск атрибутов с заданным типом
            var isAttribute = checkType.GetCustomAttributes(attributeType, false);
            if (isAttribute.Length > 0)
            {
                Result = true;
                attribute = isAttribute[0];
            }
            return Result;
        }
    }

    
}
