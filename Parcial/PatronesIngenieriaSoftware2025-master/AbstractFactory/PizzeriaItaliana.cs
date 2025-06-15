using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    public class PizzeriaItaliana:Pizzeria
    {
        public override Empanada CrearEmpanada()
        {
            return new EmpanadaJamonYQueso();
        }

        public override Pizza CrearPizza()
        {
            return new PizzaEspecial();
        }
    }
}
