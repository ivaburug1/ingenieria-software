using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Pizza pizza;
            Pizzeria pizzeria;

            pizzeria = new PizzeriaArgentina();
            pizza = pizzeria.CrearPizza("napo");
            Console.WriteLine(pizza.ToString());
            pizza = pizzeria.CrearPizza("cancha");
            Console.WriteLine(pizza.ToString());

            pizzeria = new PizzeriaItaliana();
            pizza = pizzeria.CrearPizza("napo");
            Console.WriteLine(pizza.ToString());
            pizza = pizzeria.CrearPizza("cancha");
            Console.WriteLine(pizza.ToString());
            Console.ReadKey();
        }
    }
}
