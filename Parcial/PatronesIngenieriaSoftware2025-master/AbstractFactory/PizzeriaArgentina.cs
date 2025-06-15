using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    public class PizzeriaArgentina : Pizzeria
    {
        public override Empanada CrearEmpanada()
        {
            return new EmpanadaCarne();
        }

        public override Pizza CrearPizza()
        {
            return new PizzaCancha();
        }
    }
}
