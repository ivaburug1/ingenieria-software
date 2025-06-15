using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    public abstract class Empanada
    {
        protected string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
        }
        public override string ToString()
        {
            return $"Empanada:{_descripcion}";
        }
    }
}
