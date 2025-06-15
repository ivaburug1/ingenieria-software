using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    public class PizzeriaEspañola : Pizzeria
    {
        public override Empanada CrearEmpanada()
        {
            return new EmpanadaPollo();
        }

        public override Pizza CrearPizza()
        {
            return new PizzaNapolitana();
        }
    }
}
